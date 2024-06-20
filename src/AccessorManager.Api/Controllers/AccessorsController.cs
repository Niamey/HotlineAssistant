using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Solar.Models.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.AccessorManager.Application.Accessors.Queries;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Authorization;

namespace Vostok.HotLineAssistant.AccessorManager.Api.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiController]
    public class AccessorsController : MediatrController
    {
	    public AccessorsController(IServiceProvider provider) : base(provider)
	    {
	    }
        
        [HttpGet]
        [Route("{Id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        [Produces(typeof(Accessor))]
        public Task<IActionResult> GetById([BindRequired, FromRoute] AccessorByIdQuery query) =>
	        ExecuteQuery(query, QueryAuthorizationSettings.None);

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces(typeof(Accessor))]
        public Task<IActionResult> GetByNumber([BindRequired, FromQuery] AccessorByNumberQuery query) =>
	        ExecuteQuery(query, QueryAuthorizationSettings.None);

        [HttpGet]
        [Route("card")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces(typeof(Accessor))]

        public Task<IActionResult> GetByCard([BindRequired, FromQuery] AccessorByCardQuery query) =>
	        ExecuteQuery(query, QueryAuthorizationSettings.None);

        [HttpGet]
        [Route("iBan")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces(typeof(Accessor))]

        public Task<IActionResult> GetByIBan([BindRequired, FromQuery] AccessorByIbanQuery query) =>
	        ExecuteQuery(query, QueryAuthorizationSettings.None);
    }
}