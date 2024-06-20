using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Mapper
{
	public static class ClientCountMapperExtensions
	{
		public static ClientCountViewModel ToClientCountViewModel(this CounterpartCountApiModel response)
		{
			return new ClientCountViewModel
			{
				 TotalRows = response.TotalRows
			};
		} 
	}
}