namespace Vostok.Hotline.Api.References.Helpers
{
	internal static class AddressHelper
	{
		public static string GetFullTypeName(string type, string name, bool region = false)
		{
			if (string.IsNullOrEmpty(name) || name.ToLower() == name)
			{
				return "";
			}
			else
			{
				return !string.IsNullOrEmpty(type) ?
					(region ? $"{name} {type}" : $"{type}. {name}")
					: name;
			}
		}
	}
}
