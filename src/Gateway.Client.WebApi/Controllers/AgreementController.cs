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
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;
using Vostok.Hotline.Gateway.Client.Classifiers.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Classifiers.ViewModels;

namespace Vostok.Hotline.Gateway.Client.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AgreementController : MediatrControllerBase
	{
		public AgreementController(IServiceProvider provider) 
			: base(provider)
		{
		}

		[HttpGet("List")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<AgreementViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAgreementListAsync([BindRequired, FromQuery] AgreementListQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet()]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(AgreementViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAgreementDetailAsync([BindRequired, FromQuery] AgreementDetailQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Status/History")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(AgreementHistoryStatusViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetStatusHistoryAsync([BindRequired, FromQuery] AgreementHistoryStatusQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Tariff/Current")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(AgreementTariffViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCurrentTariffAsync([BindRequired, FromQuery] AgreementTariffDetailQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Tariff/All")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<AgreementTariffViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllTariffAsync([BindRequired, FromQuery] AgreementTariffListQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Tariff/Send/Email")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SendEmailAsync([BindRequired, FromBody] AgreementTariffSendEmailCommand command)
			=> await ExecuteQueryAsync(command);

		[HttpPost("Tariff/Send/Viber")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SendViberAsync([BindRequired, FromBody] AgreementTariffSendViberCommand command)
			=> await ExecuteQueryAsync(command);

		[HttpGet("Balance")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(AgreementBalanceViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetBalanceAsync([BindRequired, FromQuery] AgreementBalanceQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Classifiers")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<ClassifierViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetClassifiersAsync([BindRequired, FromQuery] AgreementClassifierListQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("MoneyBox/List")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<AgreementMoneyBoxViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetMoneyBoxesAsync([BindRequired, FromQuery] AgreementMoneyBoxListQuery query)
			=> await ExecuteQueryAsync(query);
	}
}
