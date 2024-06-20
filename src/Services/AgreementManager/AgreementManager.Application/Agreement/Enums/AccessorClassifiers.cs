using System;
using System.ComponentModel.DataAnnotations;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Enums
{
	[Flags]
	public enum AccessorClassifiers
	{
		/// <summary>
		/// Статус карты
		/// </summary>
		[Display(Name = "Статус карты")]
		CARD_STATUS = 1,

		/// <summary>
		/// Признак «Мошенник»
		/// </summary>
		[Display(Name = "Признак «Мошенник»")]
		SCAMMER = 2,
		/// <summary>
		/// 
		/// </summary>
		ACCESSOR_ISSUE_TYPE = 3,
		/// <summary>
		/// 
		/// </summary>
		FLAG_REMOVESTOPLIST = 4,
		/// <summary>
		/// 
		/// </summary>
		FLAG_CHANGELIMIT = 5
	}
}