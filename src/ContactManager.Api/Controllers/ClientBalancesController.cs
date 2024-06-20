using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solar.Models.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.ContactManager.Application.ClientBalances.Queries;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Authorization;

namespace Vostok.HotLineAssistant.ContactManager.Api.Controllers
{
	[Route("api/v{version:apiVersion}/client/balances")]
	[ApiVersion("1.0")]
	[ApiController]
    public class ClientBalancesController : MediatrController
    {
	    public ClientBalancesController(IMediator mediator, IAuthorizationService authorizationService) : base(mediator, authorizationService)
	    {
	    }

	    [HttpGet]
	    [Route("{Id:int}")]
	    [ProducesResponseType((int) HttpStatusCode.OK)]
	    [ProducesResponseType((int)HttpStatusCode.NotFound)]
		[Produces(typeof(IEnumerable<Balance>))]
		public Task<IActionResult> GetBalance([FromRoute] ClientBalancesByIdQuery query) =>
		    ExecuteQuery(query, QueryAuthorizationSettings.None);

	    [HttpGet]
	    [Route("balance")]
	    [ProducesResponseType((int) HttpStatusCode.OK)]
	    [ProducesResponseType((int)HttpStatusCode.NotFound)]
	    [Produces(typeof(IEnumerable<Balance>))]
		public Task<IActionResult> GetByNumber([FromQuery] ClientBalancesByNumberQuery query) =>
		    ExecuteQuery(query, QueryAuthorizationSettings.None);

	    [HttpGet]
	    [Route("card")]
	    [ProducesResponseType((int) HttpStatusCode.OK)]
	    [ProducesResponseType((int)HttpStatusCode.NotFound)]
	    [Produces(typeof(IEnumerable<Balance>))]
		public Task<IActionResult> GetByCardNumber([FromQuery] ClientBalancesByCardQuery query) =>
		    ExecuteQuery(query, QueryAuthorizationSettings.None);

	    [HttpGet]
	    [Route("Iban")]
	    [ProducesResponseType((int) HttpStatusCode.OK)]
	    [ProducesResponseType((int)HttpStatusCode.NotFound)]
	    [Produces(typeof(IEnumerable<Balance>))]
		public Task<IActionResult> GetByIBan([FromQuery] ClientBalancesByIbanQuery query) =>
		    ExecuteQuery(query, QueryAuthorizationSettings.None);
	}
}