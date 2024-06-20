using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos;
using Vostok.HotLineAssistant.ContactManager.Domain.Clients;

namespace Vostok.HotLineAssistant.ContactManager.Infrastructure.QueryHandlers.Clients
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			var culture = new CultureInfo("uk");
			CreateMap<CounterpartTypes, TypeDto>()
				.ForMember(d => d.TypeId, s => s.MapFrom(_ => _.CounterpartTypeId));
			CreateMap<AddressTypes, AddressTypeDto>()
				.ForMember(d => d.TypeId, s => s.MapFrom(_ => _.AddressTypeId));
			CreateMap<DocumentTypes, DocumentTypeDto>()
				.ForMember(d => d.TypeId, s => s.MapFrom(_ => _.DocumentTypeId));
			CreateMap<ContactTypes, ContactTypeDto>()
				.ForMember(d => d.TypeId, s => s.MapFrom(_ => _.ContactTypeId));
			CreateMap<SocialStatuses, SocialStatusDto>()
				.ForMember(d => d.TypeId, s => s.MapFrom(_ => _.SocialStatusId));

			CreateMap<CounterpartDocument, DocumentDto>()
				.ForMember(d => d.Id, s => s.MapFrom(_ => _.DocumentTypeId))
				.ForMember(d => d.IssueDate, s => s.MapFrom(_ => $"{_.IssueDate:dd.MM.yyyy}"))
				.ForMember(d => d.ExpirationDate, s => s.MapFrom(_ => $"{_.ExpirationDate:dd.MM.yyyy}"))
				.ForMember(d => d.ClosingDate, s => s.MapFrom(_ => $"{_.ClosingDate:dd.MM.yyyy}"))
				.ForMember(d => d.PhotoDate, s => s.MapFrom(_ => $"{_.PhotoDate:dd.MM.yyyy}"))
				.ForMember(d => d.Type, s => s.MapFrom(_ => _.DocumentType));

			CreateMap<CounterpartContact, ContactDto>()
				.ForMember(d => d.Id, s => s.MapFrom(_ => _.CounterpartContactId))
				.ForMember(d => d.TypeId, s => s.MapFrom(_ => _.ContactTypeId))
				.ForMember(d => d.Type, s => s.MapFrom(_ => _.ContactType));

			CreateMap<CounterpartAddress, AddressDto>()
				.ForMember(d => d.Id, s => s.MapFrom(_ => _.CounterpartAddressId))
				.ForMember(d => d.TypeId, s => s.MapFrom(_ => _.AddressTypeId))
				.ForMember(d => d.Type, s => s.MapFrom(_ => _.AdressType));

			CreateMap<CounterpartAttribute, AttributeDto>()
				.ForMember(d => d.FirstNameLatin, s => s.MapFrom(_ => _.FirstNameLatin))
				.ForMember(d => d.StatusId, s => s.MapFrom(_ => _.SocialStatusId))
				.ForMember(d => d.Status, s => s.MapFrom(_ => _.SocialStatus))
				.ForMember(d => d.DateOfBirth, s => s.MapFrom(_ => $"{_.DateOfBirth:dd.MM.yyyy}"));

			CreateMap<Counterpart, ClientDto>()
				.ForMember(d => d.Id, s => s.MapFrom(_ => _.PersonId))
				.ForMember(d => d.TypeId, s => s.MapFrom(_ => _.CounterpartTypeId))
				.ForMember(d => d.FullName, s => s.MapFrom(_=>_.FullName))
				.ForMember(d => d.ClosedOn, s => s.MapFrom(_ => $"{_.ClosedOn:dd.MM.yyyy}"))
				.ForMember(d => d.OpenedOn, s => s.MapFrom(_ => $"{_.OpenedOn:dd.MM.yyyy}"))
				.ForMember(d => d.Addresses, s => s.MapFrom(_ => _.Addresses))
				.ForMember(d => d.Attributes, s => s.MapFrom(_ => _.Attributes))
				.ForMember(d => d.Type, s => s.MapFrom(_ => _.CounterpartType))
				.ForMember(d => d.Contacts, s => s.MapFrom(_ => _.Contacts))
				.ForMember(d => d.Documents, s => s.MapFrom(_ => _.Documents));
		}
	}
}