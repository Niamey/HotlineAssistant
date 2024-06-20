using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Authorization.FzBank.Mappers;
using Vostok.Hotline.Authorization.FzBank.Models;
using Vostok.Hotline.Authorization.FzBank.Requests;
using Vostok.Hotline.Authorization.FzBank.Responses;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Core.Common.Security;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Authorization.FzBank.ApiService
{
	public class FzBankAuthorizationApiService
	{
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;
		private readonly HotlineEnvironment _hotlineEnvironment;

		public FzBankAuthorizationApiService(HotlineHttpClient httpClient, EnvironmentManager environmentManager, HotlineEnvironment hotlineEnvironment)
		{
			_httpClient = httpClient;
			_environmentManager = environmentManager;
			_hotlineEnvironment = hotlineEnvironment;
		}

		public async Task<FzBankAuthorizationApiModel> AuthorizationAsync(string login, string password, CancellationToken cancellationToken)
		{
			if (!_hotlineEnvironment.IsLive
				&& login == "autotest"
				&& password == "autotest_password")
			{
				return new FzBankAuthorizationApiModel
				{
					StatusCode = StatusCodes.Status200OK,
					Data = new FzBankAuthorizationDataApiModel
					{
						FIO = "Autotest Full Name",
						Login = "autotest",
						Position = "Тестер",
						UserSessionId = Guid.NewGuid()
					}
				};
			}

			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.FzBankAuthorization.LoginUrl, cancellationToken);
			var uri = new Uri($"{host}");			

			var loginRequest = new FzBankLoginRequest()
			{
				Login = login,
				Password = password
			};

			var json = JsonHelper.ToJson(loginRequest);
			var headers = new Core.Common.Models.HttpClientModels.HttpClientHeaderCollectionModel();
			headers.Add("modulename", "HotlineAssistant");

			var response = await _httpClient.PostTryAsync(uri, json, headers, cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				var erroResponse = JsonHelper.FromJson<AuthModel>(response?.Response);
				return new FzBankAuthorizationApiModel
				{
					StatusCode = (int)response.StatusCode,
					ErrorMessage = erroResponse.ErrorString
				};
			}

			var result = JsonHelper.FromJson<AuthSuccessResponseModel>(response?.Response);
			return result?.ToFzBankAuthorization();
		}

		public async Task<FzBankAuthorizationApiModel> VerifyAsync(UserSession userSession, CancellationToken cancellationToken)
		{
			if (!_hotlineEnvironment.IsLive
				&& userSession?.CurrentUser?.Login == "autotest")
			{
				return new FzBankAuthorizationApiModel
				{
					StatusCode = StatusCodes.Status200OK,
					Data = new FzBankAuthorizationDataApiModel
					{
						FIO = "Autotest Full Name",
						Login = "autotest",
						Position = "Тестер",
						UserSessionId = userSession.UserSessionId
					}
				};
			}

			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.FzBankAuthorization.VerifyUrl, cancellationToken);
			var query = new FzBankVerifyRequest(userSession.UserSessionId).GetUrlQuery();

			var uri = new Uri($"{host}?{query}");

			var headers = new Core.Common.Models.HttpClientModels.HttpClientHeaderCollectionModel();
			headers.Add("modulename", "HotlineAssistant");

			var response = await _httpClient.GetTryAsync(uri, headers, cancellationToken);
			if (!response.IsSuccessStatusCode)
			{
				var erroResponse = JsonHelper.FromJson<AuthModel>(response?.Response);
				return new FzBankAuthorizationApiModel
				{
					StatusCode = (int)response.StatusCode,
					ErrorMessage = erroResponse.ErrorString
				};
			}

			var result = JsonHelper.FromJson<AuthSuccessResponseModel>(response?.Response);
			if (result?.ErrorCode == 0)
			{
				return new FzBankAuthorizationApiModel
				{
					Data = new FzBankAuthorizationDataApiModel
					{
						Login = userSession.CurrentUser.Login,
						UserSessionId = userSession.UserSessionId
					},
					StatusCode = StatusCodes.Status200OK
				};
			}

			return result?.ToFzBankAuthorization();
		}

		public async Task<bool> LogoutAsync(UserSession userSession, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.FzBankAuthorization.LogoffUrl, cancellationToken);
			var uri = new Uri($"{host}");

			var headers = new Core.Common.Models.HttpClientModels.HttpClientHeaderCollectionModel();
			headers.Add("modulename", "HotlineAssistant");

			var stringVal = $"\"{userSession.UserSessionId}\"";

			var response = await _httpClient.PostTryAsync(uri, stringVal, headers, cancellationToken);
			if (!response.IsSuccessStatusCode)
				return false;

			var result = JsonHelper.FromJson<AuthSuccessResponseModel>(response?.Response);
			if (result?.ErrorCode == 0)
				return true;
			else
				return false;
		}
	}
}