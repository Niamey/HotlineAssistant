using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Services.Models.CardService
{
	public class CardImageApiModel : ResponseBaseModel
	{
		public string FrontUrl { get; set; }
		public string BackUrl { get; set; }
	}
}
