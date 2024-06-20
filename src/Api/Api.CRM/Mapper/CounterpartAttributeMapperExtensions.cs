using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;
using Vostok.Hotline.Api.CRM.Mapper;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class CounterpartAttributeMapperExtensions
	{
		public static CounterpartAttributeApiModel MapToCounterpartApiModel(this CounterpartAttributeResponseModel response)
		{
			var result = new CounterpartAttributeApiModel
			{
				BirthPlace = response.BirthPlace,
				CodeWord = response.CodeWord,
				DateOfBirth = response.DateOfBirth,
				DependentAmount = response.DependentAmount,
				FirstNameLatin = response.FirstNameLatin,
				Gender = response.Gender.ToGenderEnum(),
				IsArrest = response.IsArrest,
				IsPep = response.IsPep,
				IsTerrorist = response.IsTerrorist,
				LastNameLatin = response.LastNameLatin,
				MaritalStatus = response.MaritalStatus.ToMaritalStatusEnum(),
				SocialStatus = response.SocialStatus?.MapToSocialStatusApiModel(),
				WorkPlace = response.WorkPlace,
				WorkPosition = response.WorkPosition
			};

			return result;
		}
	}
}
