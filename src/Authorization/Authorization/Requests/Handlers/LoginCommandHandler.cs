using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Authorization.Requests.Commands;
using Vostok.Hotline.Authorization.Services;
using Vostok.Hotline.Authorization.Services.SearchRequests;
using Vostok.Hotline.Authorization.ViewModels;

namespace Vostok.Hotline.Authorization.Requests.Handlers
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationViewModel>
	{
		private readonly LoginService _loginService;

		public LoginCommandHandler(LoginService loginService)
		{
			_loginService = loginService;
		}

		public async Task<AuthenticationViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var loginRequest = new AuthorizationRequest
			{
				 Login = request.Login,
				 Password = request.Password
			};

			return await _loginService.AuthenticateUserAsync(loginRequest, cancellationToken);
		}
	}
}
