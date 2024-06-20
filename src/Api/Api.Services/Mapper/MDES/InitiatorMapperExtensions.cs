using Vostok.Hotline.Api.Services.Enums.MDES;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Mapper.MDES
{
	public static class InitiatorMapperExtensions
	{
		public static InitiatorMdesEnum ToInitiatorMdesEnum(this string responses)
		{
			if (responses == null)
			{
				return InitiatorMdesEnum.Undefined;
			} else
			{
				return responses.Trim().ToUpper() switch
				{
					"I" => InitiatorMdesEnum.Issuer,
					"W" => InitiatorMdesEnum.TokenRequestorWalletProvider,
					"C" => InitiatorMdesEnum.Cardholder,
					"P" => InitiatorMdesEnum.MobPINValidService,
					"M" => InitiatorMdesEnum.MobPINChangeValidService,
					_ => InitiatorMdesEnum.Undefined
				};
			}
		}			
	}
}
