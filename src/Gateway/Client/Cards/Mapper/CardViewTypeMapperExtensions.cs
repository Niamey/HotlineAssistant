using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class CardViewTypeMapperExtensions
	{
		public static ViewTypeEnum ToViewType (this CardTypeEnum response)
		 => response switch
		 {
			 CardTypeEnum.MasterCardBVR => ViewTypeEnum.BVR,
			 CardTypeEnum.MasterCardBVRVirt => ViewTypeEnum.BVR,
			 //CardTypeEnum.MASTERCARD_BVR_BARCODE => ViewTypeEnum.BVR,

			 _ => ViewTypeEnum.General
		 };
	}
}
