using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vostok.Hotline.Api.Services.Responses.Statement
{
	public class GenerateDocumentResponseModel
	{
		public List<GenerateDocumentDataResponseModel> Data { get; set; }
		public int Result { get; set; }
		[JsonProperty(PropertyName = "$RESULT_MESSAGE")]
		public string ResultMesage {get;set;}
	}
}
