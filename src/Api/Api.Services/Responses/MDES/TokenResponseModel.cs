using System;
using Vostok.Hotline.Api.Services.Responses.MDES.Enums;

namespace Vostok.Hotline.Api.Services.Responses.MDES
{
	public class TokenResponseModel
	{
		public string TokenUniqueReference { get; set; }
		public TokenStatusNameServiceEnum TokenStatusName { get; set; }
		public DateTime THdateTime { get; set; }
		public string DeviceName { get; set; }
		public string DeviceType { get; set; }
		public string Wallet { get; set; }
		public string RequestorName { get; set; }
		public string StorageTechnology { get; set; }
	}
}
