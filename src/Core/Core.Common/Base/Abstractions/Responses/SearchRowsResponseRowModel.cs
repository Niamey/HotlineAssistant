using System.Collections.Generic;

namespace Vostok.Hotline.Core.Common.Base.Abstractions.Responses
{
	public class SearchRowsResponseRowModel<TRowViewModel> : ResponseBaseModel
		where TRowViewModel : ResponseBaseModel
	{
		public List<TRowViewModel> Rows { get; set; } = new List<TRowViewModel>();
	}
}
