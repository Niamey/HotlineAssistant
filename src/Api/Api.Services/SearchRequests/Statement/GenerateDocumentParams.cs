using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vostok.Hotline.Api.Services.SearchRequests.Statement
{
	public class GenerateDocumentParams
	{
		public GenerateDocumentParams(long agreementId, DateTime dateStart, DateTime dateEnd)
		{
			AgreementId = agreementId;
			StartDate = dateStart.ToString("yyyy-MM-dd");
			EndDate = dateEnd.ToString("yyyy-MM-dd");
		}

		[JsonProperty(PropertyName = "agreementId")]
		public long AgreementId { get; set; }

		[JsonProperty(PropertyName = "startDate")]
		public string StartDate { get; set; }

		[JsonProperty(PropertyName = "endDate")]
		public string EndDate { get; set; }
	}
}
