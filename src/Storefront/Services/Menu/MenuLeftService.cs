using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.Enums.UI;
using Vostok.Hotline.Storefront.ViewModels.Menu;

namespace Vostok.Hotline.Storefront.Services.Menu
{
	public class MenuLeftService
	{
		public MenuLeftService()
		{
		}

		public async Task<SearchRowsResponseRowModel<MenuItemViewModel>> GetValueAsync(LocalizationEnum value, CancellationToken cancellationToken)
		{
			return new SearchRowsResponseRowModel<MenuItemViewModel>
			{
				Rows = new System.Collections.Generic.List<MenuItemViewModel>
				{
					new MenuItemViewModel{ Id= 998, Align = MenuAlignEnum.LeftTop, ModuleName= "Завдання до виконання", Url= "#", IconUrl= "fact_check", DefaultModuleImage= "" },
					new MenuItemViewModel{ Id= 999, Align = MenuAlignEnum.LeftTop, ModuleName= "Навчання і тести", Url= "#", IconUrl= "school", DefaultModuleImage= "" },
					new MenuItemViewModel{ Id= 1000, Align = MenuAlignEnum.LeftTop, ModuleName= "Дашборд", Url= "#", IconUrl= "dashboard", DefaultModuleImage= "" },
					new MenuItemViewModel{ Id= 1001, Align = MenuAlignEnum.LeftTop, ModuleName= "Посилання на ресурси", Url= "#", IconUrl= "link", DefaultModuleImage= "" },
					new MenuItemViewModel{ Id= 1002, Align = MenuAlignEnum.LeftBottom, ModuleName= "Vicidial", Url= "#", IconUrl= "phone", DefaultModuleImage= "" },
					new MenuItemViewModel{ Id= 1003, Align = MenuAlignEnum.LeftBottom, ModuleName= "Чатбот", Url= "#", IconUrl= "chat", DefaultModuleImage= "" },
					new MenuItemViewModel{ Id= 1004, Align = MenuAlignEnum.LeftBottom, ModuleName= "База знань", Url= "#", IconUrl= "menu_book", DefaultModuleImage= "" }
				}
			};
		}
	}
}