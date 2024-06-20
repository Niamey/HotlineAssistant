using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Bvr.Abstractions;
using Vostok.Hotline.Api.Bvr.Models;
using Vostok.Hotline.Api.Bvr.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Extensions;

namespace Vostok.Hotline.Api.Bvr.Managers
{
	public class DeviceApiManger
	{
		protected readonly ConcurrentDictionary<string, Task<DeviceApiModel>> deviceCache = new ConcurrentDictionary<string, Task<DeviceApiModel>>();
		private readonly IDeviceApiService _deviceApiService;

		public DeviceApiManger(IDeviceApiService deviceApiService)
		{
			_deviceApiService = deviceApiService;
		}

		public async Task<DeviceApiModel> GetActiveDeviceAsync(string phoneNumber, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(phoneNumber))
				return null;

			return await deviceCache.GetOrAddAsync(phoneNumber, async phoneNumber =>
			{
				return await _deviceApiService.GetActiveDeviceAsync(phoneNumber, cancellationToken);
			});
		}

		public async Task<PushInfoBvrStatusEnum?> GetPushInfoStatusAsync(string phoneNumber, CancellationToken cancellationToken)
		{
			var result = await GetActiveDeviceAsync(phoneNumber, cancellationToken);
			return result?.IsPushNotificationsOn;
		}

		public async Task<StatusCommandApiViewModel?> ResetPINDeviceAsync(string phoneNumber, CancellationToken cancellationToken)
		{
			return await _deviceApiService.ResetPINDeviceAsync(phoneNumber, cancellationToken);
		}
	}
}
