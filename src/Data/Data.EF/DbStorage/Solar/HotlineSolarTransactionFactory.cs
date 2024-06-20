namespace Vostok.Hotline.Data.EF.DbStorage.Solar
{
	public class HotlineSolarTransactionFactory : HotlineBaseTransactionFactory<HotlineSolarContext>
	{
		public HotlineSolarTransactionFactory(HotlineSolarContext dbContext) : base(dbContext)
		{
		}
	}
}