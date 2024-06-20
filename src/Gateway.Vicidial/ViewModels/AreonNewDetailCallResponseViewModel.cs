using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Vicidial.ViewModels
{
	[XmlRoot("XML")]
	public class AreonNewDetailCallResponseViewModel : ResponseBaseModel
	{
		[JsonProperty("O_FOSLINK")]
		[XmlElement("O_FOSLINK")]
		public string Url { get; set; }

		[JsonProperty("O_ISVIP")]
		[XmlElement("O_ISVIP")]
		public byte IsVip { get; set; }

		[JsonProperty("O_OFFICE")]
		[XmlElement("O_OFFICE")]
		public string Office { get; set; }

		[JsonProperty("O_NAME")]
		[XmlElement("O_NAME")]
		public string FullName { get; set; }

		[JsonProperty("O_PERSONALMANAGER")]
		[XmlElement("O_PERSONALMANAGER")]
		public string Manager { get; set; }
	}
}
