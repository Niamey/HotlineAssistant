using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Storefront.Enums;

namespace Vostok.Hotline.Storefront.Requests.Commands
{
	public class SendFeedBackCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public string Login { get; set; }
		[BindRequired]
		public string FullName { get; set; }
		[BindRequired]
		public string Position { get; set; }
		[BindRequired]
		public string Email { get; set; }
		[BindRequired]
		public FeedBackRatingEnum? Rating { get; set; }
		[BindRequired]
		public string Message { get; set; }
	}
}
