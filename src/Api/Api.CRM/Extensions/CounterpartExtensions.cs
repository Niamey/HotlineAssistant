using System.Linq;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Core.Common.Enums.Counterparts;

namespace Vostok.Hotline.Api.CRM.Extensions
{
	public static class CounterpartExtensions
	{
		public static string GetEmail(this CounterpartApiModel model)
		{
			return model?.Contacts?.FirstOrDefault(x => x.ContactType == ContactTypeEnum.Email)?.Value;
		}
	}
}
