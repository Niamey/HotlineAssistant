using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Vostok.HotLineAssistant.Common;
using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.Application.Extensions
{
	public static class AutoMapperExtensions
	{
		public static PageResponse<TDestination> MapPagedResponse<TSource, TDestination>(this IMapper mapper, PageResponse<TSource> source)
		{
			Assure.ArgumentNotNull(source, nameof(source));

			var options = new PageOptions(source.Current, source.Size);
			var items = mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source.Items);

			return new PageResponse<TDestination>(options, source.Total, items.ToArray());
		}
	}
}