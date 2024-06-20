using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vostok.Hotline.Api.Services.Enums.MDES;
using Vostok.Hotline.Api.Services.Mapper.MDES;
using Vostok.Hotline.Api.Services.Responses.MDES.Enums;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.SearchRequests.MDES
{
	public class TokenActionRequest
	{
		internal TokenActionRequest(string tokenUniqueReference, string commentText, ReasonTypeMdesEnum reasonCode, string userId, PaymentSystemTypeMdesEnum paymentType)
		{
			TokenUniqueReference = tokenUniqueReference;
			CommentText = commentText;
			UserId = userId;
			PaymentSystem = paymentType.ToPaymentSystemTypeServiceEnum();
			ReasonCode = reasonCode.ToReasonTypeServiceEnum();
		}		

		public string TokenUniqueReference { get; set; }
		
		public string CommentText { get; set; }
		
		[JsonConverter(typeof(StringEnumConverter))]
		public ReasonTypeServiceEnum ReasonCode { get; set; }
		
		public string UserId { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public PaymentSystemTypeServiceEnum PaymentSystem { get; set; }
	}
}
