using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vostok.Hotline.Api.Retail.Responses.Accounts.Enums;
using Vostok.Hotline.Core.Common.Conversions;

namespace Vostok.Hotline.Api.Retail.Responses.Accounts
{
	/// <summary>
	/// Договор
	/// </summary>
	internal class AccountResponseModel 
    {
		/// <summary>
		/// ИД счета в Солар
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// ИД клиента в Солар
		/// </summary>
		public long ClientId { get; set; }
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
		[JsonProperty("Currency")]
		public string CurrencyCode { get; set; }
		/// <summary>
		/// Тип
		/// </summary>
		public AccountTypeRetailEnum Type { get; set; }
		/// <summary>
		/// Статус
		/// </summary>
		public AccountStatusRetailEnum Status { get; set; }
	}
}
