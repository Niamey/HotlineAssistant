using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Bvr.SearchRequests
{
	class ResetPINDeviceRequest : SearchRequestBaseModel
	{
		public ResetPINDeviceRequest(string phoneNumber)
		{
			PhoneNumber = phoneNumber;
		}
		public string PhoneNumber { get; set; }
		public override string GetUrlQuery()
		{
			return PhoneNumber.Contains("+") ? PhoneNumber : $"+{PhoneNumber}";
		}
	}
}
