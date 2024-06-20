using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;

namespace Vostok.Hotline.Api.CRM.Mapper
{
	internal static class DocumentTypeAttributeMapperExtensions
	{
		public static DocumentTypeApiModel MapToDocumentTypeApiModel(this DocumentTypeResponseModel response)
		{
			var result = new DocumentTypeApiModel
			{
				DocumentTypeId = response.DocumentTypeId,
				Description = response.Description,
				Name = response.Name
			};

			return result;
		}
	}
}