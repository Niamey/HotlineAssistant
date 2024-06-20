namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class TemplateHelper
	{
		public static string CheckData(object value)
		{
			if (value == null || string.IsNullOrEmpty(value?.ToString()))
				return "-";
			else
				return value.ToString();
		}
	}
}
