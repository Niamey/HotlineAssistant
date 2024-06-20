using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.UI;

namespace Vostok.Hotline.Storefront.ViewModels.Menu
{
	public class MenuItemViewModel : ResponseBaseModel
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public string IconUrl { get; set; }
		public string DefaultModuleImage { get; set; }
		public string ModuleName { get; set; }
		public MenuAlignEnum Align { get; set; }
	}
}