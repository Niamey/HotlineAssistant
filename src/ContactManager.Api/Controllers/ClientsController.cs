using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.ContactManager.Application.Client;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Queries;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Authorization;

namespace Vostok.HotLineAssistant.ContactManager.Api.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiController]
	public class ClientsController : MediatrController
	{
		public ClientsController(IServiceProvider provider) : base(provider)
		{
		}

		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[Produces(typeof(PagedResponseEx<ClientDto>))]
		public Task<IActionResult> GetClient([BindRequired, FromQuery] ClientSearchQuery query) =>
			ExecuteQuery(query, QueryAuthorizationSettings.None);
	}
}
