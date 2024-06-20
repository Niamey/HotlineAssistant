using Vostok.Hotline.Api.Retail.Responses.MoneyBoxes.Enums;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Retail.Mapper.MoneyBoxes
{
	internal static class MoneyBoxStatusMapperExtensions
	{
		public static MoneyBoxStatusEnum ToMoneyBoxStatusEnum(this MoneyBoxStatusRetailEnum response)
			=> response switch
			{
				MoneyBoxStatusRetailEnum.Active => MoneyBoxStatusEnum.Active,
				MoneyBoxStatusRetailEnum.Inactive => MoneyBoxStatusEnum.Inactive,
				_ => MoneyBoxStatusEnum.Undefined
			};
	}
}
