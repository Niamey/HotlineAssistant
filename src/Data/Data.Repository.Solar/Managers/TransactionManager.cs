using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Thinktecture;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Core.Common.Enums.Transactions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Data.EF.Entities.Solar;
using Vostok.Hotline.Data.Repository.Helpers;
using Vostok.Hotline.Data.Repository.Solar.Managers.Base;
using Vostok.Hotline.Data.Repository.Solar.Mappers;
using Vostok.Hotline.Data.Repository.Solar.Models.Request;
using Vostok.Hotline.Data.Repository.Solar.Models.Responses;

namespace Vostok.Hotline.Data.Repository.Solar.Managers
{
	public class TransactionManager : BaseSolarManager
	{
		private readonly AgreementManager _agreementManager;
		private readonly TransactionFeeManager _transactionFeeManager;



		public TransactionManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
			_agreementManager = ServiceProvider.GetRequiredService<AgreementManager>();
			_transactionFeeManager = ServiceProvider.GetRequiredService<TransactionFeeManager>();
		}

		private async Task<List<long>> GetAccessorIdsAsync(long clientId, string cardNumber, CancellationToken cancellationToken)
		{
			var result = new List<long>();
			using (var transaction = TransactionFactory.CreateReadUncommitted())
			{
				result = await DbContext
								.SolarAccessors
								// .Include(x => x.Agreement)
								.Join(DbContext.SolarAgreements, ac => ac.AgreementId, ag => ag.Id, (ac, ag) => new { 
									AccessorId = ac.AccessorId,
									AccessNumber = ac.AccessNumber,
									ClientId = ag.ClientId
								})
								.Where(x => x.ClientId == clientId)
								.Where(x => Microsoft.EntityFrameworkCore.EF.Functions.Like(x.AccessNumber, $"%{cardNumber}"))
								.AsNoTracking()
								.Select(x => x.AccessorId)
								.ToListAsync(cancellationToken);
			}
			
			return result;
		}

		private async Task<long?> GetLastReversalTxnIdAsync(long? txnId, CancellationToken cancellationToken)
		{					
			using var transaction = TransactionFactory.CreateReadUncommitted();
			
			var param = new List<SqlParameter>();
			param.Add(new SqlParameter("txnId", DbHelper.GetDbValue(txnId)));

			var sqlQuery = @$"WITH rev AS
							(SELECT t.ID, t.TXN_DIRECTION
								FROM { TransactionConstant.BO_TXN } t
								WHERE t.ID = @txnId

								UNION ALL

								SELECT t.ID, t.TXN_DIRECTION
								FROM rev
								JOIN { TransactionConstant.BO_TXN } t ON t.AUTH_TXN_ID = rev.ID or t.PREV_TXN_ID = rev.ID)

							SELECT cast(MAX(rev.ID) as bigint) AS TxnId
								FROM rev
								WHERE rev.TXN_DIRECTION = 'R'";

			var result = await DbContext
							.SolarTransactionReversalQuery
							.FromSqlRaw(sqlQuery, param.ToArray())
							.AsNoTracking()
							.Select(x=>x.TxnId)
							.ToListAsync(cancellationToken);

			transaction.Complete();

			return result.FirstOrDefault();
		}

		private async Task<long?> GetOriginalTxnIdAsync(long? txnId, CancellationToken cancellationToken)
		{
			using var transaction = TransactionFactory.CreateReadUncommitted();
			
			var param = new List<SqlParameter>();
			param.Add(new SqlParameter("txnId", DbHelper.GetDbValue(txnId)));

			var sqlQuery = @$"WITH orig AS
							(SELECT
								t.ID, t.PREV_TXN_ID, t.TXN_DIRECTION
								FROM { TransactionConstant.BO_TXN } t
								where t.ID = @txnId

								UNION ALL

								SELECT
								t.ID, t.PREV_TXN_ID, t.TXN_DIRECTION
								FROM orig
								JOIN { TransactionConstant.BO_TXN } t
								ON t.ID = orig.PREV_TXN_ID)

							SELECT cast(MIN(orig.ID) as bigint) as TxnId
							FROM orig
							WHERE orig.TXN_DIRECTION = 'O'";

			var result = await DbContext
							.SolarTransactionReversalQuery
							.FromSqlRaw(sqlQuery, param.ToArray())
							.AsNoTracking()
							.Select(x=>x.TxnId)
							.ToListAsync(cancellationToken);
			
			transaction.Complete();

			return result.FirstOrDefault();
		}

