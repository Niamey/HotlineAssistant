using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.Swagger.Authorize.Razore.Models;

namespace Vostok.Hotline.Core.Common.Swagger.Authorize.Razore.Areas.Auth.Pages
{
	public class LoginModel : PageModel
	{
		private readonly HotlineEnvironment _env;
		private readonly Authorization.Services.LoginService _loginService;

		public LoginModel(HotlineEnvironment env, Authorization.Services.LoginService loginService)
		{
			_env = env;
			_loginService = loginService;
		}

		public string GetEnvironmentName => _env.EnvironmentName;

		[BindProperty]
		public UserModel Input { get; set; }

		public string Error { get; set; }

		public static void OnGet()
		{

		}

		public async Task<IActionResult> OnPostAsync()
		{
			await _loginService.SignInAsync(HttpContext, new Authorization.Services.SearchRequests.AuthorizationRequest
			{
				 Login = Input.Login,
				 Password = Input.Password
			}, HttpContext.RequestAborted);
			
			return Redirect("/");
		}
	}
}