using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Authorization.Requests.Commands;
using Vostok.Hotline.Authorization.Services;

namespace Vostok.Hotline.Authorization.Requests.Handlers
{
	public class LogoutCommanHandler : IRequestHandler<LogoutCommand>
	{
		private readonly LoginService _loginService;

		public LogoutCommanHandler(LoginService loginService)
		{
			_loginService = loginService;
		}

		public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
		{
			await _loginService.LogoutUserAsync(cancellationToken);
			return Unit.Value;
		}
	}
}
