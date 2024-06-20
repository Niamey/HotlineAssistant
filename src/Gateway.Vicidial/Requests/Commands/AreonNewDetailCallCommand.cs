using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.Requests.Commands
{
	public class AreonNewDetailCallCommand : IRequest<AreonNewDetailCallResponseViewModel>
	{
		public string Event { get; set; }
		
		public string Line { get; set; }
		
		public string Initiator { get; set; }
		
		public string Uniqueid { get; set; }
		
		public string Closecallid { get; set; }
		
		public string Call_type { get; set; }

		[BindRequired]
		public string Number_a { get; set; }
		
		public string Number_b { get; set; }
		
		public string Operator_login { get; set; }
		
		public string Operator_email { get; set; }
		
		public string Guid { get; set; }
		
		public string Current_time { get; set; }
	}
}

