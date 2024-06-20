using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.Repository.Solar.Managers.Base;

namespace Vostok.Hotline.Data.Repository.Solar.Managers
{
	class AgreementManager : BaseSolarManager
	{
		public AgreementManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}

		public async Task<long[]> GetAgreementByClientAsync(long clientId, CancellationToken cancellationToken)
		{
			long[] result = null;
			using (var transaction = TransactionFactory.CreateReadUncommitted())
			{
				var projection = DbContext.SolarAgreements.Where(x => x.ClientId == clientId).Select(x => x.Id);
				result = await projection.ToArrayAsync(cancellationToken);

				transaction.Complete();
			};

			return result;
		}		
	}
}
