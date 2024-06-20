using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Data.Repository.Core.Models.Requests
{
	public class TravelSearchRequest : SearchPagedRequestBaseModel
	{
		[BindRequired]
		public long? SolarId { get; set; }

		public override string GetUrlQuery()
		{
			var parameters = HttpUtility.ParseQueryString(base.GetUrlQuery());
			parameters[nameof(SolarId)] = SolarId.ToString();

			return parameters.ToString();
		}
	}
}
