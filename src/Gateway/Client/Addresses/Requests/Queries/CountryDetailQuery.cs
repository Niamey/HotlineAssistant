using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Requests.Queries
{
	public class CountryDetailQuery : IRequest<CountryViewModel>
	{
		[Required]
		public int CountryId { get; set; }
	}
}
