using Vostok.Hotline.Api.Retail.Models.Classifiers;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Classifiers.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Classifiers.Mapper
{
	public static class ClassifierMapperExtensions
	{
		public static ClassifierViewModel ToClassifierViewModel(this ClassifierApiModel response)
		{
			return new ClassifierViewModel
			{
				ClassifierName = response.Type.Name
			};
		}

		public static SearchRowsResponseRowModel<ClassifierViewModel> ToClassifierListViewModel(this ClassifierCollectionApiModel response)
		{
			var result = new SearchRowsResponseRowModel<ClassifierViewModel>();
			foreach (var row in response)
			{
				result.Rows.Add(row.ToClassifierViewModel());
			}

			return result;
		}
	}
}
