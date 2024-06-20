using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.Requests.Queries
{
	public class ChatterNewCallQuery: IRequest<ChatterNewCallResponseViewModel>
	{
		[BindRequired]
		public string FinancePhone { get; set; }
	}
}
