using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Authorization.FzBank.ApiService;
using Vostok.Hotline.Authorization.Mappers;
using Vostok.Hotline.Authorization.Requests.Commands;
using Vostok.Hotline.Authorization.Requests.Handlers;
using Vostok.Hotline.Authorization.Requests.Validations;
using Vostok.Hotline.Authorization.Security;
using Vostok.Hotline.Authorization.Services;
using Vostok.Hotline.Authorization.ViewModels;
using Vostok.Hotline.Data.Repository.Core.Bootstrappers;

namespace Vostok.Hotline.Authorization.Bootstrappers
{
	public static class AuthorizationBootstrapperExtension
	{
		public static void AddAuthorizationRules(this IServiceCollection services)
		{
			services.AddUserProfileSettingRules();

			services.AddTransient<FzBankAuthorizationApiService>();
			services.AddTransient<FzBankAuthorizationMapper>();
			services.AddTransient<JwtTokenService>();
			services.AddTransient<LoginService>();
			services.AddTransient<UserSessionService>();

			services.AddTransient<IValidator<LoginCommand>, LoginCommandValidator>();

			services.AddTransient<IRequestHandler<LogoutCommand, Unit>, LogoutCommanHandler>();
			services.AddTransient<IRequestHandler<LoginCommand, AuthenticationViewModel>, LoginCommandHandler>();
			services.AddTransient<IRequestHandler<RefreshTokenCommand, AuthenticationViewModel>, RefreshTokenCommandHandler>();
		}
	}
}
