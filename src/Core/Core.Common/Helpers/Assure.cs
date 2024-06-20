using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class Assure
	{
		/// <exception cref="ArgumentNullException" />
		public static T ArgumentNotNull<T>(T value, string argumentName, string message = null)
		{
			if (value == null)
			{
				throw new ArgumentNullException(argumentName, message);
			}

			return value;
		}

		/// <exception cref="ArgumentNullException" />
		public static string ArgumentNotNullNotEmpty(string value, string argumentName, string message = null)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(argumentName, message);
			}

			return value;
		}

		/// <exception cref="ArgumentOutOfRangeException" />
		public static int ArgumentPositive(int value, bool zeroAllowed, string argumentName, string message = null)
		{
			var valid = zeroAllowed ? value >= 0 : value > 0;
			if (!valid)
			{
				if (message == null)
				{
					message = string.Format("'{0}' must be larger {1} zero.", argumentName, zeroAllowed ? "or equal to" : "then");
				}
				throw new ArgumentOutOfRangeException(argumentName, message);
			}

			return value;
		}

		/// <exception cref="InvalidOperationException" />
		public static void ArgumentCheck(string argumentName, bool isPassed, string message = null)
		{
			if (!isPassed)
			{
				throw new ArgumentException(message ?? "Invalid argument", argumentName);
			}
		}

		/// <exception cref="InvalidOperationException" />
		public static void EnumDefined<TEnum>(object enumValue, string argumentName, string message = null) where TEnum : Enum
		{
			if (!Enum.IsDefined(typeof(TEnum), enumValue) && enumValue != null)
			{
				throw new ArgumentException(message ?? "Invalid argument", argumentName);
			}
		}

		/// <exception cref="InvalidOperationException" />
		public static void Check(bool isPassed, string message = null)
		{
			if (!isPassed)
			{
				throw (message == null ? new InvalidOperationException() : new InvalidOperationException(message));
			}
		}

		/// <exception cref="InvalidOperationException" />
		public static void AnyNotNull(params object[] values)
		{
			if (values == null || values.All(x => x == null))
			{
				throw new InvalidOperationException();
			}
		}

		/// <exception cref="InvalidOperationException" />
		public static void AnyNotNull(string message, params object[] values)
		{
			if (values == null || values.All(x => x == null))
			{
				throw new InvalidOperationException(message);
			}
		}

		/// <exception cref="InvalidOperationException" />
		public static T NotNull<T>(T value, string message = null)
		{
			if (value == null)
			{
				throw (message == null ? new InvalidOperationException() : new InvalidOperationException(message));
			}

			return value;
		}

		/// <exception cref="ArgumentNullException" />
		public static string NotNullNotEmpty(string value, string message = null)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw (message == null ? new InvalidOperationException() : new InvalidOperationException(message));
			}

			return value;
		}

		public static void CollectionNotNullOrNotEmpty<T>(IEnumerable<T> value, string argumentName, string message = null)
		{
			if (value == null)
			{
				throw new ArgumentNullException(argumentName, message);
			}

			if (!value.Any())
			{
				throw new ArgumentException(argumentName, message);
			}
		}

		public static void EnsureEnumValue<TEnum>(object value)
			where TEnum : struct, Enum
		{
			var result = (TEnum)value;
		}
	}
}
