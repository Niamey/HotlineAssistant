using Vostok.Hotline.Api.Bvr.Models;
using Vostok.Hotline.Api.Bvr.Responses;

namespace Vostok.Hotline.Api.Bvr.Mapper
{
	internal static class DeviceMapperExtension
	{
		internal static DeviceApiModel ToDeviceApiModel(this DeviceResponseModel response)
		{
			var result = new DeviceApiModel
			{
				PhoneNumber = response.PhoneNumber,
				DeviceId = response.DeviceId,
				Brand = response.Brand,
				Model = response.Model,
				ApplicationVersionCode = response.ApplicationVersionCode,
				DisplaySize = response.DisplaySize,
				Manufacturer = response.Manufacturer,
				IOS = response.Ios,
				InstallId = response.InstallId,
				PushNotificationId = response.PushNotificationId,
				IsSystemModified = response.IsSystemModified,
				IsNFCAvailable = response.IsNFCAvailable,
				IsBiometryAvailable = response.IsBiometryAvailable,
				IsScreenBlockOn = response.IsScreenBlockOn,
				IsPushNotificationsOn = response.IsPushNotificationsOn,
				AndroidVersion = response.AndroidVersion,
				Product = response.Product,
				IMEI = response.Imei,
				PaymentAppInstanceId = response.PaymentAppInstanceId
			};

			return result;
		}
	}
}
