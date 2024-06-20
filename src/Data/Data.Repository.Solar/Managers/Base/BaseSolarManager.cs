using System;
using Vostok.Hotline.Data.EF.DbStorage.Solar;
using Vostok.Hotline.Data.Repository.Managers.Base;

namespace Vostok.Hotline.Data.Repository.Solar.Managers.Base
{
	public class BaseSolarManager : CarcaseManager<HotlineSolarContext>
	{
		public BaseSolarManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}
	}
}
