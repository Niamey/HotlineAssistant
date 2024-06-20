using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class CounterpartMapperExtensions
	{
		public static CounterpartApiModel MapToCounterpartApiModel(this CounterpartResponseModel response)
		{
			var result = new CounterpartApiModel
			{
				CounterpartId = response.CounterpartId,
				FullName = response.FullName,
				FinancialPhone = response.FinancialPhone,
				FirstName = response.FirstName,
				LastName = response.LastName,
				Inn = response.Inn,
				MiddleName = response.MiddleName,
				PersonId = response.PersonId,
				SolarId = response.SolarId,
				SrBankId = response.SrBankId,
				OpenedOn = response.OpenedOn,
				IsOpen = response.IsOpen,
				IsResident = response.IsResident,
				ClosedOn = response.ClosedOn,
				DivisionId = response.DivisionId,
				SrBankCode = response.SrBankCode,
				RiskLevelCode = response.RiskLevelCode,
				ResidentCountryId = response.ResidentCountryId,
				ManagerId = response.ManagerId,
				IsVip = response.IsVip,
				IdentityDate = response.IdentityDate,
				IdentityCode = response.IdentityCode,
				SegmentationType = response.SegmentationType?.ToSegmentType(),
				Attributes = response.Attributes?.MapToCounterpartApiModel()
			};

			result.Contacts = response?.Contacts.MapToContactApiModel();
			result.CounterpartType = response.CounterpartType.MapToCounterpartTypeApiModel();
			result.Documents = response.Documents.MapToCounterpartDocumentApiModel();
			result.Addresses = response.Addresses.MapToCounterpartAddressApiModel();

			return result;
		}

		public static SearchPagedResponseRowModel<CounterpartApiModel> MapToCounterpartApiModel(this CounterpartResponseRowModel response)
		{
			var result = new SearchPagedResponseRowModel<CounterpartApiModel>
			{
				IsNextPageAvailable = response.IsNextPageAvailable
			};

			foreach (var row in response.Rows)
			{
				result.Rows.Add(row.MapToCounterpartApiModel());
			}

			return result;
		} 
	}
}
