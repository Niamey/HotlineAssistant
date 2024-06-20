using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Countries.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Countries.Services;
using Vostok.Hotline.Gateway.Client.Countries.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Countries.Requests.Handlers
{
	public class CountryListQueryHandler : IRequestHandler<CountryListQuery, SearchRowsResponseRowModel<CountryViewModel>>
	{
		private readonly CountryService _countryService;
		public CountryListQueryHandler(CountryService countryService)
		{
			_countryService = countryService;
		}
		public async Task<SearchRowsResponseRowModel<CountryViewModel>> Handle(CountryListQuery request, CancellationToken cancellationToken)
		{
			return await _countryService.GetCountriesAsync(request, cancellationToken);
		}
	}
}
