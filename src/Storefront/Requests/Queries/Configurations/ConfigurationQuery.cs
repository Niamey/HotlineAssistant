using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Storefront.ViewModels.Configurations;

namespace Vostok.Hotline.Storefront.Requests.Queries.Configurations
{
	public class ConfigurationQuery : IRequest<ConfigurationViewModel>
	{
		[Required]
		public LocalizationEnum? Localization { get; set; }
	}
}
