using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Storefront.ViewModels.Modules;

namespace Vostok.Hotline.Storefront.Requests.Queries.Modules
{
	public class PersonalDataConfigurationQuery: IRequest<SearchRowsResponseRowModel<PersonalDataConfigurationViewModel>>
	{
		[Required]
		public LocalizationEnum? Localization { get; set; }
	}
}
