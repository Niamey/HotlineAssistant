using System.Collections.Generic;

namespace Vostok.Hotline.Core.Common.Base.Abstractions.Responses
{
	public class SearchPagedResponseRowModel<TRowViewModel> : SearchPagedResponseBaseModel
		where TRowViewModel : ResponseBaseModel
	{
		public List<TRowViewModel> Rows { get; set; } = new List<TRowViewModel>();
	}
}
