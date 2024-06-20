using Vostok.Hotline.Core.Common.Conversions;

namespace Vostok.Hotline.Core.Documents.Helpers
{
	public static class TemplateHelper
	{
		public static string CheckData(object value)
		{
			if (string.IsNullOrEmpty(Converter.ToStringNullable(value)))
				return "-";
			else
				return value.ToString();
		}
	}
}
