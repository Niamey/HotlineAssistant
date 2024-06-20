using System;
using System.Collections.Generic;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Enums;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos
{
	public class AgreementDto
	{
		private readonly IList<AccessorDto> _accessors;
		private readonly IList<AgreementClassifierDto> _classifiers;
		private readonly IList<AttributeItemDto> _attributes;

		public AgreementDto()
		{
			_accessors = new List<AccessorDto>();
			_classifiers = new List<AgreementClassifierDto>();
			_attributes = new List<AttributeItemDto>();
		}
		public long Id { get; set; }
		public string Number { get; set; }
		public (int Id, string IdString, string Code, string Name) Currency { get; set; }

		public string SignatureDate { get; set; }

		public string EffectiveDate { get; set; }

		public string ExpirationDate { get; set; }

		public string ClosingDate { get; set; }
		public long PersonId { get; set; }
		public long ClientSolarId { get; set; }
		public long SrId { get; set; }
		public long SrCode { get; set; }

		public AccessorTypes AccessorFlags { get; set; }

		public IEnumerable<AccessorDto> Accessors => _accessors;
		public IEnumerable<AgreementClassifierDto> Classifiers => _classifiers;
		public IEnumerable<AttributeItemDto> Attributes => _attributes;

		public BalanceDto Balance { get; set; }
	}
}