using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class CounterpartTypeAttributeMapperExtensions
	{
		public static CounterpartTypeApiModel MapToCounterpartTypeApiModel(this CounterpartTypeResponseModel response)
		{
			var result = new CounterpartTypeApiModel
			{
				CounterpartTypeId = response.CounterpartTypeId,
				Description = response.Description,
				Name = response.Name
			};

			return result;
		}
	}
}