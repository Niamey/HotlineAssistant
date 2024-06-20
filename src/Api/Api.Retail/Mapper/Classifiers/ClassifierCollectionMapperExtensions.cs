using Vostok.Hotline.Api.Retail.Models.Classifiers;
using Vostok.Hotline.Api.Retail.Responses.Classifiers;

namespace Vostok.Hotline.Api.Retail.Mapper.Classifiers
{
	public static class ClassifierCollectionMapperExtensions
	{
		public static ClassifierApiModel ToClassifierApiModel(this ClassifierResponseModel response)
		{
			var result = new ClassifierApiModel { 
				Id = response.Id,
				Comment = response.Comment,
				ValidFrom = response.ValidFrom,
				ValidTo = response.ValidTo
			};

			if (response.Entity != null)
			{
				result.Entity = new EntityApiModel
				{
					Id = response.Entity.Id
				};
				
				if (response.Entity.EntityType != null)
				{
					result.Entity.EntityType = new EntityTypeApiModel { 
						Id = response.Entity.EntityType.Id,
						Code = response.Entity.EntityType.Code.ToClassifierTypeEnum(),
						Name = response.Entity.EntityType.Name
					};
				}
			}

			if (response.Type != null)
			{
				result.Type = new ClassifierTypeApiModel {
					Id = response.Type.Id,
					Code = response.Type.Code,
					Name = response.Type.Name,
					Description = response.Type.Description
				};
			}

			if (response.Value != null)
			{
				result.Value = new ClassifierValueApiModel
				{
					Id = response.Value.Id,
					Code = response.Value.Code,
					Name = response.Value.Name,
					DefaultValue = response.Value.DefaultValue
				};
			}

			return result;
		}

		public static ClassifierCollectionApiModel ToClassifierCollectionApiModel (this ClassifierCollectionResponseModel response)
		{
			var result = new ClassifierCollectionApiModel();
			foreach (var row in response)
			{
				result.Add(row.ToClassifierApiModel());
			}
			return result;
		}
	}
}
