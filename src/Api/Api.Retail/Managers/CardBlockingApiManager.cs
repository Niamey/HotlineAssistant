using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Bvr.Managers;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.ConstantNames;
using Vostok.Hotline.Api.Retail.Mapper.Cards;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Core.Common.Enums.Classifiers;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Extensions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Documents.Abstractions;
using Vostok.Hotline.Core.Documents.Models.Responses;
using Vostok.Hotline.Core.Documents.Requests;
using Vostok.Hotline.Core.Documents.Services;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;
using Vostok.Hotline.Document.Templates.Cards.Blocking.ConstantNames;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Enums;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Models;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class CardBlockingApiManager
	{
		private readonly ICardBlockingApiService _cardBlockingApiService;
		private readonly ICardApiService _cardApiService;
		private readonly CardApiManager _cardApiManager;
		private readonly AgreementApiManager _agreementApiManager;
		private readonly ClassifierApiManager _classifierApiManager;
		private readonly DeviceApiManger _deviceApiManger;
		private readonly DocumentService _documentService;
		private readonly BVRApiManager _bvrApiManager;
		private const string _classifierNotNeedChange = "Класифікатор не потрібно змінювати";
		private const string _notNeedResetPIN = "Не потрібно змінювати пін";
		private const string _notNeedBlockApp = "Додаток не потрібно блокувати";


		public CardBlockingApiManager(ICardBlockingApiService cardBlockingApiService, CardApiManager cardApiManager, AgreementApiManager agreementApiManager,
			DocumentService documentService, ICardApiService cardApiService, ClassifierApiManager classifierApiManager, DeviceApiManger deviceApiManger, BVRApiManager bvrApiManager)
		{
			_cardBlockingApiService = cardBlockingApiService;
			_cardApiManager = cardApiManager;
			_documentService = documentService;
			_agreementApiManager = agreementApiManager;
			_cardApiService = cardApiService;
			_classifierApiManager = classifierApiManager;
			_deviceApiManger = deviceApiManger;
			_bvrApiManager = bvrApiManager;
		}

		public async Task<CardBlockingResultApiModel> BlockCardAsync(ICardBlockingModel request, CancellationToken cancellationToken)
		{
			const string successBlockedTxt = "Картка заблокована успішно";
			const string errorBlockedTxt = "Помилка при блокуванні картки";
			var result = new CardBlockingResultApiModel()
			{
				OperationStatuses = new CardBlockingOperationStatusCollectionApiModel()
			};

			//Change status
			var response = await DetermineAndChangeStatusAsync(request, cancellationToken);
			result.Status = new StatusCommandApiViewModel()
			{
				Success = response.Success,
				Message = response.Success ? successBlockedTxt : errorBlockedTxt
			};
			response.Message = result.Status.Message;
			result.OperationStatuses.Add(response.ToCardBlockingOperationStatusApiModel());

			//Change classifier
			response = await DetermineAndChangeClassifierAsync(request, cancellationToken);
			if (response.Message != _classifierNotNeedChange)
			{
				result.OperationStatuses.Add(response.ToCardBlockingOperationStatusApiModel());
			}

			//Block MobileApp by user phone
			var statuses = await DetermineAndBlockMobileAppAsync(request, cancellationToken);
			foreach (var status in statuses)
			{
				if (status.Message != _notNeedResetPIN && status.Message != _notNeedBlockApp)
				{
					result.OperationStatuses.Add(status.ToCardBlockingOperationStatusApiModel());
				}
			}

			//Change classifier
			response = await DetermineAndChangeClassifierAsync(request, cancellationToken);
			if (response.Message != _classifierNotNeedChange)
			{
				result.OperationStatuses.Add(response.ToCardBlockingOperationStatusApiModel());
			}

			// Get template for Operator
			var templateResponse = await GetTemplateForOperatorAsync(request, cancellationToken);
			if (!templateResponse.HasError)
				result.ResultForOperator = templateResponse.Result;




			return result;
		}

		private async Task<CardChangeStatusResultApiModel> ChangeStatusAsync(long cardId, CardStatusEnum status, string comment, CancellationToken cancellationToken)
		{
			var retailStatus = status.ToCardStatusRetailEnum();
			return await _cardApiService.ChangeStatusAsync(cardId, retailStatus, comment, cancellationToken);
		}

		private async Task<StatusCommandApiViewModel> SendBlockEmailAsync(ICardBlockingModel request, CancellationToken cancellationToken)
		{
			var model = new CardBlockingModel<ICardBlockingReason>
			{
				SolarId = request.SolarId,
				CardId = request.CardId,
				ReasonType = request.ReasonType,
				Reason = request.Reason
			};

			var resultTemplate = await GetTemplateAsync(request, $"~/Cards/Blocking/Templates/{model.ReasonType}Template.cshtml", cancellationToken);
			var subject = $"Запит на блокування карти. Причина '{model.GetCardBlockingReasonType()}'";

			return await _cardBlockingApiService.SendEmailAsync(subject, resultTemplate.Result, cancellationToken);
		}

		private async Task<Dictionary<string, object>> GetCardAgreementNumberAsync(long solarId, long cardId, CancellationToken cancellationToken)
		{
			CardApiModel cardDetail = await _cardApiManager.GetCardAsync(solarId, cardId, cancellationToken);
			string maskedCardNumber = CardHelper.GetMaskedCardNumber(cardDetail.Number);
			string agreementNumber = await _agreementApiManager.GetAgreementNumberAsync(solarId, cardDetail.AgreementId, cancellationToken);

			return new Dictionary<string, object>
			{
				{ CardBlockingConstant.MaskedCardNumber, maskedCardNumber},
				{ CardBlockingConstant.AgreementNumber, agreementNumber }
			};
		}

		public async Task<RenderResponseModel> GetTemplateAsync<T>(T request, string templatePath, CancellationToken cancellationToken) where T : ICardBlockingModel
		{
			return await GetTemplateAsync(request, templatePath, null, cancellationToken);
		}

		public async Task<RenderResponseModel> GetTemplateAsync<T>(T request, string templatePath, Dictionary<string, object> viewData, CancellationToken cancellationToken) where T : ICardBlockingModel
		{
			if (viewData == null)
				viewData = new Dictionary<string, object>();

			switch (request.ReasonType)
			{
				case CardBlockingReasonTypeEnum.FoundbByAnotherPerson:
					return await CreateTemplateAsync(
						new CardBlockingModel<FoundbByAnotherPersonModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							ReasonComment = request.ReasonComment,
							Reason = (FoundbByAnotherPersonModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.Lost:
					return await CreateTemplateAsync(
						new CardBlockingModel<LostModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							Reason = (LostModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.Stolen:
					return await CreateTemplateAsync(
						new CardBlockingModel<StolenModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							Reason = (StolenModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.ForgottenInATM:
					return await CreateTemplateAsync(
						new CardBlockingModel<ForgottenInATMModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							Reason = (ForgottenInATMModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.UnknownTransactions:
					return await CreateTemplateAsync(
						new CardBlockingModel<UnknownTransactionsModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							Reason = (UnknownTransactionsModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.ReportedData:
					return await CreateTemplateAsync(
						new CardBlockingModel<ReportedDataModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							Reason = (ReportedDataModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.ClientIsCheater:
					return await CreateTemplateAsync(
						new CardBlockingModel<ClientIsCheaterModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							Reason = (ClientIsCheaterModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.WithdrawnFromATM:
					return await CreateTemplateAsync(
						new CardBlockingModel<WithdrawnFromATMModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							ReasonComment = request.ReasonComment,
							Reason = (WithdrawnFromATMModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.BlockingOperations:
					return await CreateTemplateAsync(
						new CardBlockingModel<BlockingOperationsModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							Reason = (BlockingOperationsModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);

				case CardBlockingReasonTypeEnum.RequestedByClient:
					return await CreateTemplateAsync(
						new CardBlockingModel<RequestedByClientModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							Reason = (RequestedByClientModel)request.Reason
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);
				case CardBlockingReasonTypeEnum.Other:
					return await CreateTemplateAsync(
						new CardBlockingModel<OtherModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							ReasonComment = request.ReasonComment,
							Reason = (OtherModel)request.Reason,
							UserLogin = request.UserLogin,
							ModifyDate = request.ModifyDate
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);
				case CardBlockingReasonTypeEnum.ReceivedSMSCode:
					return await CreateTemplateAsync(
						new CardBlockingModel<ReceivedSMSCodeModel>()
						{
							SolarId = request.SolarId,
							CardId = request.CardId,
							ReasonType = request.ReasonType,
							ReasonComment = request.ReasonComment,
							Reason = (ReceivedSMSCodeModel)request.Reason,
							UserLogin = request.UserLogin,
							ModifyDate = request.ModifyDate
						},
						templatePath,
						viewData.ConcatDict(await GetCardAgreementNumberAsync(request.SolarId.Value, request.CardId.Value, cancellationToken)),
						cancellationToken);
				default:
					throw new NotSupportedException();
			}
		}

		private async Task<RenderResponseModel> CreateTemplateAsync<T>(T request, string templatePath, CancellationToken cancellationToken)
			where T : ICardBlockingModel
		{
			return await CreateTemplateAsync(request, templatePath, null, cancellationToken);
		}

		private async Task<RenderResponseModel> CreateTemplateAsync<T>(T request, string templatePath, Dictionary<string, object> viewData, CancellationToken cancellationToken)
			where T : ICardBlockingModel
		{
			//Create Template
			var resultTemplate = await _documentService.RenderAsync(new RenderQuery<ITemplateViewModel>()
			{
				TemplatePath = templatePath,
				TokenModel = (ITemplateViewModel)request,
				ViewData = viewData
			}, cancellationToken);

			if (resultTemplate.HasError)
				throw new FailedRequestException(resultTemplate.ErrorMessage);

			return resultTemplate;
		}

		private async Task<StatusCommandApiViewModel> DetermineAndChangeStatusAsync(ICardBlockingModel request, CancellationToken cancellationToken)
		{
			CardStatusEnum status = CardStatusEnum.Undefined;

			switch (request.ReasonType)
			{
				case CardBlockingReasonTypeEnum.FoundbByAnotherPerson:
				case CardBlockingReasonTypeEnum.Lost:
				case CardBlockingReasonTypeEnum.ForgottenInATM:
					status = CardStatusEnum.Lost;
					break;

				case CardBlockingReasonTypeEnum.Stolen:
					status = CardStatusEnum.Stolen;
					break;
				case CardBlockingReasonTypeEnum.Other:
				case CardBlockingReasonTypeEnum.UnknownTransactions:
				case CardBlockingReasonTypeEnum.ReportedData:
				case CardBlockingReasonTypeEnum.ReceivedSMSCode:
					status = CardStatusEnum.Blocked;
					break;
				case CardBlockingReasonTypeEnum.WithdrawnFromATM:
				case CardBlockingReasonTypeEnum.BlockingOperations:
				case CardBlockingReasonTypeEnum.RequestedByClient:
					status = CardStatusEnum.Suspend;
					break;
				case CardBlockingReasonTypeEnum.ClientIsCheater:
					status = CardStatusEnum.Blocked;

					var reason = request.Reason as ClientIsCheaterModel;
					if (reason.Person.IsRefusedToProvideInfo.Value)
					{
						status = CardStatusEnum.Active;
					}
					break;

				default:
					throw new NotSupportedException();
			}

			if (status != CardStatusEnum.Undefined)
			{
				var result = await ChangeStatusAsync(request.CardId.Value, status, request.ReasonComment, cancellationToken);
				if (result != null)
				{
					request.UserLogin = result.Comment?.UserLogin;
					request.ModifyDate = result.Comment?.ModifyDate;
					return result.Status;
				}
			}

			return new StatusCommandApiViewModel()
			{
				Success = false,
				Message = "Статус картки не змінений"
			};
		}

		private async Task<StatusCommandApiViewModel> DetermineAndChangeClassifierAsync(ICardBlockingModel request, CancellationToken cancellationToken)
		{
			var isNeedChange = false;
			var classifierCode = String.Empty;
			var classifierValue = String.Empty;
			ClassifierTypeEnum type = ClassifierTypeEnum.Undefined;

			switch (request.ReasonType)
			{
				case CardBlockingReasonTypeEnum.ReportedData:
					ReportedDataModel reportedData = request.Reason as ReportedDataModel;
					if (!(reportedData.IsClientCall ?? false) || !string.IsNullOrEmpty(reportedData.ContactPhone))
					{
						isNeedChange = true;
						type = ClassifierTypeEnum.Accessor;
						classifierCode = ClassifierConstant.Scammer;
						classifierValue = "YES";
					}
					break;
				case CardBlockingReasonTypeEnum.Other:
					isNeedChange = true;
					type = ClassifierTypeEnum.Accessor;
					classifierCode = ClassifierConstant.Scammer;
					classifierValue = "YES";
					break;

				case CardBlockingReasonTypeEnum.ClientIsCheater:
					var reason = request.Reason as ClientIsCheaterModel;
					if (!reason.Person.IsRefusedToProvideInfo.Value)
					{
						isNeedChange = true;
						type = ClassifierTypeEnum.Accessor;
						classifierCode = ClassifierConstant.Scammer;
						classifierValue = "YES";
					}
					break;

				case CardBlockingReasonTypeEnum.UnknownTransactions:
					UnknownTransactionsModel model = request.Reason as UnknownTransactionsModel;
					if (model.TransactionType != null)
					{
						isNeedChange = true;
						type = ClassifierTypeEnum.Accessor;
						classifierCode = ClassifierConstant.Scammer;
						classifierValue = "YES";
					}
					break;
				case CardBlockingReasonTypeEnum.ReceivedSMSCode:
					ReceivedSMSCodeModel receivedSMSCode = request.Reason as ReceivedSMSCodeModel;
					if (((receivedSMSCode.IsClientToldSMCodeFromSMS ?? false) && !string.IsNullOrEmpty(receivedSMSCode.Cheater?.FullName)) ||
						!(receivedSMSCode.IsClientToldSMCodeFromSMS ?? false))
					{
						isNeedChange = true;
						type = ClassifierTypeEnum.Accessor;
						classifierCode = ClassifierConstant.Scammer;
						classifierValue = "YES";
					}
					break;
				default:
					break;
			}

			if (type != ClassifierTypeEnum.Undefined)
			{
				var result = await _classifierApiManager.SetClassifierAsync(type, request.SolarId.Value, classifierCode, classifierValue, request.ReasonComment, cancellationToken);
				return result;
			}
			else if (isNeedChange)
			{
				return new StatusCommandApiViewModel()
				{
					Success = false,
					Message = "Класифікатор не змінений"
				};
			}
			else
			{
				return new StatusCommandApiViewModel()
				{
					Success = true,
					Message = _classifierNotNeedChange
				};
			}

		}

		private async Task<List<StatusCommandApiViewModel>> DetermineAndBlockMobileAppAsync(ICardBlockingModel request, CancellationToken cancellationToken)
		{
			List<StatusCommandApiViewModel> result = new List<StatusCommandApiViewModel>();
			var isNeedResetPIN = false;
			var isNeedBlock = false;

			switch (request.ReasonType)
			{
				case CardBlockingReasonTypeEnum.UnknownTransactions:
					UnknownTransactionsModel model = request.Reason as UnknownTransactionsModel;
					if (model.TransactionType == CardBlockingTransactionTypeEnum.MobileApp)
					{
						isNeedBlock = !(model.IsFromUserDevice ?? false);
						if (model.IsFromUserDevice != null)
						{
							isNeedResetPIN = !(bool)model.IsFromUserDevice;
						}
					}
					break;
				case CardBlockingReasonTypeEnum.Lost:
					LostModel lostModel = request.Reason as LostModel;
					if (lostModel.IsClientLostPhone == true)
					{
						isNeedResetPIN = true;
						isNeedBlock = true;
					}
					break;
				case CardBlockingReasonTypeEnum.Stolen:
					StolenModel stolenModel = request.Reason as StolenModel;
					if (stolenModel.IsClientLostPhone == true)
					{
						isNeedResetPIN = true;
						isNeedBlock = true;
					}
					break;

				case CardBlockingReasonTypeEnum.ReportedData:
					ReportedDataModel reportedData = request.Reason as ReportedDataModel;
					isNeedResetPIN = reportedData.IsCancelAccessCode ?? false;
					break;
				case CardBlockingReasonTypeEnum.ReceivedSMSCode:
					ReceivedSMSCodeModel receivedSMSCode = request.Reason as ReceivedSMSCodeModel;
					isNeedResetPIN = receivedSMSCode.SecurityCodeOperationType == SecurityCodeOperationTypeEnum.MobileAppPassword;
					break;

				default:
					break;
			}
			CardApiModel cardDetail = new CardApiModel();

			if (isNeedResetPIN || isNeedBlock)
				cardDetail = await _cardApiManager.GetCardAsync(request.SolarId, request.CardId.Value, cancellationToken);

			if (isNeedResetPIN)
				result.Add(await _deviceApiManger.ResetPINDeviceAsync(cardDetail.FinancePhone, cancellationToken));
			else
			{
				result.Add(new StatusCommandApiViewModel()
				{
					Success = true,
					Message = _notNeedResetPIN
				});
			}

			if (isNeedBlock)
				result.Add(await _bvrApiManager.AddPermanentBlockingAsync(cardDetail.FinancePhone, "Mobile application blocked", cancellationToken));
			else
			{
				result.Add(new StatusCommandApiViewModel()
				{
					Success = true,
					Message = _notNeedBlockApp
				});
			}

			return result;
		}

		private async Task<RenderResponseModel> GetTemplateForOperatorAsync(ICardBlockingModel request, CancellationToken cancellationToken)
		{
			var model = new CardBlockingModel<ICardBlockingReason>
			{
				SolarId = request.SolarId,
				CardId = request.CardId,
				ReasonType = request.ReasonType,
				ReasonComment = request.ReasonComment,
				Reason = request.Reason,
				UserLogin = request.UserLogin,
				ModifyDate = request.ModifyDate
			};

			return await GetTemplateAsync(
						model,
						$"~/Cards/Blocking/Templates/{model.ReasonType}Template.cshtml",
						new Dictionary<string, object>()
						{
							{ CardBlockingConstant.ForOperator, true}
						},
						cancellationToken
			);
		}
	}
}