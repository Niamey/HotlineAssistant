using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
    public class SolarStmtEntryAgreement : EntityBase
	{
        public long AgreementId { get; set; }
		
		public virtual SolarAgreement Agreement { get; set; }
		
		public long StmtEntryId { get; set; }
		
		public virtual SolarStmtEntry StmtEntry { get; set; }
		
		public DateTime StmtDate { get; set; }
        public string ShowInStatement { get; set; }
    }
}
