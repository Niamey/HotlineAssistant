using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;
using System.Collections.Generic;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class CounterpartDocumentMapperExtensions
	{
		public static CounterpartDocumentApiModel MapToCounterpartDocumentApiModel(this CounterpartDocumentResponseModel response)
		{
			var result = new CounterpartDocumentApiModel
			{
				ClosingDate = response.ClosingDate,
				CounterpartDocumentId = response.CounterpartDocumentId,
				CountryId = response.CountryId,
				DocumentType = response.DocumentType.MapToDocumentTypeApiModel(),
				ExpirationDate = response.ExpirationDate,
				IsMain = response.IsMain,
				IssueBy = response.IssueBy,
				IssueDate = response.IssueDate,
				Number = response.Number,
				PhotoDate = response.PhotoDate,
				Series = response.Series
			};

			return result;
		}

		public static List<CounterpartDocumentApiModel> MapToCounterpartDocumentApiModel(this IEnumerable<CounterpartDocumentResponseModel> response)
		{
			var result = new List<CounterpartDocumentApiModel>();
			foreach (var row in response)
			{
				result.Add(row.MapToCounterpartDocumentApiModel());
			}

			return result;
		} 
	}
}
