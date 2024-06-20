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
using Vostok.Hotline.Storefront.Requests.Queries.SearchLinks;
using Vostok.Hotline.Storefront.ViewModels.SearchLinks;

namespace Vostok.Hotline.Storefront.WebApi.Controllers
{
	//[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceController : MediatrControllerBase
	{
		public ServiceController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpGet("List")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<SearchLinkViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAccountListAsync([BindRequired, FromQuery] SearchLinkListQuery query)
			=> await ExecuteQueryAsync(query);
	}
}
