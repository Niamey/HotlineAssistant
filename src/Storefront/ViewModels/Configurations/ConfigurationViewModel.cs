using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Storefront.ViewModels.Configurations
{
	public class ConfigurationViewModel : ResponseBaseModel
	{
		public SettingViewModel Settings { get; set; }
		public Version VersionId { get; set; }
	}
}
