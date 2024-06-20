using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Authorization.FzBank.Models
{
	public class FzBankAuthorizationApiModel
	{
		public FzBankAuthorizationDataApiModel Data { get; set; }

		public bool HasError => StatusCode != StatusCodes.Status200OK;
		public AuthorizationStateEnum AuthorizationState 
		{
			get 
			{
				if (HasError || string.IsNullOrEmpty(Data?.Login))
					return AuthorizationStateEnum.UnSuccess;

				return AuthorizationStateEnum.Success;
			}
		}

		public string ErrorMessage { get; set; }
		public int? StatusCode { get; set; }
	}
}
