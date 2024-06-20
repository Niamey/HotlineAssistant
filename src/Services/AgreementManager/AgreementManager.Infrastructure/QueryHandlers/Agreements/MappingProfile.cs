using AutoMapper;
using Solar.Models.Models;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;

namespace Vostok.HotLineAssistant.AgreementManager.Infrastructure.QueryHandlers.Agreements
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<AccessorClassifier, AccessorClassifierDto>();
			CreateMap<Accessor, AccessorDto>();
			CreateMap<AccessorData, AccessorDataDto>();
			CreateMap<AgreementClassifier, AgreementClassifierDto>();
			CreateMap<Agreement, AgreementDto>()
				.ForMember(d => d.SrId, s => s.MapFrom(_ => _.SRId))
				.ForMember(d => d.SrCode, s => s.MapFrom(_ => _.SRCode))
				.ForMember(d => d.ExpirationDate, s => s.MapFrom(_ => $"{_.ExpirationDate:dd.MM.yyyy}"))
				.ForMember(d => d.ClosingDate, s => s.MapFrom(_ => $"{_.ClosingDate:dd.MM.yyyy}"))
				.ForMember(d => d.EffectiveDate, s => s.MapFrom(_ => $"{_.EffectiveDate:dd.MM.yyyy}"))
				.ForMember(d => d.SignatureDate, s => s.MapFrom(_ => $"{_.SignatureDate:dd.MM.yyyy}"));
			CreateMap<AttributeItem, AttributeItemDto>();
			CreateMap<Balance, BalanceDto>()
				.ForMember(d => d.Date, s => s.MapFrom(_ => $"{_.Date:dd.MM.yyyy}"))
				.ForMember(d => d.NextDueDate, s => s.MapFrom(_ => $"{_.NextDueDate:dd.MM.yyyy}"))
				.ForMember(d => d.NextBillingDate, s => s.MapFrom(_ => $"{_.NextBillingDate:dd.MM.yyyy}"));
			CreateMap<Classifier, ClassifierDto>()
				.ForMember(d => d.ValidFrom, s => s.MapFrom(_ => _.ValidFrom))
				.ForMember(d => d.ValidTo, s => s.MapFrom(_ => _.ValidTo));
			//CreateMap<CardCommonData, CardCommonDataDto>();
			CreateMap<CardData, CardDataDto>()
				.ForMember(d => d.ExpiryDate, s => s.MapFrom(_ => $"{_.ExpiryDate:dd.MM.yyyy}"));
			;
			//CreateMap<CardType, CardTypeDto>();
		}
	}
}