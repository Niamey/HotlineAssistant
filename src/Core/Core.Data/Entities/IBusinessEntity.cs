using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Core.Data.Entities
{
	public interface IBusinessEntity : IEntity
	{
		byte[] Version { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}
