using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.Services.Abstractions;

namespace Vostok.Hotline.Api.Services.Models
{
	public class MessagePackageCollectionApiModel<T> : List<IMessageApiModel>
	{
	}
}
