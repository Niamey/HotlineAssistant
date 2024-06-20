using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Models.MDES
{
	public class TokenApiModel: ResponseBaseModel
	{
		public string TokenUniqueReference { get; set; }
		public TokenStatusNameMdesEnum TokenStatusName { get; set; }
		public DateTime THdateTime { get; set; }
		public string DeviceName { get; set; }
		public string DeviceType { get; set; }
		public string Wallet { get; set; }
		public string RequestorName { get; set; }
		public string StorageTechnology { get; set; }
	}
}
