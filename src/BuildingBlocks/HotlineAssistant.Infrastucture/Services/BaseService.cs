using Solar.Models.Models;
using Solar.Models.Models.Params;
using Solar.SDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.Infrastucture.Services
{
	public class BaseService
	{
		private readonly ISolarSdkFacade _facade;
		public BaseService(ISolarSdkFacade facade)
		{
			_facade = facade ?? throw new ArgumentNullException(nameof(facade));
		}

		protected async Task<IEnumerable<Accessor>> GetAccessor(Options options)
		{
			return await _facade.GetAccessorListAsync(options);
		}

		protected async Task<Agreement> GetAgreement(AgreementOptions options)
		{
			return await _facade.GetAgreementAsync(options);
		}

		protected async Task<IEnumerable<Agreement>> GetAgreements(AgreementOptions options)
		{
			return await _facade.GetAgreementListAsync(options);
		}

		protected async Task<Balance> GetAgreementBalance(AgreementOptions options)
		{
			return await _facade.GetAgreementBalanceAsync(options);
		}

		protected async Task<IEnumerable<Balance>> GetClientBalance(ClientOptions options)
		{
			return await _facade.GetClientBalanceAsync(options);
		}
	}
}
