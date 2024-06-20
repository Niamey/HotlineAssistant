using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.CardManager.Application.Cards.Queries;
using Vostok.HotLineAssistant.WebApi;
using Vostok.HotLineAssistant.WebApi.Authorization;

namespace Vostok.HotLineAssistant.CardManager.Api.Controllers
{
	//[Authorize]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiController]

	public class CardsController : MediatrController
	{
		public CardsController(IServiceProvider provider) : base(provider)
		{
		}

		[HttpGet]
		[Route("detail")]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		[ProducesResponseType((int) HttpStatusCode.BadRequest)]
		public Task<IActionResult> GetDetail([BindRequired, FromRoute] CardInfoByIdQuery query) =>
			ExecuteQuery(query, QueryAuthorizationSettings.None);
	}
}