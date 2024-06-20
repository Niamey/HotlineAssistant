using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Gateway.Client.Addresses.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Addresses.Requests.Queries
{
	public class AddressDetailQuery : IRequest<AddressViewModel>
	{
		[Required]
		public int AddressId { get; set; }
	}
}

