using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Exceptions;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.ApiServices
{
	internal class MessengerApiService : IMessengerApiService
	{
		private readonly IEnumerable<IMessengerProvider> _providers;

		internal MessengerApiService(IServiceProvider serviceProvider)
		{
			_providers = serviceProvider.GetRequiredService<IEnumerable<IMessengerProvider>>();
		}

		protected IMessengerProvider GetProvider(ProviderTypeEnum providerType)
		{ 
			return _providers.FirstOrDefault(item => item.ProviderType == providerType);
		}

		public async Task<SendingStatusApiModel> SendMessageAsync<TResult>(IMessagePackageApiModel<TResult> msg, CancellationToken cancellationToken)
		{
			if (Validate(msg))
			{
				var result = await SendAsync(msg, cancellationToken);
				return result;
			}
			throw new MessengerApiValidateException();
		}

		public async Task<SendingStatusApiModel> GetStatusAsync(MessageStatusRequestApiModel statusRequest, CancellationToken cancellationToken)
		{
			var provider = GetProvider(statusRequest.ProviderType);
			var result = await provider.GetStatusAsync(statusRequest, cancellationToken);
			return result;
		}

		protected async Task<SendingStatusApiModel> SendAsync<TResult>(IMessagePackageApiModel<TResult> msg, CancellationToken cancellationToken) 
		{
			var provider = GetProvider(msg.ProviderType);
			return await provider.SendAsync(msg, cancellationToken);
		}

		protected bool Validate<TResult>(IMessagePackageApiModel<TResult> msg)
		{
			var results = new List<ValidationResult>();
			var context = new ValidationContext(msg);
			if (!Validator.TryValidateObject(msg, context, results, true))
			{
				StringBuilder errors = new StringBuilder();
				foreach (var error in results)
				{
					errors.AppendLine(error.ErrorMessage);
				}
				throw new MessengerApiValidateException(errors.ToString());
			}
			return true;
		}
	}
}
