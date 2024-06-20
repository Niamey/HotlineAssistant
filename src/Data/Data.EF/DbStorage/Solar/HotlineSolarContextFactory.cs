using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Data.Contexts;
using Vostok.Hotline.Data.EF.Configurations;

namespace Vostok.Hotline.Data.EF.DbStorage.Solar
{
	public class HotlineSolarContextFactory : ContextBaseFactory<HotlineSolarContext>
	{
		protected override string Context => "HotlineContext.Solar";

		protected override string ContextPassword => "HotlineContext.Solar.Password";

		protected override string ProfileName => "Data.EF";

		protected override string GetConnectionString(IConfigurationRoot configuration)
		{
			return new HotlineSolarDbConnectionConfig(new HotlineEnvironment(configuration)).ConnectionString;
		}
	}
}