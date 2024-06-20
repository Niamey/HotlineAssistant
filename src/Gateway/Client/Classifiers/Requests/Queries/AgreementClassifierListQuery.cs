using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums.Classifiers;
using Vostok.Hotline.Gateway.Client.Classifiers.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Classifiers.Requests.Queries
{
	public class AgreementClassifierListQuery : IRequest<SearchRowsResponseRowModel<ClassifierViewModel>>
	{
		[BindRequired]
		public long? EntityId { get; set; }
	}
}
