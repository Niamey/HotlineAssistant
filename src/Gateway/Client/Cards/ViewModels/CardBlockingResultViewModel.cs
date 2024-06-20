using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Cards.ViewModels
{
	public class CardBlockingResultViewModel : ResponseBaseModel
	{
		public StatusCommandViewModel Status { get; set; }
		public CardBlockingOperationStatusCollectionViewModel OperationStatuses { get; set; }
		public string ResultForOperator { get; set; }
	}
}
