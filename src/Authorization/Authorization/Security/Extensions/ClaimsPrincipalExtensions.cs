using System;
using System.Security.Claims;

namespace Vostok.Hotline.Authorization.Security.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static int? GetNullableIntClaimValue(this ClaimsPrincipal principal, string claimType)
		{
			var claimValueAsString = principal.GetClaimValue<string>(claimType);
			int? claimValue = null;
			if (!string.IsNullOrWhiteSpace(claimValueAsString))
				claimValue = int.Parse(claimValueAsString);
			return claimValue;
		}

		public static TValueType GetClaimValue<TValueType>(this ClaimsPrincipal principal, string claimType)
		{
			var claim = principal.FindFirst(claimType);
			if (claim == null)
			{
				throw new ArgumentException($"User token doesn't contain claim of type '{claimType}'.");
			}
			var value = claim.Value;
			try
			{
				// Enums
				if (typeof(TValueType).IsEnum)
				{
					var intValue = int.Parse(value);
					return (TValueType)(object)intValue;
				}

				return (TValueType)Convert.ChangeType(value, typeof(TValueType));
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Tokenized {claimType} is not of type {typeof(TValueType).Name} (error: {ex.Message}).");
			}
		}

		public static TValueType GetClaimNullableValue<TValueType>(this ClaimsPrincipal principal, string claimType)
		{
			var claim = principal.FindFirst(claimType);
			if (claim == null)
			{
				return default(TValueType);
			}

			var value = claim.Value;
			try
			{
				// Enums
				if (typeof(TValueType).IsEnum)
				{
					var intValue = int.Parse(value);
					return (TValueType)(object)intValue;
				}

				return (TValueType)Convert.ChangeType(value, typeof(TValueType));
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Tokenized {claimType} is not of type {typeof(TValueType).Name} (error: {ex.Message}).");
			}
		}
	}
}
