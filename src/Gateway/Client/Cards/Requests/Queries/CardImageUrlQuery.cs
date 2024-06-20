using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Queries
{
	public class CardImageUrlQuery : IRequest<CardImageUrlViewModel>
	{
		[BindRequired]
		public string CardCode { get; set; }
	}
}
