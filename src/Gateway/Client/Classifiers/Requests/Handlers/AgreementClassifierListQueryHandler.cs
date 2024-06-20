using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Classifiers.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Classifiers.Services;
using Vostok.Hotline.Gateway.Client.Classifiers.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Classifiers.Requests.Handlers
{
	public class AgreementClassifierListQueryHandler : IRequestHandler<AgreementClassifierListQuery, SearchRowsResponseRowModel<ClassifierViewModel>>
	{
		private readonly ClassifierService _classifierService;

		public AgreementClassifierListQueryHandler(ClassifierService classifierService)
		{
			_classifierService = classifierService;
		}

		public async Task<SearchRowsResponseRowModel<ClassifierViewModel>> Handle(AgreementClassifierListQuery request, CancellationToken cancellationToken)
		{
			return await _classifierService.GetAgreementClassifierListAsync(request, cancellationToken);
		}
	}
}
