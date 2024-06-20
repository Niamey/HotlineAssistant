using System;
using System.Collections.Generic;
using System.Linq;
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
using Vostok.Hotline.Gateway.Client.Statements.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Statements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatementController : MediatrControllerBase
	{
		public StatementController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpGet()]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetStatementAsync([BindRequired, FromQuery] StatementQuery query)
		{
			var res = await ExecuteQueryModelAsync(query);
			return File(res.Data, "text/html", "statement.html");
		}

		[HttpPost("Send/Email")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(StatusCommandViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SendEmailStatementAsync([BindRequired, FromBody] StatementSendEmailCommand query)
			=> await ExecuteQueryAsync(query);

	}
}
