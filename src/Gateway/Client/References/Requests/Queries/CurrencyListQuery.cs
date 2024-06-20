using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.References.ViewModels;

namespace Vostok.Hotline.Gateway.Client.References.Requests.Queries
{
	public class CurrencyListQuery : IRequest<SearchRowsResponseRowModel<CurrencyViewModel>>
	{
		[Required]
		public string[] CurrencyCodes { get; set; }
	}
}
