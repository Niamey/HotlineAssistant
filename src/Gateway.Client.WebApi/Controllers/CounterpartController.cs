using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Base.Controllers;
using Vostok.Hotline.Core.Common.Models.Responses;
using Vostok.Hotline.Core.Common.Models.Validations;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CounterpartController : MediatrControllerBase
	{
		public CounterpartController(IServiceProvider provider) 
			: base(provider)
		{
		}

		[HttpGet("Search")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchPagedResponseRowModel<ClientViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetClientSearchAsync([BindRequired, FromQuery] ClientSearchQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Count")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchPagedResponseRowModel<ClientCountViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetClientCountAsync([BindRequired, FromQuery] ClientCountQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet()]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(ClientViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseNotFoundModel>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetClientDetailAsync([BindRequired, FromQuery] ClientDetailQuery query)
			=> await ExecuteQueryAsync(query);
	}
}
