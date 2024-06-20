using MediatR;
using Vostok.Hotline.Authorization.ViewModels;

namespace Vostok.Hotline.Authorization.Requests.Commands
{
	public class RefreshTokenCommand : IRequest<AuthenticationViewModel>
	{
	}
}
