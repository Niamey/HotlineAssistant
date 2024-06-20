namespace Vostok.Hotline.Core.Data.Abstractions
{
	public interface ITransactionFactory
	{
		ITransaction Create();
		ITransaction CreateReadUncommitted();
	}
}
