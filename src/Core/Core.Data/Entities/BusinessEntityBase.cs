using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vostok.Hotline.Core.Data.Entities
{
	public abstract class BusinessEntityBase : EntityBase, IBusinessEntity
	{
		[Column("version")]
		[Timestamp]
		public byte[] Version { get; set; }

		[Column("createdOn")]
		public DateTime CreatedOn { get; set; }

		[Column("updatedOn")]
		public DateTime UpdatedOn { get; set; }

		[Column("sessionId")]
		public Guid? SessionId { get; set; }
	}
}