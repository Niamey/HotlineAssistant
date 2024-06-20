using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Bvr.Models;

namespace Vostok.Hotline.Api.Bvr.Abstractions
{
	public interface IDeviceApiService
	{
		Task<DeviceApiModel> GetActiveDeviceAsync(string phoneNumber, CancellationToken cancellationToken);
		Task<StatusCommandApiViewModel> ResetPINDeviceAsync(string phoneNumber, CancellationToken cancellationToken);
	}
}
