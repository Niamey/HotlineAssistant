using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.Services.Enums;
using Vostok.Hotline.Api.Services.Models;

namespace Vostok.Hotline.Api.Services.Responses.Messenger
{
	internal class MessageResponseModel
	{
		public string MessageId { get; set; }
		public string ExternalId { get; set; }
		public MessageResponseStatusModel Status { get; set; }
		public MessageResponseStatusesModel Statuses { get; set; }
	}
}
