using Vostok.Hotline.Api.CRM.Responses.Models;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class AddressTypeMapperExtensions
	{
		public static AddressTypeEnum? ToAddressTypeEnum(this AddressTypeResponseModel response)
			  => response?.AddressTypeId switch
			  {
				  1 => AddressTypeEnum.ADR,
				  2 => AddressTypeEnum.REG,
				  _ => null
			  };
	}
}
