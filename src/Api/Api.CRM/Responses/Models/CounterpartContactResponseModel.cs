namespace Vostok.Hotline.Api.CRM.Responses.Models
{
	internal class CounterpartContactResponseModel 
	{
		public long CounterpartContactId { get; set; }
		public string Value { get; set; }
		public short? ContactAttributes { get; set; }
		public ContactTypeResponseModel ContactType { get; set; }
	}
}