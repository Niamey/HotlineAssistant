namespace Vostok.Hotline.Data.EF.DbStorage.Core
{
	public class HotlineCoreTransactionFactory : HotlineBaseTransactionFactory<HotlineCoreContext>
	{
		public HotlineCoreTransactionFactory(HotlineCoreContext dbContext) : base(dbContext)
		{
		}
	}
}