using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Storefront.Requests.Queries.Menu;
using Vostok.Hotline.Storefront.Services.Menu;
using Vostok.Hotline.Storefront.ViewModels.Menu;

namespace Vostok.Hotline.Storefront.Requests.Handlers.Menu
{
	public class MenuLeftQueryHandler : IRequestHandler<MenuLeftQuery, SearchRowsResponseRowModel<MenuItemViewModel>>
	{
		private readonly MenuLeftService _menuLeftService;

		public MenuLeftQueryHandler(MenuLeftService menuLeftService)
		{
			_menuLeftService = menuLeftService;
		}

		public async Task<SearchRowsResponseRowModel<MenuItemViewModel>> Handle(MenuLeftQuery request, CancellationToken cancellationToken)
		{
			return await _menuLeftService.GetValueAsync(request.Localization.Value, cancellationToken);
		}
	}
}
