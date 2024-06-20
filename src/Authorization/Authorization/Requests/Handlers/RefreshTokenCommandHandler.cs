using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Authorization.Requests.Commands;
using Vostok.Hotline.Authorization.Services;
using Vostok.Hotline.Authorization.ViewModels;

namespace Vostok.Hotline.Authorization.Requests.Handlers
{
	public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticationViewModel>
	{
		private readonly LoginService _loginService;

		public RefreshTokenCommandHandler(LoginService loginService)
		{
			_loginService = loginService;
		}

		public async Task<AuthenticationViewModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			return await _loginService.RefreshUserTokenAsync(cancellationToken);
		}
	}
}
