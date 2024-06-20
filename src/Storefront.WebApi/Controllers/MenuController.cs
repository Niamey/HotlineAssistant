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
using Vostok.Hotline.Storefront.Requests.Queries.Menu;
using Vostok.Hotline.Storefront.ViewModels.Menu;

namespace Vostok.Hotline.Storefront.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MenuController : MediatrControllerBase
	{
		public MenuController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpGet("Left")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<MenuItemViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetMenuLeftAsync([BindRequired, FromQuery] MenuLeftQuery query)
			=> await ExecuteQueryAsync(query);
	}
}
