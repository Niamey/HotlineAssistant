using Vostok.Hotline.Api.Services.Models.MDES;
using Vostok.Hotline.Api.Services.Responses.MDES;

namespace Vostok.Hotline.Api.Services.Mapper.MDES
{
	public static class TokenListMapperExtensions
	{
		public static TokenApiModel ToTokenApiModel(this TokenResponseModel response)
		{
			var result = new TokenApiModel
			{
				TokenUniqueReference = response.TokenUniqueReference,
				TokenStatusName = response.TokenStatusName.ToTokenStatusNameMdesEnum(),
				THdateTime = response.THdateTime,
				DeviceName = response.DeviceName,
				DeviceType = response.DeviceType,
				Wallet = response.Wallet,
				RequestorName = response.RequestorName,
				StorageTechnology = response.StorageTechnology
			};
			return result;
		}
		public static TokenCollectionApiModel ToTokenListApiModel(this TokenListResponseModel response)
		{
			var result = new TokenCollectionApiModel();
			foreach (var row in response.Tokens)
			{
				result.Add(row.ToTokenApiModel());
			}
			return result;
		}
	}
}
