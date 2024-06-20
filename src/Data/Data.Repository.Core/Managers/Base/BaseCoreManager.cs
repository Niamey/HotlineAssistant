using System;
using Vostok.Hotline.Data.EF.DbStorage.Core;
using Vostok.Hotline.Data.Repository.Managers.Base;

namespace Vostok.Hotline.Data.Repository.Core.Managers.Base
{
	public class BaseCoreManager : CarcaseManager<HotlineCoreContext>
	{
		public BaseCoreManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}
	}
}
