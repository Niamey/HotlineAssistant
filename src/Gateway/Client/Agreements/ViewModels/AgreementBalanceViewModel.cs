using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.Agreements.ViewModels
{
	public class AgreementBalanceViewModel : ResponseBaseModel
	{
		/// <summary>
		/// Дата, по состоянию на которую отображается информация
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Навание валюты
		/// </summary>
		public string Currency { get; set; }

		/// <summary>
		/// оступный остаток средств на балансе, вычисляемый при помощи следующих компонентов:
		/// собственные,
		/// заблокированные,
		/// овердрафт,
		/// доступный остаток кредитного лимита
		/// </summary>
		public decimal Available { get; set; }

		/// <summary>
		/// Наличие кредитного лимита
		/// </summary>
		public bool HasCreditLimit { get; set; }

		/// <summary>
		/// Размер кредитного лимита
		/// </summary>
		public decimal CreditLimit { get; set; }

		/// <summary>
		/// Использовано кредитных средств
		/// </summary>
		public decimal UsedCreditLimit { get; set; }
	}
}
