using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Controllers;
using Vostok.Hotline.Core.Common.Models.Responses;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Core.Common.Models.Validations;
using Vostok.Hotline.Storefront.Requests.Commands;

namespace Vostok.Hotline.Storefront.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedBackController : MediatrControllerBase
	{
		public FeedBackController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpPost("Send")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SendAsync([BindRequired, FromBody] SendFeedBackCommand command)
			=> await ExecuteQueryAsync(command);

		[HttpPost("Report/Exceptions")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> ReportExceptionsAsync([BindRequired, FromBody] ReportExceptionsCommand command)
			=> await ExecuteQueryAsync(command);
	}
}
