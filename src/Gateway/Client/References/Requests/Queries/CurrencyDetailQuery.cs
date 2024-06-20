using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Gateway.Client.References.ViewModels;

namespace Vostok.Hotline.Gateway.Client.References.Requests.Queries
{
	public class CurrencyDetailQuery : IRequest<CurrencyViewModel>
	{
		[Required]
		public string CurrencyCode { get; set; }
	}
}
