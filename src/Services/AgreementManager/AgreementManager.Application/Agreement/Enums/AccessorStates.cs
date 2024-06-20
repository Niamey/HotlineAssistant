using System.ComponentModel.DataAnnotations;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Enums
{
	public enum AccessorStates
	{
		/// <summary>
		/// Неактивный
		/// </summary>
		[Display(Name = "Неактивный")]
		INACTIVE = 0,

		/// <summary>
		/// Активный
		/// </summary>
		[Display(Name = "Активный")]
		ACTIVE = 1
	}
}