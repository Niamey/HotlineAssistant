using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Storefront.ViewModels.SearchLinks;

namespace Vostok.Hotline.Storefront.Services.SearchLinks
{
	public class SearchLinkService
	{
		public async Task<SearchRowsResponseRowModel<SearchLinkViewModel>> GetListAsync(int count, CancellationToken cancellationToken)
		{
			var result = new SearchRowsResponseRowModel<SearchLinkViewModel>
			{
				Rows = new System.Collections.Generic.List<SearchLinkViewModel> 
				{
						new SearchLinkViewModel
						{
							Id = 1,
							Title = "Доступна нова інструкція",
							IsCustomIcon= false,
							Description= "",
							ImageUrl= "fact_check",
							Url= "#"
						},
						new SearchLinkViewModel
						{
							Id= 2, Title= "Пройти тестування", IsCustomIcon= false, Description= "", ImageUrl= "school", Url= "#"
						},
						new SearchLinkViewModel
						{
							Id= 3, Title= "Сайт ПАТ «Банк Восток»", IsCustomIcon= true, Description= "", ImageUrl= "assets/images/icons/vostok.svg", Url= "https=//bankvostok.com.ua"
						},
						new SearchLinkViewModel
						{
							Id= 4, Title= "База знань", IsCustomIcon= true, Description= "", ImageUrl= "assets/images/icons/vostok.svg", Url= "#"
						}
				}.Take(count).ToList()
			};

			return result;
		}
	}
}