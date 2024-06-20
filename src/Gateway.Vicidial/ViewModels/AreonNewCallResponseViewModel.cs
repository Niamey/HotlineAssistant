using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Vicidial.ViewModels
{
	public class AreonNewCallResponseViewModel : ResponseBaseModel
	{
		/// <summary>
		/// ссылка_на_карточку_клиента_в_црм
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// айди_обращения_в_црм
		/// </summary>
		public Guid Guid { get; set; }
	}
}
