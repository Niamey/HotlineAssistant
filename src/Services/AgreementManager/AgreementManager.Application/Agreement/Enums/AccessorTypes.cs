using System;
using System.ComponentModel.DataAnnotations;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Enums
{
	[Flags]
	public enum AccessorTypes
	{
		NONE = -0x1,

		[Display(Name = "Номер основного счета")]
		IBAN = 0x1,

		[Display(Name = "Номер основной сделки")]
		SRBANK_ID = 0x2,

		[Display(Name = "Пластиковая карта")]
		CARD = 0x4
	}
}