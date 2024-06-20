using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Storefront.ViewModels.Menu;

namespace Vostok.Hotline.Storefront.Requests.Queries.Menu
{
	public class MenuLeftQuery : IRequest<SearchRowsResponseRowModel<MenuItemViewModel>>
	{
		[Required]
		public LocalizationEnum? Localization { get; set; }
	}
}
