using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vostok.Hotline.Api.Services.SearchRequests.Statement
{
	public class GenerateDocumentRequest
	{
		[JsonProperty(PropertyName = "parameters")]
		public GenerateDocumentParams Parameters { get; set; }

		[JsonProperty(PropertyName = "typeCode")]
		public string TypeCode { get => "boStatement"; }
	}
}
