using System;
using Vostok.Hotline.Core.Documents.Abstractions;

namespace Vostok.Hotline.Document.Templates.Cards.Limits.Models
{
	public class ChangeRiskModel : ITemplateViewModel
	{
		public string AgreementNumber { get; set; }
		public string CardNumber{ get; set; }
		public decimal Limit { get; set; }
		public bool IsP2PLimited { get; set; }
		public DateTime? LimitSetDate { get; set; }
		public string Phone { get; set; }

	}
}
