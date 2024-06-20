using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Core
{
	public class TravelCard : BusinessEntityBase
	{
		public TravelCard()
		{
		}

		public int TravelCardId { get; set; }

		public int TravelId { get; set; }

		public long CardId { get; set; }

		public string CardMaskNumber { get; set; }

		public virtual Travel Travel { get; set; }
	}
}
