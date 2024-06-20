using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Countries.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Countries.Requests.Queries
{
	public class CountryListQuery : IRequest<SearchRowsResponseRowModel<CountryViewModel>>
	{
	}
}
