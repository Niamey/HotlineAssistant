using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Models.Classifiers;
using Vostok.Hotline.Api.Retail.Responses.Classifiers.Enums;

namespace Vostok.Hotline.Api.Retail.Abstractions
{
	public interface IClassifierApiService
	{
		Task<ClassifierCollectionApiModel> GetClassifiersAsync(ClassifierRetailTypeEnum type, long entityId, CancellationToken cancellationToken);
		Task<StatusCommandApiViewModel> SetClassifierAsync(ClassifierRetailTypeEnum type, long entityId, string classifierCode, string classifierValue, string comment, CancellationToken cancellationToken);
	}
}
