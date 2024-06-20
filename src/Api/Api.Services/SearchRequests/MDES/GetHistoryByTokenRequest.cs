using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vostok.Hotline.Api.Services.Enums.MDES;
using Vostok.Hotline.Api.Services.Mapper.MDES;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.SearchRequests.MDES
{
	public class GetHistoryByTokenRequest
	{
		internal GetHistoryByTokenRequest(string tokenUniqueReference, string userId, PaymentSystemTypeMdesEnum paymentType)
		{
			TokenUniqueReference = tokenUniqueReference;
			UserId = userId;
			PaymentSystem = paymentType.ToPaymentSystemTypeServiceEnum();
		}

		public string TokenUniqueReference { get; set; }
		public string UserId { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public PaymentSystemTypeServiceEnum PaymentSystem { get; set; }
	}
}
