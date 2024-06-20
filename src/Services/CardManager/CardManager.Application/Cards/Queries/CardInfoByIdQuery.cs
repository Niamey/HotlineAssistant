using MediatR;
using Vostok.HotLineAssistant.CardManager.Application.Cards.Dtos;

namespace Vostok.HotLineAssistant.CardManager.Application.Cards.Queries
{
	public class CardInfoByIdQuery : IRequest<CardInfoDto>
	{
		public int Id { get; set; }
	}
}
