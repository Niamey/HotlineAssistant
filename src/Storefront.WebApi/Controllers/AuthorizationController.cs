using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Authorization.Requests.Commands;
using Vostok.Hotline.Authorization.ViewModels;
using Vostok.Hotline.Core.Common.Base.Controllers;
using Vostok.Hotline.Core.Common.Models.Responses;
using Vostok.Hotline.Core.Common.Models.Validations;

namespace Vostok.Hotline.Storefront.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationController : MediatrControllerBase
	{
		public AuthorizationController(IServiceProvider provider)
			: base(provider)
		{
		}

		[AllowAnonymous]
		[HttpPost("Login")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(AuthenticationViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<AuthorizationFailedModel>), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> LoginAsync([BindRequired, FromBody] LoginCommand query)
			=> await ExecuteQueryAsync(query);

		[HttpPost("RefreshToken")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(AuthenticationViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<AuthorizationFailedModel>), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> RefreshTokenAsync()
			=> await ExecuteQueryAsync(new RefreshTokenCommand());
		
		[AllowAnonymous]
		[HttpPost("Logout")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<AuthorizationFailedModel>), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task LogoutAsync()
			=> await ExecuteQueryAsync(new LogoutCommand());
	}
}
