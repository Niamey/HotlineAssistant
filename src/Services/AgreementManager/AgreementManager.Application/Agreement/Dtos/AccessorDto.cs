using System.Collections.Generic;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Enums;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos
{
	public class AccessorDto
	{
		private readonly IList<AccessorClassifierDto> _classifiers;
		private readonly IList<CardTypeDto> _cardTypes;

		public AccessorDto()
		{
			_classifiers = new List<AccessorClassifierDto>();
			_cardTypes = new List<CardTypeDto>();
		}

		public long Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }

		public AccessorTypes Type { get; set; }
		public string Number { get; set; }

		public AccessorStates State { get; set; }
		public AccessorClassifiers ClassifiersFlags { get; set; }

		public IEnumerable<AccessorClassifierDto> Classifiers => _classifiers;
		public IEnumerable<CardTypeDto> CardTypes => _cardTypes;

		public long AgreementSolarId { get; set; }
		public long ClientSolarId { get; set; }

		public AccessorDataDto AccessorData { get; set; }
	}
}