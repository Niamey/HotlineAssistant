using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Core.Common.Models.HttpClientModels
{
	public class HttpClientHeaderCollectionModel : List<HttpClientHeaderModel>
	{
		public void Add(string name, string value)
		{
			this.Add(new HttpClientHeaderModel
			{
				Name = name,
				Value = value
			});
		}
	}
}