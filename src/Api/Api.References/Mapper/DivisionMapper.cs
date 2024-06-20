using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.References.Responses;

namespace Vostok.Hotline.Api.References.Mapper
{
	public class DivisionMapper
	{
		public DivisionApiModel MapToDivisionApiModel(DivisionResponseModel response)
		{
			if (response == null)
				return null;

			var result = new DivisionApiModel
			{
				Id = response.Id,
				Code = response.Code,
				Name = response.Name
			};

			return result;
		}
	}
}
