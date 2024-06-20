using Vostok.Hotline.Api.Retail.Responses.Classifiers.Enums;

namespace Vostok.Hotline.Api.Retail.Requests.Commands.Classifiers
{
	internal class SetClassifierCommand
	{
		internal SetClassifierCommand(ClassifierRetailTypeEnum type, long entityId, string classifierCode, string classifierValue, string comment)
		{
			EntityType = type.ToString().ToUpper();
			EntityId = entityId;
			ClassifierCode = classifierCode;
			ClassifierValue = classifierValue;
			Comment = comment;
		}

		public string EntityType { get; set; }
		public long EntityId { get; set; }
		public string ClassifierCode { get; set; }
		public string ClassifierValue { get; set; }
		public string Comment { get; set; }
	}
}
