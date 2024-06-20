using Vostok.Hotline.Api.CRM.Responses.Enums;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class MaritalStatusMapperExtensions
	{
		public static MaritalStatusEnum? ToMaritalStatusEnum(this MaritalStatusCrmEnum? response)
			  => response switch
			  {
				  MaritalStatusCrmEnum.Divorce => MaritalStatusEnum.Divorce,
				  MaritalStatusCrmEnum.InCivilMarriage => MaritalStatusEnum.InCivilMarriage,
				  MaritalStatusCrmEnum.Married => MaritalStatusEnum.Married,
				  MaritalStatusCrmEnum.Single => MaritalStatusEnum.Single,
				  MaritalStatusCrmEnum.Widow => MaritalStatusEnum.Widow,
				  _ => null
			  };
	}
}