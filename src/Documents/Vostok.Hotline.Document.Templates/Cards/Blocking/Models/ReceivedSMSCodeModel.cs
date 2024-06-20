using System;
using System.Text;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Abstractions;
using Vostok.Hotline.Document.Templates.Cards.Blocking.Enums;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class ReceivedSMSCodeModel : ICardBlockingReason
	{
		public string ContactPhone { get; set; }
		public ClientTransactionsModel ClientTransactions { get; set; }
		public PropertyListModel Properties { get; set; }
		public PersonWhoCallsModel Person { get; set; }
		public CheaterModel Cheater { get; set; }
		public TokenListModel DeletedTokens { get; set; }
		public bool? IsClientToldSMCodeFromSMS { get; set; }
		public bool? IsNeedDeleteToken { get; set; }
		public bool? IsCancelAccessCode { get; set; }
		public SecurityCodeOperationTypeEnum? SecurityCodeOperationType { get; set; }
		public SmsSenderTypeEnum? SenderName { get; set; }
		public string MerchantName { get; set; }
		public DateTime? MerchantDate { get; set; }
		public bool? IsSMSMissingUPC { get; set; }
		public DateTime? SmsDateUPC { get; set; }
		public bool? IsSMSMissingGate { get; set; }
		public DateTime? SmsDateGate { get; set; }

		public override string ToString()
		{
			if (IsClientToldSMCodeFromSMS ?? false)
			{
				StringBuilder sb = new StringBuilder();
				if (Properties.Count > 0)
				{
					sb.AppendLine("Список даних переданих шахраєві:");

					foreach (var item in Properties)
					{
						sb.AppendLine($"{item.PersonDataTitle}");
					};
				}

				if (!string.IsNullOrEmpty(Cheater.ToString()))
					sb.AppendLine($"Інфо про шахрая: {Cheater}");

				if (!string.IsNullOrEmpty(ContactPhone))
					sb.AppendLine($"Конт. тел. клієнта: {ContactPhone}");

				if (!string.IsNullOrEmpty(ClientTransactions.ToString()))
					sb.AppendLine($"{ClientTransactions}");

				if (IsNeedDeleteToken ?? false)
				{
					sb.AppendLine("Вилучені токени:");

					if (DeletedTokens.Count > 0)
						sb.AppendLine($"{DeletedTokens}");
					else
						sb.AppendLine("Операції з токенами не здійснювалося");
				}

				return sb.ToString();
			}
			else 
			{
				if (SenderName == SmsSenderTypeEnum.BankVostok) {
					return $"Не підтверджує код завершення операції {MerchantDate} {MerchantName}, в HLA/ SMSGate не відображається, відправить скрін, контактний телефон {ContactPhone}";
				}

				if (IsSMSMissingUPC != null) 
					if (!(bool)IsSMSMissingUPC) 
						return $"Дзвінок шахрая, код не повідомив: Не підтверджує SecureCode {SmsDateUPC}, контактний телефон {ContactPhone}";

				StringBuilder sb = new StringBuilder();

				switch (SecurityCodeOperationType)
				{
					case SecurityCodeOperationTypeEnum.MobileAppPassword:
						sb.AppendLine($"Дзвінок шахрая, код не повідомив: Не підтверджує пароль мобІльного додатку {SmsDateGate}, контактний телефон {ContactPhone}");
						break;
					case SecurityCodeOperationTypeEnum.CardVerification:
						sb.AppendLine($"Дзвінок шахрая, код не повідомив: Спроба оцифрувати карту без відома клієнта, контактний телефон {ContactPhone}");
						break;
					case SecurityCodeOperationTypeEnum.Other:
						sb.AppendLine($"Дзвінок шахрая, код не повідомив: Не підтверджує код {SmsDateGate}, контактний телефон {ContactPhone}");
						break;
					default:
						break;
				}

				if (IsNeedDeleteToken ?? false)
				{
					sb.AppendLine("Вилучені токени:");

					if (DeletedTokens.Count > 0)
						sb.AppendLine($"{DeletedTokens}");
					else
						sb.AppendLine("Операції з токенами не здійснювалося");
				}
			}
			return string.Empty;
 		}
	}
}
