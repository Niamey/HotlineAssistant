using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Core
{
	public class EnvironmentSetting : BusinessEntityBase
	{
		public EnvironmentSetting()
		{			
		}

		public string EnvironmentKey { get; set; }
		public string EnvironmentValue { get; set; }
	}
}
