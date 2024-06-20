namespace Vostok.Hotline.Api.Services.SearchRequests.BVR
{
	public class AddPermanentBlockingRequest
	{
		internal AddPermanentBlockingRequest(string phoneNumber, string description)
		{
			PhoneNumber = $"+{ phoneNumber }";
			Description = description;			
		}

		public string PhoneNumber { get; set; }
		
		public string Description { get; set; }		
	}
}
