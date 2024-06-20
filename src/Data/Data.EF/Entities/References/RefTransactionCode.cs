using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.References
{
    public class RefTransactionCode : EntityBase
	{
        public string Code { get; set; }
		public string Rc { get; set; }
		public string Description { get; set; }
    }
}
