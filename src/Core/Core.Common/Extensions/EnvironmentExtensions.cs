using System.IO;
using System.Text.RegularExpressions;

namespace Vostok.Hotline.Core.Common.Extensions
{
	public static class EnvironmentExtensions
	{
		private static Regex _secretRegex { get; } = new Regex(@"^SECRETS:(?<path>.*)$", RegexOptions.Compiled);

		public static string GetSecretVariable(this string variable)
		{
			if (variable == null)
				return null;
			
			var secret = _secretRegex.Match(variable);
			if (secret.Success)
			{
				return File.ReadAllText(secret.Groups["path"].Value)?.Trim();
			}

			return variable;
		}
	}
}
