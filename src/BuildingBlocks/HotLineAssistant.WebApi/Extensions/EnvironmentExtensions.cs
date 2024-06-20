using System.IO;
using System.Text.RegularExpressions;

namespace Vostok.HotLineAssistant.WebApi.Extensions
{
	public static class EnvironmentExtensions
	{
		private static Regex SecretRegex { get; } = new Regex(@"^SECRETS:(?<path>.*)$", RegexOptions.Compiled);

		public static string GetSecretVariable(this string variable)
		{
			if (variable == null)
			{
				return null;
			}
			var secret = SecretRegex.Match(variable);
			return secret.Success ? File.ReadAllText(secret.Groups["path"].Value) : variable;
		}
	}
}