		public async Task<SearchPagedResponseRowModel<TransactionResponseModel>> SearchTransactionAsync(TransactionSearchRequest searchRequest, CancellationToken cancellationToken)
		{
			SearchPagedResponseRowModel<TransactionResponseModel> response = null;

			long? originalTxnId = null;
			if (searchRequest.TxnId != null) {
				var lastReversalTxnId = await GetLastReversalTxnIdAsync(searchRequest.TxnId, cancellationToken);
				if (lastReversalTxnId != null)
				{
					originalTxnId = await GetOriginalTxnIdAsync(lastReversalTxnId, cancellationToken);
				}

				if (lastReversalTxnId == null || originalTxnId == null)
				{
					return new SearchPagedResponseRowModel<TransactionResponseModel> { IsNextPageAvailable = false };
				}
			}

			List<long> accessorIds = null;
			if (searchRequest.CardNumber != null)
			{
				accessorIds = await GetAccessorIdsAsync(searchRequest.ClientId.Value, searchRequest.CardNumber, cancellationToken);
				if (accessorIds.Count == 0)
				{
					return new SearchPagedResponseRowModel<TransactionResponseModel> { 
						IsNextPageAvailable = false
					};					
				}
			}

			using (var transaction = TransactionFactory.CreateReadUncommitted())
			{
				string additionalConditions = "";
				string filterConditions = "";
				int? maxPageRecords = null;

				var param = new List<SqlParameter>();
				param.Add(new SqlParameter("clientId", DbHelper.GetDbValue(searchRequest.ClientId)));

				if (originalTxnId != null)
				{
					additionalConditions += " AND t.ID = @txnId";
					param.Add(new SqlParameter("txnId", DbHelper.GetDbValue(originalTxnId)));
				}
				else
				{

					if (searchRequest.DateFrom != null)
					{
						additionalConditions += " AND t.TXN_DATE >= @dateFrom";
						param.Add(new SqlParameter("dateFrom", DbHelper.GetDbValue(searchRequest.DateFrom)));
					}

					if (searchRequest.DateTo != null)
					{
						additionalConditions += " AND t.TXN_DATE < @dateTo";
						param.Add(new SqlParameter("dateTo", DbHelper.GetDbValue(searchRequest.DateTo.Value.AddDays(1))));
					}

					if (searchRequest.AmountFrom != null)
					{
						additionalConditions += " AND ABS(t.TXN_AMOUNT) >= @amountFrom";
						param.Add(new SqlParameter("amountFrom", DbHelper.GetDbValue(searchRequest.AmountFrom)));
					}

					if (searchRequest.AmountTo != null)
					{
						additionalConditions += " AND ABS(t.TXN_AMOUNT) <= @amountTo";
						param.Add(new SqlParameter("amountTo", DbHelper.GetDbValue(searchRequest.AmountTo)));
					}

					if (searchRequest.CardNumber != null)
					{
						var accessorListStr = String.Join(',', accessorIds);
						additionalConditions += $" AND (t.ORIG_ACCESSOR_ID in ({accessorListStr}) OR t.RCVR_ACCESSOR_ID in ({accessorListStr}))";
					}

					filterConditions = $" WHERE cte.rowNumber BETWEEN @rowFrom AND @rowTo";
				
					
					var paging = (searchRequest as ISearchRequestPagedModel)?.Paging;
					if (paging != null)
					{
						var between = SearchBuildHelper.ConvertPagingToBetweenValues(paging);
						if (between != null)
						{
							var (rowFrom, rowTo, pageSize) = between.Value;
							maxPageRecords = pageSize;
							rowTo++;
							param.Add(new SqlParameter("rowFrom", DbHelper.GetDbValue(rowFrom)));
							param.Add(new SqlParameter("rowTo", DbHelper.GetDbValue(rowTo)));
						}
					}
				}

				var sqlQuery = @$"WITH cte AS
				(SELECT
					ROW_NUMBER() OVER(ORDER BY t.TXN_DATE DESC) AS rowNumber,
					t.id
					FROM { TransactionConstant.BO_TXN } t
					JOIN { TransactionConstant.BO_AGREEMENT } ao ON ao.id = t.ORIG_AGREEMENT_ID
					JOIN { TransactionConstant.BO_AGREEMENT } ar ON ar.id = t.RCVR_AGREEMENT_ID
					WHERE (ao.CLIENT_ID = @clientId OR ar.CLIENT_ID = @clientId) 
					{ additionalConditions } 
					AND t.AUTH_TXN_ID IS NULL
				)
				SELECT
					cte.rowNumber, 
					cte.id AS parentId, 
					c.id AS childId,
  					p.STATUS AS status,
					p.TXN_TYPE_ID AS txnTypeId,
					pt.CODE AS txnTypeCode,
					pt.NAME AS txnTypeName,
					pt.DESCRIPTION AS txnTypeDescription,
					pt.DIRECTION AS txnTypeDirection,
					p.TXN_CLASS AS txnClass,
					p.TXN_CATEGORY AS txnCategory,
					p.TXN_DIRECTION AS txnDirection,
					p.txn_date AS txnDate,
					p.AUTH_CODE as authCode,
					p.RET_REF_NUMBER AS retRefNumber,
					p.TXN_AMOUNT AS txnAmount,
					p.TXN_CURRENCY AS txnCurrency,
					p.TXN_DETAILS AS txnDetails,
					p.RESPONSE_CODE as responseCode,
					p.MCC as mcc,
					p.CARD_ACCEPTOR_ID as merchant,
					p.MERCHANT_NAME as merchantName,
					p.MERCHANT_COUNTRY as merchantCountry,
					p.MERCHANT_CITY as merchantCity,
					p.MERCHANT_STATE as merchantState,
					p.ORIG_ACCESSOR_ID as origCardId,
					p.ORIG_NUMBER as origCardNum,
					p.ORIG_AGREEMENT_ID as origAgreementId,
					pao.AGR_NUMBER as origAgreementNum,
					pao.CLIENT_ID as origClientId,
					p.ORIG_AMOUNT as origAmount,
					p.ORIG_CURRENCY as origCurrency,
					p.ORIG_BILLING_AMOUNT as origBillingAmount,
					p.ORIG_BILLING_CURRENCY as origBillingCurrency,
					p.RCVR_ACCESSOR_ID as rcvrCardId,
					p.RCVR_NUMBER as rcvrCardNum,
					p.RCVR_AGREEMENT_ID as rcvrAgreementId,
					par.AGR_NUMBER as rcvrAgreementNum,
					par.CLIENT_ID as rcvrClientId,
					p.RCVR_AMOUNT as rcvrAmount,
					p.RCVR_CURRENCY as rcvrCurrency,
					p.RCVR_BILLING_AMOUNT as rcvrBillingAmount,
					p.RCVR_BILLING_CURRENCY as rcvrBillingCurrency,
					c.STATUS as childStatus,
					c.TXN_TYPE_ID as childTxnTypeId,
					ct.CODE as childTxnTypeCode,
					ct.NAME as childTxnTypeName,
					ct.DESCRIPTION as childTxnTypeDescription,
					ct.DIRECTION as childTxnTypeDirection,
					c.TXN_CLASS as childTxnClass,
					c.TXN_CATEGORY as childTxnCategory,
					c.TXN_DIRECTION as childTxnDirection,
					c.TXN_DATE as childTxnDate,
					c.TXN_AMOUNT as childTxnAmount,
					c.TXN_CURRENCY as childTxnCurrency,
					c.TXN_DETAILS as childTxnDetails,
					c.RESPONSE_CODE AS childResponseCode,
					c.ORIG_AGREEMENT_ID as childOrigAgreementId,
					cao.CLIENT_ID as childOrigClientId,
					c.RCVR_AGREEMENT_ID as childRcvrAgreementId,
					car.CLIENT_ID as childRcvrClientId,

					-- td.SerializedModel AS originalTxnData,
					td.AcqInstitutionCode,
					td.TerminalType,
					td.CardDataInputMode,
					td.OnlinePinVerificationResult,
					td.Cvv2VerificationResult,
					td.CavvValidationResult,
					td.Stan
				FROM cte
					JOIN { TransactionConstant.BO_TXN } p ON p.ID = cte.id
					LEFT JOIN { TransactionConstant.BO_TXN_TYPE } pt ON pt.ID = p.TXN_TYPE_ID
					LEFT JOIN { TransactionConstant.BO_AGREEMENT } pao ON pao.ID = p.ORIG_AGREEMENT_ID
					LEFT JOIN { TransactionConstant.BO_AGREEMENT } par ON par.ID = p.RCVR_AGREEMENT_ID
					LEFT JOIN { TransactionConstant.BO_TXN } c ON c.AUTH_TXN_ID = cte.id
					LEFT JOIN { TransactionConstant.BO_TXN_TYPE } ct ON ct.ID = c.TXN_TYPE_ID
					LEFT JOIN { TransactionConstant.BO_AGREEMENT } cao ON cao.ID = c.ORIG_AGREEMENT_ID
					LEFT JOIN { TransactionConstant.BO_AGREEMENT } car ON car.ID = c.RCVR_AGREEMENT_ID
					LEFT JOIN { TransactionConstant.BO_ORIGINAL_TXN_DATA } td ON td.TxnId = p.ID
				{ filterConditions }
					ORDER BY cte.rowNumber, c.txn_date, c.id";

				var result = await DbContext
							.SolarTransactionQuery
							.FromSqlRaw(sqlQuery, param.ToArray())
							.AsNoTracking()
							.ToListAsync(cancellationToken);

				response = new SearchPagedResponseRowModel<TransactionResponseModel>();
				decimal lastId = -1;
				TransactionResponseModel record = null;
				foreach (var row in result)
				{
					if (lastId != row.ParentId)
					{
						lastId = row.ParentId;

						record = row.ToTransactionResponseModel();
						response.Rows.Add(record);
					}

					if (row.ChildId != null)
					{
						if (record.Childs == null)
							record.Childs = new TransactionChildCollectionResponseModel();

						record.Childs.Add(row.ToTransactionChildResponseModel());
					}
				}

				response.IsNextPageAvailable = false;
				if (maxPageRecords != null && response.Rows.Count > maxPageRecords)
				{
					response.IsNextPageAvailable = true;
					response.Rows.RemoveAt(response.Rows.Count - 1);
				}

				transaction.Complete();
			}

			if (response != null)
			{
				var txnIds = response.Rows.Select(x => x.TxnId).ToArray();
				var fees = await _transactionFeeManager.GetFeeByTxnIdsAsync(txnIds, cancellationToken);
				foreach (var row in response.Rows)
				{
					if (row.OrigClientId == searchRequest.ClientId.Value)
					{
						row.CardId = row.OrigCardId;
						row.CardNum = row.OrigCardNum;
						row.AgreementId = row.OrigAgreementId;
						row.AgreementNum = row.OrigAgreementNum;
						row.ClientId = row.OrigClientId;
						row.Amount = row.OrigAmount;
						row.Currency = row.OrigCurrency;
						row.BillingAmount = row.OrigBillingAmount;
						row.BillingCurrency = row.OrigBillingCurrency;
					}
					else
					{
						row.CardId = row.RcvrCardId;
						row.CardNum = row.RcvrCardNum;
						row.AgreementId = row.RcvrAgreementId;
						row.AgreementNum = row.RcvrAgreementNum;
						row.ClientId = row.RcvrClientId;
						row.Amount = row.RcvrAmount;
						row.Currency = row.RcvrCurrency;
						row.BillingAmount = row.RcvrBillingAmount;
						row.BillingCurrency = row.RcvrBillingCurrency;
					}
					var txnFees = fees.Where(x => x.TxnId == row.TxnId && x.AgreementId == row.AgreementId);
					row.Fees = new TransactionFeeCollectionResponseModel(txnFees);

					row.FeeAmount = row.Fees.Sum(x => x.FeeAmount) ?? 0;
					// row.FeeCurrency = row.Fees.Select(x => x.FeeCurrency).FirstOrDefault() ?? "980";
					row.FeeCurrency = "980"; // Комиссия всегда в гривне

					// для операции solarTransfer делаем подмену направления операции и сумм.
					if (row.TxnTypeCode == "solarTransfer" && row.OrigClientId == searchRequest.ClientId.Value)
					{
						//row.TxnAmount *= -1;
						row.TxnTypeDirection = DirectionClassEnum.Credit;
					}

					if (row.Childs != null) {
						var childTxnIds = row.Childs.Select(x => x.TxnId).ToArray();
						var childFees = await _transactionFeeManager.GetFeeByTxnIdsAsync(childTxnIds, cancellationToken);
						foreach (var child in row.Childs)
						{
							if (child.OrigClientId == searchRequest.ClientId.Value)
							{
								child.AgreementId = row.OrigAgreementId;
								child.ClientId = row.OrigClientId;
							}
							else
							{
								child.AgreementId = row.RcvrAgreementId;
								child.ClientId = row.RcvrClientId;
							}

							var childTxnFees = childFees.Where(x => x.TxnId == child.TxnId && x.AgreementId == child.AgreementId);
							child.Fees = new TransactionFeeCollectionResponseModel(childTxnFees);

							child.FeeAmount = child.Fees.Sum(x => x.FeeAmount) ?? 0;
							child.FeeCurrency = "980"; // Комиссия всегда в гривне

							// для операции solarTransfer делаем подмену направления операции и сумм.
							if (child.TxnTypeCode == "solarTransfer" && child.OrigClientId == searchRequest.ClientId.Value)
							{
								//row.TxnAmount *= -1;
								child.TxnTypeDirection = DirectionClassEnum.Credit;
							}
						}
					}
				}
			}
			return response;
		}
	}
}