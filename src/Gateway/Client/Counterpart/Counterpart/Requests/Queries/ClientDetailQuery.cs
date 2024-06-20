using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries
{
	public class ClientDetailQuery : IRequest<ClientViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }
	}
}
