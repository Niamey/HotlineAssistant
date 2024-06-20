using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Base.Controllers;
using Vostok.Hotline.Core.Common.Models.Responses;
using Vostok.Hotline.Core.Common.Models.Validations;
using Vostok.Hotline.Gateway.Vicidial.Requests.Commands;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AreonController : MediatrControllerBase
	{
		public AreonController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpPost()]
		[AllowAnonymous]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(AreonNewCallResponseViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> OpenNewCallAsync([BindRequired, FromBody] AreonNewCallCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("Detail")]
		[AllowAnonymous]
		//[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(AreonNewCallResponseViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> OpenNewDetailCallAsync([BindRequired, FromBody] AreonNewDetailCallCommand query)
			=> await ExecuteQueryAsync(query);
	}
}
