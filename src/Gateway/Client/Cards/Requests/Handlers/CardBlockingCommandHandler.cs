using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Models;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class CardBlockingCommandHandler : IRequestHandler<CardBlockingCommand, CardBlockingResultViewModel>
	{
		private readonly CardBlockingService _cardBlockingService;

		/*
		private static readonly Dictionary<CardBlockingReasonTypeEnum, Type> _enumToType = new Dictionary<CardBlockingReasonTypeEnum, Type>
		{
			{ CardBlockingReasonTypeEnum.FoundbByAnotherPerson, typeof(FoundbByAnotherPersonModel) },
			{ CardBlockingReasonTypeEnum.Lost, typeof(LostModel) },
			{ CardBlockingReasonTypeEnum.Stolen, typeof(StolenModel) },
			{ CardBlockingReasonTypeEnum.ForgottenInATM,  typeof(ForgottenInATMModel) },
			{ CardBlockingReasonTypeEnum.UnknownTransactions, typeof(UnknownTransactionsModel) },
			{ CardBlockingReasonTypeEnum.ReportedData, typeof(ReportedDataModel) },
			{ CardBlockingReasonTypeEnum.ClientIsCheater, typeof(ClientIsCheaterModel) },ReasonType = request.ReasonType, ReasonComment = request.ReasonComment,
			{ CardBlockingReasonTypeEnum.WithdrawnFromATM, typeof(WithdrawnFromATMModel) },
			{ CardBlockingReasonTypeEnum.BlockingOperations, typeof(BlockingOperationsModel) }
		};
		*/

		public CardBlockingCommandHandler(CardBlockingService cardService)
		{
			_cardBlockingService = cardService;
		}

		public async Task<CardBlockingResultViewModel> Handle(CardBlockingCommand request, CancellationToken cancellationToken)
		{
			ICardBlockingModel modifyRequest;
			ClientTransactionsModel clientTransactions = new ClientTransactionsModel();
			PropertyListModel properties = new PropertyListModel();
			TokenListModel deletedTokens = new TokenListModel();

			if (request.ReasonInformation?.clientTransactions != null)
			{
				TransactionListModel transactions = new TransactionListModel();
				foreach (var t in request.ReasonInformation?.clientTransactions?.transactions)
				{
					transactions.Add(new TransactionModel
					{
						OperationId = t.operationId,
						OperationDate = t.operationDate,
						MerchantName = t.merchantName,
						OperationDetails = t.operationDetails,
						Amount = t.amount,
						Currency = t.currency,
						Status = t.status
					});
				}

				clientTransactions.AllTransaction = request.ReasonInformation?.clientTransactions?.allTransaction;
				clientTransactions.Transactions = transactions;
			}

			if (request.ReasonInformation?.properties != null)
			{
				foreach (var p in request.ReasonInformation?.properties)
				{
					properties.Add(new PropertyModel
					{
						PersonDataType = p.personDataType,
						PersonDataTitle = p.personDataTitle,
						PersonDataValue = p.personDataValue,
					});
				}
			}

			if (request.ReasonInformation?.deletedTokens != null)
			{
				foreach (var t in request.ReasonInformation?.deletedTokens)
				{
					deletedTokens.Add(new TokenModel
					{
						TokenId = t.tokenId,
						Wallet = $"{t.requestorName}({t.wallet})"
					});
				}
			}

			switch (request.ReasonType)
			{
				case CardBlockingReasonTypeEnum.FoundbByAnotherPerson:
					modifyRequest = new CardBlockingModel<FoundbByAnotherPersonModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new FoundbByAnotherPersonModel
						{
							FullName = request.ReasonInformation?.fullName,
							Phone = request.ReasonInformation?.phone,
							FoundThings = request.ReasonInformation?.foundThings,
							Comments = request.ReasonInformation?.comments
						}
					};

					if (!string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $" {modifyRequest.Reason}";

					break;

				case CardBlockingReasonTypeEnum.Lost:
					modifyRequest = new CardBlockingModel<LostModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new LostModel
						{
							ClientTransactions = clientTransactions,
							IsClientCall = request.ReasonInformation?.isClientCall,
							IsClientLostPhone = request.ReasonInformation?.isClientLostPhone,
							Person = new PersonWhoCallsModel
							{
								FullName = request.ReasonInformation?.person?.fullName,
								Phone = request.ReasonInformation?.person?.phone,
								Comments = request.ReasonInformation?.person?.comments
							}
						}
					};
					if ((modifyRequest as CardBlockingModel<LostModel>).Reason.IsClientCall == false && !string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $". За запитом 3-ї особи ({modifyRequest.Reason})";
					else if (!string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $" {modifyRequest.Reason}";
					break;

				case CardBlockingReasonTypeEnum.Stolen:
					modifyRequest = new CardBlockingModel<StolenModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new StolenModel
						{
							ClientTransactions = clientTransactions,
							IsClientCall = request.ReasonInformation?.isClientCall,
							IsClientLostPhone = request.ReasonInformation?.isClientLostPhone,
							Person = new PersonWhoCallsModel
							{
								FullName = request.ReasonInformation?.person?.fullName,
								Phone = request.ReasonInformation?.person?.phone,
								Comments = request.ReasonInformation?.person?.comments
							}
						}
					};
					if ((modifyRequest as CardBlockingModel<StolenModel>).Reason.IsClientCall == false && !string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $". За запитом 3-ї особи ({modifyRequest.Reason})";
					else if (!string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $" {modifyRequest.Reason}";
					break;

				case CardBlockingReasonTypeEnum.ForgottenInATM:
					modifyRequest = new CardBlockingModel<ForgottenInATMModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new ForgottenInATMModel
						{
							ClientTransactions = clientTransactions,
							IsClientCall = request.ReasonInformation?.isClientCall,
							ContactPhone = request.ReasonInformation?.contactPhone,
							Person = new PersonWhoCallsModel
							{
								FullName = request.ReasonInformation?.person?.fullName,
								Phone = request.ReasonInformation?.person?.phone,
								Comments = request.ReasonInformation?.person?.comments
							}
						}
					};

					if ((modifyRequest as CardBlockingModel<ForgottenInATMModel>).Reason.IsClientCall == false && !string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $". За запитом 3-ї особи ({modifyRequest.Reason})";
					else
						modifyRequest.ReasonComment += $". ({modifyRequest.Reason})";


					break;

				case CardBlockingReasonTypeEnum.UnknownTransactions:
					modifyRequest = new CardBlockingModel<UnknownTransactionsModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new UnknownTransactionsModel
						{
							ClientTransactions = clientTransactions,
							ContactPhone = request.ReasonInformation?.contactPhone,
							IsFromUserDevice = request.ReasonInformation?.isFromUserDevice,
							TransactionType = request.ReasonInformation?.transactionType
						}
					};

					if (!string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $" {modifyRequest.Reason}";
					break;

				case CardBlockingReasonTypeEnum.ReportedData:
					modifyRequest = new CardBlockingModel<ReportedDataModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new ReportedDataModel
						{
							ContactPhone = request.ReasonInformation?.contactPhone,
							ClientTransactions = clientTransactions,
							Properties = properties,
							IsClientCall = request.ReasonInformation?.isClientCall,
							Person = new PersonWhoCallsModel
							{
								FullName = request.ReasonInformation?.person?.fullName,
								Phone = request.ReasonInformation?.person?.phone,
								Comments = request.ReasonInformation?.person?.comments
							},
							Cheater = new CheaterModel
							{
								FullName = request.ReasonInformation?.cheater?.fullName,
								Phone = request.ReasonInformation?.cheater?.phone,
								DateCall = request.ReasonInformation?.cheater?.dateCall,
								Comments = request.ReasonInformation?.cheater?.comments
							},
							DeletedTokens = deletedTokens,
							IsCancelAccessCode = request.ReasonInformation?.isCancelAccessCode,
							IsNeedDeleteToken = request.ReasonInformation?.isNeedDeleteToken,

						}
					};

					if ((modifyRequest as CardBlockingModel<ReportedDataModel>).Reason.IsClientCall == false && !string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $". За запитом 3-ї особи ({modifyRequest.Reason})";
					else if (!string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $" {modifyRequest.Reason}";
					break;

				case CardBlockingReasonTypeEnum.ClientIsCheater:
					modifyRequest = new CardBlockingModel<ClientIsCheaterModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new ClientIsCheaterModel
						{
							Person = new PersonWhoCallsModel
							{
								FullName = request.ReasonInformation?.person?.fullName,
								Phone = request.ReasonInformation?.person?.phone,
								Comments = request.ReasonInformation?.person?.comments,
								IsRefusedToProvideInfo = request.ReasonInformation?.person?.isRefusedToProvideInfo
							},
							ClientTransactions = clientTransactions
						}
					};

					if (!string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $". {modifyRequest.Reason}";

					break;

				case CardBlockingReasonTypeEnum.WithdrawnFromATM:
					modifyRequest = new CardBlockingModel<WithdrawnFromATMModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new WithdrawnFromATMModel
						{
							IsClientCall = request.ReasonInformation?.isClientCall,
							Person = new PersonWhoCallsModel
							{
								FullName = request.ReasonInformation?.person?.fullName,
								Phone = request.ReasonInformation?.person?.phone,
								Comments = request.ReasonInformation?.person?.comments
							}
						}
					};

					if ((modifyRequest as CardBlockingModel<WithdrawnFromATMModel>).Reason.IsClientCall == false && !string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment += $". За запитом 3-ї особи ({modifyRequest.Reason})";

					break;

				case CardBlockingReasonTypeEnum.BlockingOperations:
					modifyRequest = new CardBlockingModel<BlockingOperationsModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new BlockingOperationsModel()
					};
					break;
				case CardBlockingReasonTypeEnum.RequestedByClient:
					modifyRequest = new CardBlockingModel<RequestedByClientModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						Reason = new RequestedByClientModel()
					};
					break;
				case CardBlockingReasonTypeEnum.Other:
					modifyRequest = new CardBlockingModel<OtherModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new OtherModel()
					};
					break;
				case CardBlockingReasonTypeEnum.ReceivedSMSCode:
					modifyRequest = new CardBlockingModel<ReceivedSMSCodeModel>
					{
						SolarId = request.SolarId,
						CardId = request.CardId,
						ReasonType = request.ReasonType,
						ReasonComment = request.ReasonComment,
						Reason = new ReceivedSMSCodeModel
						{
							Properties = properties,
							ClientTransactions = clientTransactions,
							ContactPhone = request.ReasonInformation?.contactPhone,
							IsClientToldSMCodeFromSMS = request.ReasonInformation?.isClientToldSMCodeFromSMS,
							Cheater = new CheaterModel
							{
								FullName = request.ReasonInformation?.cheater?.fullName,
								Phone = request.ReasonInformation?.cheater?.phone,
								DateCall = request.ReasonInformation?.cheater?.dateCall,
								Comments = request.ReasonInformation?.cheater?.comments
							},
							DeletedTokens = deletedTokens,
							IsCancelAccessCode = request.ReasonInformation?.isCancelAccessCode,
							IsNeedDeleteToken = request.ReasonInformation?.isNeedDeleteToken,

							SenderName = request.ReasonInformation?.senderName,
							MerchantName = request.ReasonInformation?.merchantName,
							MerchantDate = request.ReasonInformation?.merchantDate,

							IsSMSMissingUPC = request.ReasonInformation?.isSMSMissingUPC,
							SmsDateUPC = request.ReasonInformation?.smsDateUPC,
							IsSMSMissingGate = request.ReasonInformation?.isSMSMissingGate,
							SmsDateGate = request.ReasonInformation?.smsDateGate,

							SecurityCodeOperationType = request.ReasonInformation?.securityCodeOperationType
						}
					};

					if ((modifyRequest as CardBlockingModel<ReceivedSMSCodeModel>).Reason.IsClientToldSMCodeFromSMS == true && !string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment = $"Повідомив дані шахраям ({modifyRequest.Reason})";
					else if (!string.IsNullOrEmpty(modifyRequest.Reason.ToString()))
						modifyRequest.ReasonComment = modifyRequest.Reason.ToString();
					break;

				default: throw new NotFoundRequestException();
			}

			return await _cardBlockingService.BlockCardAsync(modifyRequest, cancellationToken);
		}
	}
}
