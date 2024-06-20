using System;
using System.Threading;
using System.Linq;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Conversions;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;
using System.Threading.Tasks;
using Vostok.Hotline.Api.References.Managers;
using Vostok.Hotline.Gateway.Client.Counterpart.Models;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Mapper
{
	public class ClientMapper
	{
		private readonly CountryApiManager _countryManager;
		public ClientMapper(CountryApiManager countryManager)
		{
			_countryManager = countryManager;
		}

		public async Task<ClientViewModel> MapToClientViewModelAsync(CounterpartApiModel response, CancellationToken cancellationToken)
		{
			var result = new ClientViewModel
			{
				CounterpartId = response.CounterpartId,
				FullName = response.FullName,
				BirthPlace = response.Attributes?.BirthPlace,
				DateOfBirth = Converter.ToDateTimeToStringVueUi(response.Attributes?.DateOfBirth),
				FinancialPhone = response.FinancialPhone,
				FirstName = response.FirstName,
				LastName = response.LastName,
				Inn = response.Inn,
				Gender = response.Attributes?.Gender,
				MiddleName = response.MiddleName,
				PersonId = response.PersonId,
				SolarId = response.SolarId,
				SrBankId = response.SrBankId,
				WorkPlace = response.Attributes?.WorkPlace,
				WorkPosition = response.Attributes?.WorkPosition,
				OpenedOn = Converter.ToDateTimeToStringVueUi(response.OpenedOn),
				IsOpen = response.IsOpen,
				IsResident = response.IsResident,
				ClosedOn = Converter.ToDateTimeToStringVueUi(response.ClosedOn),
				CodeWord = response.Attributes.CodeWord				
			};

			if (response.ResidentCountryId != null)
			{
				result.ResidentCountry = await _countryManager.GetCountryNameAsync(response.ResidentCountryId.Value, cancellationToken);
			}

			if (response.Contacts != null)
			{
				result.Email = response.Contacts
							.Where(x => x.ContactType == ContactTypeEnum.Email)
							.Select(x=>x.Value)
							.FirstOrDefault();
			}

			if (response.SegmentationType != null)
			{
				result.SegmentationType = response.SegmentationType.ToSegmentType();
			}

			if (response.Documents.Any(x => x.IsMain ?? false))
			{
				var document = response.Documents.Where(x => x.IsMain ?? false && x.DocumentType != null)
					.OrderBy(x => x.DocumentType.DocumentTypeId == 1) // UKR_PASSPORT
					.OrderBy(x => x.DocumentType.DocumentTypeId == 81) // UKR_PASSPORT_ID_CARD
					.OrderBy(x => x.DocumentType.DocumentTypeId == 141) // PERMANENT_RESIDENCE_PERMIT_ID
					.OrderBy(x => x.DocumentType.DocumentTypeId == 101) // UKR_DISPLACED_PERSON
					.OrderBy(x => x.DocumentType.DocumentTypeId == 161) // TEMPORARY_ACCOMMODATION_ID
					.FirstOrDefault();

				if (document != null)
				{
					result.MainDocument = new ClientDocumentViewModel
					{
						ClosingDate = Converter.ToDateTimeToStringVueUi(document.ClosingDate),
						CounterpartDocumentId = document.CounterpartDocumentId,
						CountryId = document.CountryId,
						ExpirationDate = Converter.ToDateTimeToStringVueUi(document.ExpirationDate),
						IssueBy = document.IssueBy,
						IssueDate = Converter.ToDateTimeToStringVueUi(document.IssueDate),
						Number = document.Number,
						PhotoDate = Converter.ToDateTimeToStringVueUi(document.PhotoDate),
						Series = document.Series
					};

					if (document.DocumentType != null)
					{
						result.MainDocument.DocumentType = new ClientDocumentTypeViewModel
						{
							DocumentTypeId = document.DocumentType.DocumentTypeId,
							Name = document.DocumentType.Description
						};
					}
				}
			}

			if (response.Addresses.Any(x => x.AddressType == AddressTypeEnum.ADR))
			{
				var addr = response.Addresses.First(x => x.AddressType == AddressTypeEnum.ADR);
				result.ResidentialAddress = new CounterpartAddressModel
				{
					CityName = addr.CityName,
					DistrictName = addr.DistrictName,
					Flat = addr.Flat,
					HouseNumber = addr.HouseNumber,
					HouseNumberAdd = addr.HouseNumberAdd,
					Korp = addr.Korp,
					PostIndex = addr.PostIndex,
					RegionName = addr.RegionName,
					StreetName = addr.StreetName
				}.GetAddress();
			} 

			if (response.Addresses.Any(x => x.AddressType == AddressTypeEnum.REG))
			{
				var reg = response.Addresses.First(x => x.AddressType == AddressTypeEnum.REG);
				result.RegistrationAddress = new CounterpartAddressModel
				{
					CityName = reg.CityName,
					DistrictName = reg.DistrictName,
					Flat = reg.Flat,
					HouseNumber = reg.HouseNumber,
					HouseNumberAdd = reg.HouseNumberAdd,
					Korp = reg.Korp,
					PostIndex = reg.PostIndex,
					RegionName = reg.RegionName,
					StreetName = reg.StreetName
				}.GetAddress();
			}

			return result;
		}

		public async Task<SearchPagedResponseRowModel<ClientViewModel>> MapToClientViewModelAsync(SearchPagedResponseRowModel<CounterpartApiModel> response, CancellationToken cancellationToken)
		{
			var result = new SearchPagedResponseRowModel<ClientViewModel>
			{
				IsNextPageAvailable = response.IsNextPageAvailable
			};

			foreach (var row in response.Rows)
			{
				result.Rows.Add(await MapToClientViewModelAsync(row, cancellationToken));
			}

			return result;
		} 
	}
}
