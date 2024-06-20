using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;
using Vostok.Hotline.Api.CRM.Mapper;
using System.Collections.Generic;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class CounterpartContactMapperExtensions
	{
		public static CounterpartContactApiModel MapToContactApiModel(this CounterpartContactResponseModel response)
		{
			var result = new CounterpartContactApiModel
			{
				ContactAttributes = response.ContactAttributes,
				ContactType = response.ContactType?.ToContactTypeEnum(),
				CounterpartContactId = response.CounterpartContactId,
				Value = response.Value
			};

			return result;
		}

		public static List<CounterpartContactApiModel> MapToContactApiModel(this IEnumerable<CounterpartContactResponseModel> response)
		{
			var result = new List<CounterpartContactApiModel>();
			foreach (var row in response)
			{
				result.Add(row.MapToContactApiModel());
			}

			return result;
		} 
	}
}
