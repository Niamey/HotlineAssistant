using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Accounts;

namespace Vostok.Hotline.Gateway.Client.Accounts.ViewModels
{
	public class AccountViewModel : ResponseBaseModel
	{
		/// <summary>
		/// ИД счета в Солар
		/// </summary>
		public long AccountId { get; set; }
		/// <summary>
		/// ИД клиента в Солар
		/// </summary>
		public long SolarId { get; set; }
		/// <summary>
		/// ИД договора в Солар
		/// </summary>
		public long AgreementId { get; set; }
		/// <summary>
		/// Номер счета
		/// </summary>
		public string Number { get; set; }
		/// <summary>
		/// Валюта
		/// </summary>
		public string Currency { get; set; }
		/// <summary>
		/// Тип
		/// </summary>
		public AccountTypeEnum Type { get; set; }
		/// <summary>
		/// Статус
		/// </summary>
		public AccountStatusEnum Status { get; set; }
	}
}