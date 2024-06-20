using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Queries;
using Vostok.HotLineAssistant.Common.Response.Common;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Authorization;

namespace Vostok.HotLineAssistant.AgreementManager.Api.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiController]
	//[Authorize]
	public class AgreementController : MediatrController
	{
		public AgreementController(IServiceProvider provider) : base(provider)
		{
		}

		[HttpGet]
		[Route("{Id:int}")]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		[ProducesResponseType((int) HttpStatusCode.BadRequest)]
		[Produces(typeof(ModelResponse<AgreementDto>))]
		public Task<IActionResult> GetById([BindRequired, FromRoute] AgreementByIdQuery query) =>
			ExecuteQuery(query, QueryAuthorizationSettings.None);

		[HttpGet]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		[ProducesResponseType((int) HttpStatusCode.BadRequest)]
		[Produces(typeof(ModelResponse<AgreementDto>))]
		public Task<IActionResult> GetByNumber([BindRequired, FromQuery] AgreementByNumberQuery query) =>
			ExecuteQuery(query, QueryAuthorizationSettings.None);

		[HttpGet]
		[Route("card")]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		[ProducesResponseType((int) HttpStatusCode.BadRequest)]
		[Produces(typeof(ModelResponse<AgreementDto>))]

		public Task<IActionResult> GetByCard([BindRequired, FromQuery] AgreementByCardQuery query) =>
			ExecuteQuery(query, QueryAuthorizationSettings.None);

		[HttpGet]
		[Route("iBan")]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		[ProducesResponseType((int) HttpStatusCode.BadRequest)]
		[Produces(typeof(ModelResponse<AgreementDto>))]

		public Task<IActionResult> GetByIBan([BindRequired, FromQuery] AgreementByIbanQuery query) =>
			ExecuteQuery(query, QueryAuthorizationSettings.None);
	}
}