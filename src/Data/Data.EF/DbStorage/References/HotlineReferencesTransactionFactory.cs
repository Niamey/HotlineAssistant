namespace Vostok.Hotline.Data.EF.DbStorage.References
{
	public class HotlineReferencesTransactionFactory : HotlineBaseTransactionFactory<HotlineReferencesContext>
	{
		public HotlineReferencesTransactionFactory(HotlineReferencesContext dbContext) : base(dbContext)
		{
		}
	}
}