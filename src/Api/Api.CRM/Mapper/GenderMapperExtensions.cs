using Vostok.Hotline.Api.CRM.Responses.Enums;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class GenderMapperExtensions
	{
		public static GenderEnum? ToGenderEnum(this GenderCrmEnum? response)
			  => response switch
			  {
				  GenderCrmEnum.Female => GenderEnum.Female,
				  GenderCrmEnum.Male => GenderEnum.Male,
				  _ => null
			  };
	}
}