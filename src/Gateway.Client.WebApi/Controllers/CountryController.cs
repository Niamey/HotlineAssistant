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
using Vostok.Hotline.Gateway.Client.Countries.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Countries.ViewModels;

namespace Vostok.Hotline.Gateway.Client.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : MediatrControllerBase
	{
		public CountryController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpGet("List")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<CountryViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCountryListAsync([FromQuery] CountryListQuery query)
			=> await ExecuteQueryAsync(query);

	}
}
