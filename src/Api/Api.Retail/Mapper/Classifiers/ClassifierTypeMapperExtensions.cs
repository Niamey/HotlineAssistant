using Vostok.Hotline.Api.Retail.Responses.Classifiers.Enums;
using Vostok.Hotline.Core.Common.Enums.Classifiers;

namespace Vostok.Hotline.Api.Retail.Mapper.Classifiers
{
	internal static class ClassifierTypeMapperExtensions
	{
		public static ClassifierRetailTypeEnum ToClassifierRetailTypeEnum(this ClassifierTypeEnum response)
		   => response switch
		   {
			   ClassifierTypeEnum.Client => ClassifierRetailTypeEnum.Client,
			   ClassifierTypeEnum.Agreement => ClassifierRetailTypeEnum.Agreement,
			   ClassifierTypeEnum.Accessor => ClassifierRetailTypeEnum.Accessor,
			   _ => ClassifierRetailTypeEnum.Undefined
		   };
	}
}
