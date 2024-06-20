using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Data.Contexts;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.DbStorage
{
	public static class EntitySelfAuditor
	{
		public static void ApplyAudit(ISessionContext sessionContext, DbBaseContext context)
		{
			var entries = context.ChangeTracker.Entries();

			var queryAdded = entries.Where(x => x.State == EntityState.Added).Select(x => x.Entity).OfType<BusinessEntityBase>();
			if (queryAdded.Any())
			{
				foreach (var entry in queryAdded)
				{
					entry.CreatedOn = DateTime.Now;
					entry.UpdatedOn = DateTime.Now;
					entry.SessionId = sessionContext.SessionId;
				}
			}

			var queryModified = entries.Where(x => x.State == EntityState.Modified).Select(x => x.Entity).OfType<BusinessEntityBase>();
			if (queryModified.Any())
			{
				foreach (var entry in queryModified)
				{
					entry.UpdatedOn = DateTime.Now;
					entry.SessionId = sessionContext.SessionId;
				}
			}
		}
	}
}
