namespace Vostok.Hotline.Api.Services.Models
{
	public class SendingStatusApiModel
	{
		public string MessagePackageId { get; set; } 
		public string ExternalId { get; set; }
		public MessageStatusApiModel Status { get; set; }
		public MessagePackageStatusesApiModel PackageStatuses { get; set; }
	}
}
