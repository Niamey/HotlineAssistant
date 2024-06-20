using System;
using Vostok.Hotline.Api.Retail.Responses.Accounts.Enums;
using Vostok.Hotline.Core.Common.Enums.Accounts;

namespace Vostok.Hotline.Api.Retail.Mapper.Accounts
{
	internal static class AccountTypeMapperExtensions
	{
		public static AccountTypeEnum ToAccountTypeEnum(this AccountTypeRetailEnum response)
			=> response switch
			{
				// AccountTypeRetailEnum.Main => AccountTypeEnum.Main,

				AccountTypeRetailEnum.DEVICE_1004 => AccountTypeEnum.Device1004,
				AccountTypeRetailEnum.INT_2608 => AccountTypeEnum.Int2608,
				AccountTypeRetailEnum.TRANSIT_2920 => AccountTypeEnum.Transit2920,
				AccountTypeRetailEnum.DISPUTE_2924 => AccountTypeEnum.Dispute2924,
				AccountTypeRetailEnum.SHORTAGE_2924 => AccountTypeEnum.Shortage2924,
				AccountTypeRetailEnum.SURPLUS_2924 => AccountTypeEnum.Surplus2924,
				AccountTypeRetailEnum.TRANSIT_2924 => AccountTypeEnum.Transit2924,
				AccountTypeRetailEnum.FEE_3570 => AccountTypeEnum.Fee3570,
				AccountTypeRetailEnum.FEE_OVD_3570 => AccountTypeEnum.FeeOvd3570,
				AccountTypeRetailEnum.FEE_3578 => AccountTypeEnum.Fee3578,
				AccountTypeRetailEnum.FEE_OVD_3578 => AccountTypeEnum.FeeOvd3578,
				AccountTypeRetailEnum.CASHBACK_3678 => AccountTypeEnum.Cashback3678,
				AccountTypeRetailEnum.LIMIT_9129 => AccountTypeEnum.Limit9129,
				AccountTypeRetailEnum.DAMPING_9601_9618 => AccountTypeEnum.Damping96019618,
				AccountTypeRetailEnum.RESERVE_9601 => AccountTypeEnum.Reserve9601,
				AccountTypeRetailEnum.ACC_2924_ADVICE_OFFUS => AccountTypeEnum.Acc2924AdviceOffus,
				AccountTypeRetailEnum.ACC_3622_NDFL => AccountTypeEnum.Acc3622Ndfl,
				AccountTypeRetailEnum.ACC_6050 => AccountTypeEnum.Acc6050,
				AccountTypeRetailEnum.ACC_6052 => AccountTypeEnum.Acc6052,
				AccountTypeRetailEnum.ACC_2924_P2P_VB => AccountTypeEnum.Acc2924P2PVb,
				AccountTypeRetailEnum.ACC_2924_TRANSIT_29 => AccountTypeEnum.Acc2924Transit29,
				AccountTypeRetailEnum.ACC_3622_MILITARY_FEE => AccountTypeEnum.Acc3622MilitaryFee,
				AccountTypeRetailEnum.ACC_3800 => AccountTypeEnum.Acc3800,
				AccountTypeRetailEnum.ACC_3801_EUR => AccountTypeEnum.Acc3801Eur,
				AccountTypeRetailEnum.ACC_3801_USD => AccountTypeEnum.Acc3801Usd,
				AccountTypeRetailEnum.ACC_6397 => AccountTypeEnum.Acc6397,
				AccountTypeRetailEnum.ACC_6510 => AccountTypeEnum.Acc6510,
				AccountTypeRetailEnum.ACC_6510_CASH => AccountTypeEnum.Acc6510Cash,
				AccountTypeRetailEnum.ACC_6514 => AccountTypeEnum.Acc6514,
				AccountTypeRetailEnum.ACC_7020 => AccountTypeEnum.Acc7020,
				AccountTypeRetailEnum.ACC_7020_VAL => AccountTypeEnum.Acc7020Val,
				AccountTypeRetailEnum.ACC_7040 => AccountTypeEnum.Acc7040,
				AccountTypeRetailEnum.ACC_7040_VAL => AccountTypeEnum.Acc7040Val,
				AccountTypeRetailEnum.ACC_9900 => AccountTypeEnum.Acc9900,
				AccountTypeRetailEnum.ACC_9910 => AccountTypeEnum.Acc9910,
				AccountTypeRetailEnum.OVER => AccountTypeEnum.Over,
				AccountTypeRetailEnum.OVER_EXPIRE_2809BPK => AccountTypeEnum.OverExpire2809Bpk,
				AccountTypeRetailEnum.OVER_INT => AccountTypeEnum.OverInt,
				AccountTypeRetailEnum.OVER_INT_OVD => AccountTypeEnum.OverIntOvd,
				AccountTypeRetailEnum.OVER_OVD => AccountTypeEnum.OverOvd,
				AccountTypeRetailEnum.OVER_OVD_INT => AccountTypeEnum.OverOvdInt,
				AccountTypeRetailEnum.OVER_OVD_INT_OVD => AccountTypeEnum.OverOvdIntOvd,
				AccountTypeRetailEnum.OVER_REST_2809BPK => AccountTypeEnum.OverRest2809Bpk,
				AccountTypeRetailEnum.SKS => AccountTypeEnum.Sks,
				AccountTypeRetailEnum.SKS_INT => AccountTypeEnum.SksInt,
				AccountTypeRetailEnum.АСС_2924_P2P => AccountTypeEnum.Асс2924P2P,

				_ => AccountTypeEnum.Undefined
			};
	}
}
