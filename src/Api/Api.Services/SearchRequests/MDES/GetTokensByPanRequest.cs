using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vostok.Hotline.Api.Services.Enums.MDES;
using Vostok.Hotline.Api.Services.Mapper.MDES;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.SearchRequests.MDES
{
	public class GetTokensByPanRequest
	{
		internal GetTokensByPanRequest(string cardNumber, string userId, PaymentSystemTypeMdesEnum paymentType)
		{
			Pan = cardNumber;
			UserId = userId;
			PaymentSystem = paymentType.ToPaymentSystemTypeServiceEnum();
		}

		public string Pan { get; set; }
		public string UserId { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public PaymentSystemTypeServiceEnum PaymentSystem { get; set; }
	}
}
