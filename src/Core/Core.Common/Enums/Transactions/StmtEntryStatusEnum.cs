namespace Vostok.Hotline.Core.Common.Enums.Transactions
{
	public enum StmtEntryStatusEnum: byte
	{
		Undefined = UndefinedEnum.Undefined,
		Rejected = 1,
		Finished = 2,
		PreProcessing = 3,
		Closed = 4,
		Shaded = 5,
		PreAppliedAsFin = 6
	}
}
