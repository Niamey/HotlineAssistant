using Vostok.Hotline.Api.Retail.Responses.Classifiers.Enums;
using Vostok.Hotline.Core.Common.Enums.Classifiers;

namespace Vostok.Hotline.Api.Retail.Mapper.Classifiers
{
	public static class ClassifierEntityTypeCodeMapperExtension
	{
		public static ClassifierTypeEnum ToClassifierTypeEnum(this ClassifierRetailTypeEnum response)
		=> response switch
		{
			ClassifierRetailTypeEnum.Client => ClassifierTypeEnum.Client,
			ClassifierRetailTypeEnum.Agreement => ClassifierTypeEnum.Agreement,
			ClassifierRetailTypeEnum.Accessor => ClassifierTypeEnum.Accessor,
			_ => ClassifierTypeEnum.Undefined
		};
	}
}
