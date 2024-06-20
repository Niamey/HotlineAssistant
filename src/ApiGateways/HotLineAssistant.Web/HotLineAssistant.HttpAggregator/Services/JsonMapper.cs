using Newtonsoft.Json;
using System;

namespace Vostok.HotLineAssistant.HttpAggregator.Services
{
	public abstract class JsonMapper
	{
		protected virtual T DeserializeOrThrow<T>(string content)
		{
			if (string.IsNullOrEmpty(content))
				throw new ArgumentException("Value cannot be null or empty.", nameof(content));

			T deserialized;
			try
			{
				deserialized = JsonConvert.DeserializeObject<T>(content);
			}
			catch (JsonReaderException e)
			{
				throw new ArgumentException("Content is invalid JSON.", nameof(content), e);
			}

			if (deserialized == null)
				throw new ArgumentException("Content is invalid JSON.", nameof(content));

			return deserialized;
		}
	}
}