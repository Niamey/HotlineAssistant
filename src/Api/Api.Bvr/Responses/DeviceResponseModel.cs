using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.Bvr.Responses.Cards.Enums;

namespace Vostok.Hotline.Api.Bvr.Responses
{
	internal class DeviceResponseModel
	{
		public string PhoneNumber { get; set; }
		public string DeviceId { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public string ApplicationVersionCode { get; set; }
		public string DisplaySize { get; set; }
		public string Manufacturer { get; set; }
		public string Ios { get; set; }
		public string InstallId { get; set; }
		public string PushNotificationId { get; set; }
		public bool IsSystemModified { get; set; }
		public bool IsNFCAvailable { get; set; }
		public bool IsBiometryAvailable { get; set; }
		public bool IsScreenBlockOn { get; set; }
		public PushInfoBvrStatusEnum? IsPushNotificationsOn { get; set; }
		public string AndroidVersion { get; set; }
		public string Product { get; set; }
		public string Imei { get; set; }
		public string PaymentAppInstanceId { get; set; }
	}
}
