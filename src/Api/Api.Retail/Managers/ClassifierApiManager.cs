using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.ConstantNames;
using Vostok.Hotline.Api.Retail.Extensions;
using Vostok.Hotline.Api.Retail.Mapper.Classifiers;
using Vostok.Hotline.Api.Retail.Models.Classifiers;
using Vostok.Hotline.Core.Common.Enums.Classifiers;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class ClassifierApiManager
	{
		private readonly IClassifierApiService _classifierApiService;
		public ClassifierApiManager(IClassifierApiService classifierApiService)
		{
			_classifierApiService = classifierApiService;
		}

		public async Task<ClassifierCollectionApiModel> GetClassifiersAsync(ClassifierTypeEnum type, long entityId, CancellationToken cancellationToken)
		{
			var retailType = type.ToClassifierRetailTypeEnum();
			return await _classifierApiService.GetClassifiersAsync(retailType, entityId, cancellationToken);
		}

		public async Task<StatusCommandApiViewModel> SetClassifierAsync(ClassifierTypeEnum type, long entityId, string classifierCode, string classifierValue, string comment, CancellationToken cancellationToken)
		{
			var retailType = type.ToClassifierRetailTypeEnum();
			return await _classifierApiService.SetClassifierAsync(retailType, entityId, classifierCode, classifierValue, comment, cancellationToken);
		}

		public async Task<bool> Has3dsForCardAsync(long cardId, CancellationToken cancellationToken)
		{
			var result = await GetClassifiersAsync(ClassifierTypeEnum.Accessor, cardId, cancellationToken);
			return result?.GetActiveClassifier(ClassifierConstant.Code3DS) != null;
		}
	}
}
