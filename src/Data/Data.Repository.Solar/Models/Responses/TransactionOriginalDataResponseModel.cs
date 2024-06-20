namespace Vostok.Hotline.Data.Repository.Solar.Models.Responses
{
	public class TransactionOriginalDataResponseModel
	{
		public string AcqInstitutionCode { get; set; }
		public string CardDataInputMode { get; set; }
		public string TerminalType { get; set; }
		public string OnlinePinVerificationResult { get; set; }
		public string Cvv2VerificationResult { get; set; }
		public string CavvValidationResult { get; set; }
		public string Stan { get; set; }
	}
}
