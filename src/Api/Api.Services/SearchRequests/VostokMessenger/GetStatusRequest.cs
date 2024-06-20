using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Services.SearchRequests
{
	internal class GetStatusRequest
	{
		/// <summary>
		/// Унікальний код повідомлення (messageId);
		/// </summary>
		public string GuidId { get; set; }
		/// <summary>
		/// Зовнішній код повідомлення на стороні агента;
		/// </summary>
		public string ExternalId { get; set; }
		/// <summary>
		/// Унікальний код повідомлення Messanger.
		/// </summary>
		public long SmsGateId { get; set; } 
	}
}
