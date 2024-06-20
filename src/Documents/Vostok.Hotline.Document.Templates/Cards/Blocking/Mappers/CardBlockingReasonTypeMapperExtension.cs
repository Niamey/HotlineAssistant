using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Mappers
{
	public static class CardBlockingReasonTypeMapperExtension
	{
		public static string GetText(this CardBlockingReasonTypeEnum? request) 
		{
			switch (request)
			{
				case CardBlockingReasonTypeEnum.Undefined:
					return "Не визначено";
				case CardBlockingReasonTypeEnum.FoundbByAnotherPerson:
					return "Знайдена іншою особою";
				case CardBlockingReasonTypeEnum.Lost:
					return "Загублена";
				case CardBlockingReasonTypeEnum.Stolen:
					return "Вкрадена";
				case CardBlockingReasonTypeEnum.ForgottenInATM:
					return "Забута в банкоматі";
				case CardBlockingReasonTypeEnum.UnknownTransactions:
					return "Операції без відома клієнта";
				case CardBlockingReasonTypeEnum.ReportedData:
					return "Повідомив дані шахраям";
				case CardBlockingReasonTypeEnum.ClientIsCheater:
					return "Клієнт банку - шахрай";
				case CardBlockingReasonTypeEnum.WithdrawnFromATM:
					return "Вилучена в банкоматі";
				case CardBlockingReasonTypeEnum.BlockingOperations:
					return "Не впевнений, що карта втрачена";
				case CardBlockingReasonTypeEnum.RequestedByClient:
					return "Блокування за рішенням клієнта (картка в наявності)";
				case CardBlockingReasonTypeEnum.Other:
					return "Інше";
				case CardBlockingReasonTypeEnum.ReceivedSMSCode:
					return "Отримав SMS з кодом";
				default:
					return string.Empty;
			}
		}
	}
}
