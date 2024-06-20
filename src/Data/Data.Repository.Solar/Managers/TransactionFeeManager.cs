using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;
using Vostok.Hotline.Data.Repository.Solar.Managers.Base;
using Vostok.Hotline.Data.Repository.Solar.Mappers;
using Vostok.Hotline.Data.Repository.Solar.Models.Responses;

namespace Vostok.Hotline.Data.Repository.Solar.Managers
{
	class TransactionFeeManager : BaseSolarManager
	{
		public TransactionFeeManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}

		public async Task<TransactionFeeResponseModel[]> GetFeeByTxnIdsAsync(long[] txnIds, CancellationToken cancellationToken)
		{
			TransactionFeeResponseModel[] result = null;
			using (var transaction = TransactionFactory.CreateReadUncommitted())
			{
				var projection = DbContext.SolarTransactionFees.Where(x => x.TxnId.HasValue && txnIds.Contains(x.TxnId.Value));
				var feeProjection = GetFeeProjection(projection);
				result = await feeProjection.AsNoTracking().ToArrayAsync(cancellationToken);

				transaction.Complete();
			};

			return result;
		}
		
		private IQueryable<TransactionFeeResponseModel> GetFeeProjection(IQueryable<SolarTransactionFee> query)
		{
			return query.Select(response => new TransactionFeeResponseModel {
				FeeId = response.Id,
				TxnId = response.TxnId,
				AgreementId = response.AgreementId,
				FeeAmount = response.FeeAmount ?? 0,
				FeeCurrency = response.FeeCurrency,
				BillingAmount = response.BillingAmount ?? 0,
				BillingCurrency = response.BillingCurrency,
				TxnTypeName = response.TxnType.Name,
				TxnTypeDescription = response.TxnType.Description
			});

		}
	}
}
