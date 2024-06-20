using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Vostok.Hotline.Core.Common.Extensions
{
	public static class PropertiesByAttributesExtensions
	{
		private static object _syncGetPropertiesByAttribute = new object();
		private static object _syncGetCollectionPropertiesByAttribute = new object();

		private static Dictionary<string, Dictionary<string, string>> _dictionary = new Dictionary<string, Dictionary<string, string>>();

		public static Dictionary<string, string> GetPropertiesByAttribute(this Type typeClass, Type typeAttribute)
		{
			string key = $"GetPropertiesByAttribute.{typeClass.FullName}--{typeAttribute.FullName}";

			Dictionary<string, string> result;
			if (_dictionary.ContainsKey(key))
				result = _dictionary[key];
			else
			{
				if (!_dictionary.ContainsKey(key))
				{
					lock (_syncGetPropertiesByAttribute)
					{
						if (!_dictionary.ContainsKey(key))
						{
							result = new Dictionary<string, string>();
							FillPropertiesByAttribute(typeClass, typeAttribute, result);

							if (!_dictionary.ContainsKey(key))
								_dictionary.Add(key, result);
						}
						else
						{
							result = _dictionary[key];
						}
					}
				}
				else
				{
					result = _dictionary[key];
				}
			}

			return result;
		}

		public static Dictionary<string, string> GetCollectionPropertiesByAttribute(this Type typeClass, Type[] typeAttribute)
		{
			var result = new Dictionary<string, string>();
			foreach (var type in typeAttribute)
			{
				var typeResult = typeClass.GetCollectionPropertiesByAttribute(type);

				result.Merge(typeResult);
			}

			return result;
		}

		public static Dictionary<string, string> GetCollectionPropertiesByAttribute(this Type typeClass, Type typeAttribute)
		{
			string key = $"GetCollectionPropertiesByAttribute.{typeClass.FullName}--{typeAttribute.FullName}";

			Dictionary<string, string> result;
			if (_dictionary.ContainsKey(key))
				result = _dictionary[key];
			else
			{
				if (!_dictionary.ContainsKey(key))
				{
					lock (_syncGetCollectionPropertiesByAttribute)
					{
						if (!_dictionary.ContainsKey(key))
						{
							result = new Dictionary<string, string>();
							FillCollectionPropertiesByAttribute(typeClass, typeAttribute, result);

							if (!_dictionary.ContainsKey(key))
								_dictionary.Add(key, result);
						}
						else
						{
							result = _dictionary[key];
						}
					}
				}
				else
				{
					result = _dictionary[key];
				}
			}

			return result;
		}

		private static void FillCollectionPropertiesByAttribute(Type typeClass, Type typeAttribute, Dictionary<string, string> list, string root = "")
		{
			var properties = typeClass.GetProperties().Where(prop => (prop.IsDefined(typeAttribute, false) || prop.PropertyType.IsClass) && prop.PropertyType != typeof(JObject));
			foreach (var property in properties)
			{
				if (property.PropertyType.IsArray)
				{
					FillPropertiesByAttribute(property.PropertyType.GetEnumeratedType(), typeAttribute, list, $"{(!string.IsNullOrEmpty(root) ? $"{root}." : "")}{property.Name}");
				}
			}
		}

		private static void FillPropertiesByAttribute(Type typeClass, Type typeAttribute, Dictionary<string, string> list, string root = "")
		{
			var properties = typeClass.GetProperties().Where(prop => (prop.IsDefined(typeAttribute, false) || prop.PropertyType.IsClass) && prop.PropertyType != typeof(JObject));
			foreach (var property in properties)
			{
				if (property.PropertyType.IsClass)
				{
					if (property.PropertyType.IsArray)
					{
						FillPropertiesByAttribute(property.PropertyType.GetEnumeratedType(), typeAttribute, list, $"{(!string.IsNullOrEmpty(root) ? $"{root}." : "")}{property.Name}");
					}
					else
					{
						FillPropertiesByAttribute(property.PropertyType, typeAttribute, list, $"{(!string.IsNullOrEmpty(root) ? $"{root}." : "")}{property.Name}");
					}
				}
				else
				{
					list.Add((!string.IsNullOrEmpty(root) ? $"{root}." : "") + property.Name, (!string.IsNullOrEmpty(root) ? $"{root}." : "") + property.Name);
				}
			}
		}
	}
}
