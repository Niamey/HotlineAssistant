using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Data.Contexts;
using Vostok.Hotline.Data.EF.Configurations;

namespace Vostok.Hotline.Data.EF.DbStorage.Core
{
	public class HotlineCoreContextFactory : ContextBaseFactory<HotlineCoreContext>
	{
		protected override string Context => "HotlineContext";

		protected override string ContextPassword => "HotlineContext.Password";

		protected override string ProfileName => "Data.EF";

		protected override string GetConnectionString(IConfigurationRoot configuration)
		{
			return new HotlineDbConnectionConfig(new HotlineEnvironment(configuration)).ConnectionString;
		}
	}
}