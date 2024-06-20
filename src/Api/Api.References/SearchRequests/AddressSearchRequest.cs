using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.References.SearchRequests
{
	internal class AddressSearchRequest : SearchRequestBaseModel
	{
		public AddressSearchRequest(int addressId)
		{
			AddressId = addressId;
		}

		public int AddressId { get; set; }

		public override string GetUrlQuery()
		{
			return AddressId.ToString();
		}
	}
}