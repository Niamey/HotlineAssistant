using Microsoft.Extensions.Configuration;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Data.Contexts;
using Vostok.Hotline.Data.EF.Configurations;

namespace Vostok.Hotline.Data.EF.DbStorage.References
{
	public class HotlineReferencesContextFactory : ContextBaseFactory<HotlineReferencesContext>
	{
		protected override string Context => "HotlineContext.References";

		protected override string ContextPassword => "HotlineContext.References.Password";

		protected override string ProfileName => "Data.EF";

		protected override string GetConnectionString(IConfigurationRoot configuration)
		{
			return new HotlineReferencesDbConnectionConfig(new HotlineEnvironment(configuration)).ConnectionString;
		}
	}
}