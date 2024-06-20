using System.ComponentModel.DataAnnotations;
using System.Web;
using Vostok.Hotline.Api.CRM.Responses.Enums;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Api.CRM.Responses.SearchRequests
{
	public class CounterpartRequest : SearchPagedRequestBaseModel
	{
		[Required]
		public CounterpartSearchCodeCrmEnum? Code { get; set; }

		/// <summary>
		/// Search criteria.
		/// </summary>
		[Required]
		public string Criteria { get; set; }

		/// <summary>
		/// Counterparty search type.
		/// </summary>
		public CounterpartSearchTypeCrmEnum Type { get; set; } = CounterpartSearchTypeCrmEnum.None;

		public override string GetUrlQuery()
		{			
			var parameters = HttpUtility.ParseQueryString(base.GetUrlQuery());
			
			if(Code != null)
				parameters[nameof(Code)] = Code.ToString();

			parameters[nameof(Criteria)] = Criteria;
			parameters[nameof(Type)] = Type.ToString();

			return parameters.ToString();
		}
	}
}
