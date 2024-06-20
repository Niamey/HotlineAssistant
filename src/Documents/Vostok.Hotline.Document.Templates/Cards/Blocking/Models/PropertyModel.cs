using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class PropertyModel
	{
		public PersonDataTypeEnum? PersonDataType { get; set; }
		public bool? PersonDataValue { get; set; }
		public string PersonDataTitle { get; set; }
	}
}
