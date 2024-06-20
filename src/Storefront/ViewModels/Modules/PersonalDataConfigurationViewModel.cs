using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Storefront.ViewModels.Modules
{
	public class PersonalDataConfigurationViewModel : ResponseBaseModel
	{        
		/// <summary>
		/// 
		/// </summary>
		public PersonDataTypeEnum PersonDataType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int PersonDataValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string PersonDataTitle { get; set; }
	}
}
