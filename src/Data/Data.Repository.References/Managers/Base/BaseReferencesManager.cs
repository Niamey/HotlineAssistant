using System;
using Vostok.Hotline.Data.EF.DbStorage.References;
using Vostok.Hotline.Data.Repository.Managers.Base;

namespace Vostok.Hotline.Data.Repository.References.Managers.Base
{
	public class BaseReferencesManager : CarcaseManager<HotlineReferencesContext>
	{
		public BaseReferencesManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}
	}
}
