using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Documents.Abstractions;

namespace Vostok.Hotline.Document.Templates.Statements
{
	public class ClientStatementModel : ITemplateViewModel
	{
		public string ClientName { get; set; }
	}
}
