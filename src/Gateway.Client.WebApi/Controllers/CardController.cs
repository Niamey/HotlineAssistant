using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Base.Controllers;
using Vostok.Hotline.Core.Common.Models.Responses;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Core.Common.Models.Validations;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardController : MediatrControllerBase
	{
		public CardController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpGet("List")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<CardViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardListAsync([BindRequired, FromQuery] CardListQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet()]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardDetailAsync([BindRequired, FromQuery] CardDetailQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Balance")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardBalanceViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardBalanceAsync([BindRequired, FromQuery] CardBalanceQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Status")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardCurrentStatusViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCurrentStatusHistoryAsync([BindRequired, FromQuery] CardCurrentStatusQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Status/History")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardHistoryStatusViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetStatusHistoryAsync([BindRequired, FromQuery] CardHistoryStatusQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Balance/Extended")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardExtendedBalanceViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardExtendedBalanceAsync([BindRequired, FromQuery] CardExtendedBalanceQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Balance/Full")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardFullBalanceViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardFullBalanceAsync([BindRequired, FromQuery] CardFullBalanceQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Service/Sms-Info")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SmsInfoViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetStatusAsync([BindRequired, FromQuery] SmsInfoQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Service/Sms-Info/TurnOn")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SmsInfoChangeStatusViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> ChangeStatusTurnOnAsync([BindRequired, FromBody] SmsInfoChangeStatusTurnOnCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Service/Sms-Info/TurnOff")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SmsInfoChangeStatusViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> ChangeStatusTurnOffAsync([BindRequired, FromBody] SmsInfoChangeStatusTurnOffCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Service/3D-Secure")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(Secure3DInfoViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetStatusAsync([BindRequired, FromQuery] Secure3DInfoQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Service/3D-Secure/History")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(Secure3DStatusHistoryViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetSecure3DStatusHistoryAsync([BindRequired, FromQuery] Secure3DStatusHistoryQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Service/Sms-Info/History")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SmsInfoHistoryStatusViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetSmsInfoHistoryStatusAsync([BindRequired, FromQuery] SmsInfoHistoryStatusQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Tariff/Current")]
		//[ServiceFilter(typeof(GeneralValidateActionFilter))]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardTariffViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCurrentTariffAsync([BindRequired, FromQuery] CardTariffDetailQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Tariff/All")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<CardTariffViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllTariffAsync([BindRequired, FromQuery] CardTariffListQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Limit/Cash-Withdrawal")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> ChangeCardLimitOfCashWithdrawalAsync([BindRequired, FromBody] CardLimitOfCashWithdrawalCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Limit/Cash-Withdrawal")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardLimitOfCashWithdrawalViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardLimitOfCashWithdrawalAsync([BindRequired, FromQuery] CardLimitOfCashWithdrawalQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Limit/ChangeRisk")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CardLimitRiskChangeAsync([BindRequired, FromBody] CardLimitRiskChangeCommand command)
			=> await ExecuteQueryAsync(command);


		[HttpPost("Tariff/Send/Email")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SendEmailAsync([BindRequired, FromBody] CardTariffSendEmailCommand command)
			=> await ExecuteQueryAsync(command);

		[HttpPost("Tariff/Send/Viber")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SendViberAsync([BindRequired, FromBody] CardTariffSendViberCommand command)
			=> await ExecuteQueryAsync(command);

		[HttpPost("Blocking")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardBlockingResultViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> BlockCardAsync([BindRequired, FromBody] CardBlockingCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Tokens")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<CardTokenViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardTokenListAsync([BindRequired, FromQuery] CardTokenListQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Tokens/LastStatus")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardTokenLastStatusViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardTokenLastStatusAsync([BindRequired, FromQuery] CardTokenLastStatusQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Tokens/DeleteToken")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> TokenDeleteAsync([BindRequired, FromBody] CardTokenDeleteCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Tokens/ActivateToken")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> TokenActivateAsync([BindRequired, FromBody] CardTokenActivateCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Shirt/FrontUrl")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardImageUrlViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCardFrontImageUrlAsync([BindRequired, FromQuery] CardImageUrlQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Debt-to-bank")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CardDebtToBankViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetDebtToBankAsync([BindRequired, FromQuery] CardDebtToBankQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Unlocking/NotSuccess/Finish")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CardUnlockingNotSuccessAsync([BindRequired, FromBody] CardUnlockingFailedCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Unlocking/On")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CardUnlockingAsync([BindRequired, FromBody] CardUnlockingCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Unlocking/Off")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CardUnlockingLockAsync([BindRequired, FromBody] CardUnlockingLockCommand query)
			=> await ExecuteQueryAsync(query);
	}
}
