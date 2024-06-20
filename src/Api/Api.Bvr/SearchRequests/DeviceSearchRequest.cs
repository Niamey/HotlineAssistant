using System.Web;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.Bvr.SearchRequests
{
	internal class DeviceSearchRequest : SearchRequestBaseModel
	{
		public DeviceSearchRequest(string phoneNumber)
		{
			PhoneNumber = phoneNumber;
		}
		public string PhoneNumber { get; set; }
		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters[nameof(PhoneNumber)] = PhoneNumber.Contains("+") ? PhoneNumber : $"+{PhoneNumber}";
			return parameters.ToString();
		}
	}
}
