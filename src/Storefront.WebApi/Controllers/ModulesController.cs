using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Base.Controllers;
using Vostok.Hotline.Core.Common.Models.Responses;
using Vostok.Hotline.Core.Common.Models.Validations;
using Vostok.Hotline.Storefront.Requests.Queries.Modules;
using Vostok.Hotline.Storefront.ViewModels.Modules;

namespace Vostok.Hotline.Storefront.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ModulesController : MediatrControllerBase
	{
		public ModulesController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpGet("PersonalData/Configuration")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<PersonalDataConfigurationViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetPersonalDataConfigurationAsync([BindRequired, FromQuery] PersonalDataConfigurationQuery query)
			=> await ExecuteQueryAsync(query);
	}
}
