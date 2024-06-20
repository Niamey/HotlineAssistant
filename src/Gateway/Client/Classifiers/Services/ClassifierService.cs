using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Extensions;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Classifiers;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Classifiers.Mapper;
using Vostok.Hotline.Gateway.Client.Classifiers.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Classifiers.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Classifiers.Services
{
    public class ClassifierService
    {
		private readonly ClassifierApiManager _classifierManager;

		public ClassifierService(ClassifierApiManager classifierManager)
		{
			_classifierManager = classifierManager;
		}

		public async Task<SearchRowsResponseRowModel<ClassifierViewModel>> GetAgreementClassifierListAsync(AgreementClassifierListQuery request, CancellationToken cancellationToken)
		{
			var result = await _classifierManager.GetClassifiersAsync(ClassifierTypeEnum.Agreement, request.EntityId.Value, cancellationToken);
			if (result != null)
				return result.GetActiveLocks().ToClassifierListViewModel();
			else
				throw new NotFoundRequestException();
		}
	}
}
