using System;
using System.Linq;
using Newtonsoft.Json;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Core.Common.GlobalContext;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Loggers.Abstractions;

namespace Vostok.Hotline.Core.Common.Conversions
{
	public class HotlineStringEnumConverter : JsonConverter
	{
		private readonly Lazy<ILoggingService> _logger;

		public HotlineStringEnumConverter()
		{
			_logger = new Lazy<ILoggingService>(() =>
			{
				var type = this.GetType();
				var loggerType = typeof(ILoggingService<>).MakeGenericType(type);

				return GlobalHttpContextAccessor.Current.RequestServices.GetService(loggerType) as ILoggingService;
			});
		}

		public override bool CanConvert(Type objectType)
		{
			Type type = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
			return type.IsEnum;
		}

		private bool IsNullableType(Type type)
		{
			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool isNullable = IsNullableType(objectType);
			Type enumType = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;
			string[] names = Enum.GetNames(enumType);

			if (reader.TokenType == JsonToken.String)
			{
				var enumText = reader.Value.ToString();				
				if (!string.IsNullOrEmpty(enumText))
				{				
					string match = names
						.Where(n => string.Equals(n, enumText, StringComparison.OrdinalIgnoreCase))
						.FirstOrDefault();

					if (match != null)
						return Enum.Parse(enumType, match);
					else
					{
						var enumMemberValue = EnumHelper.GetEnumMemberValue(objectType, reader.Value);
						match = names.Where(n => string.Equals(n, enumMemberValue, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

						if (match != null)
							return Enum.Parse(enumType, match);
						else
							_logger.Value.LogCritical($"Enum item {enumText} in {enumType} not found.", new { enumText });
					}
				}
			}

			if (!isNullable)
			{
				string defaultName = names
					.Where(n => string.Equals(n, UndefinedEnum.Undefined.ToString("G"), StringComparison.OrdinalIgnoreCase))
					.FirstOrDefault();
				if (defaultName == null)
				{
					string message = $"Cannot find {UndefinedEnum.Undefined:G} in {enumType.Name}";
					_logger.Value.LogCritical(message);

					throw new Exception(message);
				}

				return Enum.Parse(enumType, defaultName);
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString());
		}
	}
}
