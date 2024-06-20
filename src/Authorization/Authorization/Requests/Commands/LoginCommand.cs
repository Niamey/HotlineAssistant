using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Authorization.ViewModels;

namespace Vostok.Hotline.Authorization.Requests.Commands
{
	public class LoginCommand : IRequest<AuthenticationViewModel>
	{
		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
