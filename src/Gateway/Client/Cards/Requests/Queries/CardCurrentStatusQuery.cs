using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Queries
{
	public class CardCurrentStatusQuery : IRequest<CardCurrentStatusViewModel>
	{
		[BindRequired]
		public long? CardId { get; set; }
	}
}
