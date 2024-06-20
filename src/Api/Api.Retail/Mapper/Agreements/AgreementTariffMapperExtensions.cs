using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Retail.Mapper.Agreements
{
	internal static class AgreementTariffMapperExtensions
	{
		public static SearchRowsResponseRowModel<TariffApiModel> ToTariffListApiModel(this TariffCollectionApiModel response)
		{
			var result = new SearchRowsResponseRowModel<TariffApiModel>();

			foreach (var row in response)
			{
				result.Rows.Add(row);
			}

			return result;
		}
	}
}
