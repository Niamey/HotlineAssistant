namespace Vostok.Hotline.Core.Common.Enums.Cards
{
	public enum CardBlockingReasonTypeEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,
		FoundbByAnotherPerson = 1,
		Lost = 2,
		Stolen = 3,
		ForgottenInATM = 4,
		UnknownTransactions = 5,
		ReportedData = 6,
		ClientIsCheater = 7,
		WithdrawnFromATM = 8,
		BlockingOperations = 9,
		RequestedByClient = 10,
		Other = 11,
		ReceivedSMSCode = 12
	}
}
