using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Controllers;
using Vostok.Hotline.Core.Common.Models.Responses;
using Vostok.Hotline.Core.Common.Models.Validations;
using Vostok.Hotline.Storefront.Requests.Queries.Configurations;
using Vostok.Hotline.Storefront.ViewModels.Configurations;

namespace Vostok.Hotline.Storefront.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DomainController : MediatrControllerBase
	{
		public DomainController(IServiceProvider provider)
			: base(provider)
		{
		}

		[AllowAnonymous]
		[HttpGet("Configuration")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(ConfigurationViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetConfigurationAsync([BindRequired, FromQuery] ConfigurationQuery query)
			=> await ExecuteQueryAsync(query);
	}
}
