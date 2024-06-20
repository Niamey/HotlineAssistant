using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardImageUrlViewModel : ResponseBaseModel
	{
		public string CardCode { get; set; }
		public string FrontUrl { get; set; }
	}
}
