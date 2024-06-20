using Vostok.Hotline.Api.Bvr.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Gateway.Client.Cards.Mapper
{
	public static class PushInfoStatusMapperExtensions
	{
		public static PushInfoStatusEnum ToPushInfoStatusEnum(this PushInfoBvrStatusEnum? response)
			=> response switch
			{
				PushInfoBvrStatusEnum.Active => PushInfoStatusEnum.Active,
				PushInfoBvrStatusEnum.Inactive => PushInfoStatusEnum.Inactive,
				_ => PushInfoStatusEnum.Undefined
			};
	}

}
