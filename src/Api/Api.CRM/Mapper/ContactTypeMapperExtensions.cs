using Vostok.Hotline.Api.CRM.Responses.Models;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class ContactTypeMapperExtensions
	{
		public static ContactTypeEnum? ToContactTypeEnum(this ContactTypeResponseModel response)
			  => response?.ContactTypeId switch
			  {
				  1 => ContactTypeEnum.ContactPhone,
				  2 => ContactTypeEnum.Fax,
				  3 => ContactTypeEnum.WorkPhone,
				  4 => ContactTypeEnum.Mobile,
				  5 => ContactTypeEnum.HomePhone,
				  20 => ContactTypeEnum.Email,
				  _ => null
			  };
	}
}
