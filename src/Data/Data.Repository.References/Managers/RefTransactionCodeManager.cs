using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Vostok.Hotline.Data.EF.Entities.References;
using Vostok.Hotline.Data.Repository.References.Managers.Base;
using Vostok.Hotline.Data.Repository.References.Models.Responses;

namespace Vostok.Hotline.Data.Repository.References.Managers
{
	public class RefTransactionCodeManager : BaseReferencesManager
	{
		private readonly IMemoryCache _memoryCache;
		public RefTransactionCodeManager(IServiceProvider serviceProvider, IMemoryCache memoryCache)
			: base(serviceProvider)
		{
			_memoryCache = memoryCache;
		}

		public async Task<RefTransactionCodeResponseModel> GetByCodeAsync(string code, CancellationToken cancellationToken)
		{
			RefTransactionCodeResponseModel result = null;

			if (code == null)
			{
				return result;
			}

			if (!_memoryCache.TryGetValue(code, out result))
			{
				using (var transaction = TransactionFactory.CreateReadUncommitted())
				{
					result = await DbContext.RefTransactionCodes.Where(x => x.Code == code).Select(query => new RefTransactionCodeResponseModel
					{
						Code = query.Code,
						Rc = query.Rc,
						Description = query.Description
					}).FirstOrDefaultAsync(cancellationToken);
					
					transaction.Complete();
				};

				if (result != null)
				{
					_memoryCache.Set(code, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
				}
			}
			return result;		
		}		
	}
}
