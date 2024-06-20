using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public class EnumDto
	{
		public string Name { get; set; }

		public Enum Enum { get; set; }

		public IDictionary<Type, Attribute> Attributes { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public class EnumDto<TEnum> : EnumDto
		where TEnum : struct
	{
		public TEnum Value { get; set; }
	}

	/// <summary>
	/// Enum accelerator
	/// </summary>
	public class Enum<TEnum> : IEnumerable<EnumDto<TEnum>>
		where TEnum : struct
	{
		public static Enum<TEnum> Current { get; } = new Enum<TEnum>();

		private IList<EnumDto<TEnum>> _items { get; }

		private Enum()
		{
			_items = new List<EnumDto<TEnum>>();

			foreach (var name in Enum.GetNames(typeof(TEnum)))
			{
				var value = Enum.Parse(typeof(TEnum), name);

				var item = new EnumDto<TEnum>
				{
					Name = name,
					Enum = (Enum)value,
					Value = (TEnum)value
				};

				item.Attributes = typeof(TEnum)
					.GetField(item.Name)
					.GetCustomAttributes()
					.ToDictionary(x => x.GetType(), x => x);

				_items.Add(item);
			}
		}

		private bool TryGet<TAttribute>(Enum value, out TAttribute attribute)
			where TAttribute : Attribute
		{
			attribute = null;

			var item = _items.FirstOrDefault(x => x.Enum == value);
			if (item == null)
				return false;

			Attribute attr;
			if (!item.Attributes.TryGetValue(typeof(TAttribute), out attr))
				return false;

			attribute = (TAttribute)attr;

			return true;
		}

		private bool TryGet<TAttribute>(string value, out TAttribute attribute)
			where TAttribute : Attribute
		{
			attribute = null;

			var item = _items.FirstOrDefault(x => x.Name == value);
			if (item == null)
				return false;

			Attribute attr;
			if (!item.Attributes.TryGetValue(typeof(TAttribute), out attr))
				return false;

			attribute = (TAttribute)attr;

			return true;
		}

		#region IEnumerable

		public IEnumerator<EnumDto<TEnum>> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Static API

		/// <summary>
		/// Gets enum values as Enumerable
		/// </summary>
		public static IEnumerable<TEnum> AsEnumerable()
		{
			return Current.Select(x => x.Value);
		}

		/// <summary>
		/// Gets attributes dictionary
		/// </summary>
		public static IEnumerable<KeyValuePair<TEnum, TAttribute>> GetAttributes<TAttribute>()
			where TAttribute : Attribute
		{
			Func<EnumDto<TEnum>, TAttribute> selector = (x) =>
			{
				Attribute attribute;
				return x.Attributes.TryGetValue(typeof(TAttribute), out attribute) ? (TAttribute)attribute : null;
			};

			return Current
				.Select(x => new KeyValuePair<TEnum, TAttribute>(x.Value, selector(x)));
		}

		/// <summary>
		/// Gets enum names 
		/// </summary>
		public static IEnumerable<string> GetNames()
		{
			return Current.Select(x => x.Name);
		}

		/// <summary>
		/// Gets enum values;
		/// </summary>
		public static IEnumerable<Enum> GetValues()
		{
			return Current.Select(x => x.Enum);
		}

		/// <summary>
		/// Try get any attribute
		/// </summary>
		public static bool TryGetAttribute<TAttribute>(TEnum value, out TAttribute attribute)
			where TAttribute : Attribute
		{
			return Current.TryGet(value.ToString(), out attribute);
		}

		public static IEnumerable<TEnum> Parse(IEnumerable<string> names, bool ignoreCase = false)
		{
			return names
				.SelectMany(name => Current._items.Where(x => string.Compare(name, x.Name, ignoreCase) == 0))
				.Select(x => x.Value);
		}

		public static TEnum Parse(string name, bool ignoreCase = false)
		{
			var result = Parse(new[] { name }, ignoreCase).ToArray();
			if (result.Length == 0)
				throw new KeyNotFoundException(name);

			return result.FirstOrDefault();
		}

		public static TEnum Parse(Enum source)
		{
			return (TEnum)Enum.Parse(typeof(TEnum), source.ToString(), true);
		}

		#endregion
	}

	/// <summary>
	/// Enum accelerator extensions
	/// </summary>
	public static class EnumHelper
	{
		public static String GetEnumMemberValue(Type type, object value)
		{
			var member = type.GetTypeInfo().DeclaredMembers
				.FirstOrDefault(x => x.GetCustomAttribute<EnumMemberAttribute>(false)?.Value == value.ToString());

			return member?.Name;
		}

		public static Dictionary<Enum, string> GetEnumTranslation(Type enumType)
		{
			var result = EnumHelper.AsEnumerable(enumType).ToDictionary(x => x,
				x => EnumHelper.ToDisplayName(x));

			return result;
		}

		public static IEnumerable<Enum> AsEnumerable(Type enumType)
		{
			return Enum.GetValues(enumType).OfType<Enum>();
		}

		public static string ToDisplayName(Enum value)
		{
			try
			{
				var enumType = Nullable.GetUnderlyingType(value.GetType()) ?? value.GetType();
				var enumName = Enum.GetName(enumType, value) ?? string.Empty;
				var enumField = enumType.GetField(enumName);
				var enumFieldDisplayAttribute = enumField?.GetCustomAttribute<DisplayAttribute>();
				return enumFieldDisplayAttribute == null
					? enumName
					: enumFieldDisplayAttribute.Name;
			}
			catch
			{
				return "undefined";
			}
		}

		/// <summary>
		/// Gets DescriptionAttribute value
		/// </summary>
		public static string GetDescription<TEnum>(this TEnum value)
			where TEnum : struct
		{
			DescriptionAttribute attribute;

			return Enum<TEnum>.TryGetAttribute<DescriptionAttribute>(value, out attribute)
				? attribute.Description
				: value.ToString();
		}
	}
}
