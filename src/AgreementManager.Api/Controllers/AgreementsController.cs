using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreements.Queries;
using Vostok.HotLineAssistant.Common.Response.Common;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Authorization;

namespace Vostok.HotLineAssistant.AgreementManager.Api.Controllers
{
	[Route("api/v{version:apiVersion}/client/[controller]")]
	[ApiVersion("1.0")]
	[ApiController]
	public class AgreementsController : MediatrController
    {
	    public AgreementsController(IServiceProvider provider) : base(provider)
		{
	    }

	    [HttpGet]
	    [ProducesResponseType((int)HttpStatusCode.OK)]
	    [ProducesResponseType((int)HttpStatusCode.NotFound)]
	    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[Produces(typeof(ModelsResponse<AgreementDto>))]
		public Task<IActionResult> GetByNumber([BindRequired, FromQuery] AgreementsByNumberQuery query) =>
		    ExecuteQuery(query, QueryAuthorizationSettings.None);

	    [HttpGet]
	    [Route("{Id:int}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
	    [ProducesResponseType((int)HttpStatusCode.NotFound)]
	    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[Produces(typeof(ModelsResponse<AgreementDto>))]
		public Task<IActionResult> GetById([BindRequired, FromRoute] AgreementsByIdQuery query) =>
		    ExecuteQuery(query, QueryAuthorizationSettings.None);

	    [HttpGet]
	    [Route("card")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
	    [ProducesResponseType((int)HttpStatusCode.NotFound)]
	    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[Produces(typeof(ModelsResponse<AgreementDto>))]
		public Task<IActionResult> GetByCard([BindRequired, FromQuery] AgreementsByCardQuery query) =>
		    ExecuteQuery(query, QueryAuthorizationSettings.None);

	    [HttpGet]
	    [Route("iBan")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
	    [ProducesResponseType((int)HttpStatusCode.NotFound)]
	    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[Produces(typeof(ModelsResponse<AgreementDto>))]
		public Task<IActionResult> GetByIBan([BindRequired, FromQuery] AgreementsByIbanQuery query) =>
		    ExecuteQuery(query, QueryAuthorizationSettings.None);
    }
}