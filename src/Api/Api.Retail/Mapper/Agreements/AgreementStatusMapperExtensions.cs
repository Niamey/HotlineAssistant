using System;
using System.Linq;
using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Api.Retail.Responses.Agreements;
using Vostok.Hotline.Api.Retail.Responses.Agreements.Enums;
using Vostok.Hotline.Core.Common.Enums.Agreements;

namespace Vostok.Hotline.Api.Retail.Mapper.Agreements
{
	internal static class AgreementStatusMapperExtensions
	{
		public static AgreementStatusEnum ToAgreementStatusEnum(this AgreementStatusRetailEnum response)
		=> response switch
		{
			AgreementStatusRetailEnum.Active => AgreementStatusEnum.Active,
			AgreementStatusRetailEnum.Closed => AgreementStatusEnum.Closed,
			AgreementStatusRetailEnum.Frozen => AgreementStatusEnum.Frozen,
			AgreementStatusRetailEnum.Reserve => AgreementStatusEnum.Reserve,
			AgreementStatusRetailEnum.Suspended => AgreementStatusEnum.Suspended,
			_ => AgreementStatusEnum.Undefined
		};

		public static AgreementCollectionHistoryStatusApiModel ToAgreementCollectionHistoryStatusApiModel(this AgreementCollectionHistoryStatusResponseModel response)
		{
			var result = new AgreementCollectionHistoryStatusApiModel();

			foreach (var row in response.OrderByDescending(x => x.ValidFrom))
			{
				result.Add(row.ToAgreementHistoryStatusViewModel());
			}

			return result;
		}

		public static AgreementHistoryStatusApiModel ToAgreementHistoryStatusViewModel(this AgreementHistoryStatusResponseModel response)
		{
			var result = new AgreementHistoryStatusApiModel
			{
				CurrentStatus = response.Status.ToAgreementStatusEnum(),
				ModifyDate = response.AuditHistory?.ModifyDate ?? response.ValidFrom,
				Comment = response.AuditHistory?.Comment ?? response.Comment,
				IsEmployee = response.AuditHistory?.IsEmployee,
				SystemName = response.AuditHistory?.SystemName,
				UserLogin = response.AuditHistory?.UserLogin
			};
			return result;
		}
	}
}
