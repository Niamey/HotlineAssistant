using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class CounterpartCountMapperExtensions
	{
		public static CounterpartCountApiModel ToCounterpartCountApiModel(this CounterpartResponseRowCountModel response)
		{
			return new CounterpartCountApiModel
			{
				 TotalRows = response.TotalRows
			};
		} 
	}
}