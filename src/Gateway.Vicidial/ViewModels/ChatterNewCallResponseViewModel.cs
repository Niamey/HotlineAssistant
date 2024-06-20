using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Vicidial.ViewModels
{
	public class ChatterNewCallResponseViewModel : ResponseBaseModel
	{
		public string Url { get; set; }
	}
}
