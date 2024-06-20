using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Authorization.FzBank.ApiService;
using Vostok.Hotline.Authorization.FzBank.Models;
using Vostok.Hotline.Authorization.Mappers;
using Vostok.Hotline.Authorization.Security;
using Vostok.Hotline.Authorization.Services.SearchRequests;
using Vostok.Hotline.Authorization.ViewModels;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Security;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Authorization.Services
{
	public class LoginService
	{
		private readonly JwtTokenService _jwtTokenService;
		private readonly FzBankAuthorizationMapper _fzMapper;
		private readonly FzBankAuthorizationApiService _fzBankAuthorizationApiService;
		private readonly UserSession _userSession;
		private readonly UserProfileManager _userProfileManager;

		public LoginService(JwtTokenService jwtTokenServic, 
			FzBankAuthorizationMapper fzMapper, 
			FzBankAuthorizationApiService fzBankAuthorizationApiService,
			UserSession userSession,
			UserProfileManager userProfileManager)
		{
			_jwtTokenService = jwtTokenServic;
			_fzMapper = fzMapper;
			_fzBankAuthorizationApiService = fzBankAuthorizationApiService;
			_userSession = userSession;
			_userProfileManager = userProfileManager;
		}

		public async Task<AuthenticationViewModel> AuthenticateUserAsync(AuthorizationRequest loginRequest, CancellationToken cancellationToken)
		{
			FzBankAuthorizationApiModel authorizationResponse = await AuthenticateAsync(loginRequest, cancellationToken);

			var userProfile = _fzMapper.ToUserProfile(authorizationResponse.Data);
			var claims = userProfile.ToClaim(authorizationResponse.Data.UserSessionId);

			var (jwtToken, expiresAt) = _jwtTokenService.GenerateToken(claims);

			return new AuthenticationViewModel
			{
				Token = jwtToken,
				TokenExpiresAt = expiresAt,
				UserProfile = userProfile.ToUserProfileViewModel()
			};
		}

		public async Task<bool> SignInAsync(HttpContext context, AuthorizationRequest loginRequest, CancellationToken cancellationToken)
		{
			FzBankAuthorizationApiModel authorizationResponse = await AuthenticateAsync(loginRequest, cancellationToken);

			var userProfile = _fzMapper.ToUserProfile(authorizationResponse.Data);
			var claims = userProfile.ToClaim(authorizationResponse.Data.UserSessionId);

			await context.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
				new AuthenticationProperties());

			return true;
		}

		protected async Task<FzBankAuthorizationApiModel> AuthenticateAsync(AuthorizationRequest loginRequest, CancellationToken cancellationToken)
		{
			var authorizationResponse = await _fzBankAuthorizationApiService.AuthorizationAsync(loginRequest.Login, loginRequest.Password, cancellationToken);
			if (authorizationResponse.HasError)
				throw new AuthorizationRequestException(authorizationResponse.ErrorMessage);

			var userProfile = _fzMapper.ToUserProfile(authorizationResponse.Data);

			await _userProfileManager.LoginAsync(authorizationResponse.AuthorizationState, authorizationResponse.Data.UserSessionId, userProfile, cancellationToken);

			return authorizationResponse;
		}

		public async Task LogoutUserAsync(CancellationToken cancellationToken)
		{
			if (_userSession != null)
			{
				await _fzBankAuthorizationApiService.LogoutAsync(_userSession, cancellationToken);
				await _userProfileManager.LogoutAsync(_userSession.UserSessionId, cancellationToken);
			}
		}

		public async Task<AuthenticationViewModel> RefreshUserTokenAsync(CancellationToken cancellationToken)
		{
			if(_userSession == null)
				throw new SessionExpiredRequestException();

			var authorizationResponse = await _fzBankAuthorizationApiService.VerifyAsync(_userSession, cancellationToken);
			if (authorizationResponse.HasError)
				throw new SessionExpiredRequestException(authorizationResponse.ErrorMessage);

			var userProfile = _fzMapper.ToUserProfile(authorizationResponse.Data);

			var userPofileViewModel = await _userProfileManager.VerifyLoginAsync(authorizationResponse.Data.UserSessionId, userProfile.Login, cancellationToken);

			userProfile = userPofileViewModel.ToUserProfileModel(); 

			var claims = userProfile.ToClaim(authorizationResponse.Data.UserSessionId);

			var (jwtToken, expiresAt) = _jwtTokenService.GenerateToken(claims);

			return new AuthenticationViewModel
			{
				Token = jwtToken,
				TokenExpiresAt = expiresAt,
				UserProfile = userProfile.ToUserProfileViewModel()
			};
		}
	}
}
