using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Storefront.ViewModels.Modules;

namespace Vostok.Hotline.Storefront.Services.Menu
{
	public class PersonalDataService
	{
		public PersonalDataService()
		{
		}

		public async Task<SearchRowsResponseRowModel<PersonalDataConfigurationViewModel>> GetConfigurationAsync(LocalizationEnum value, CancellationToken cancellationToken)
		{
			return new SearchRowsResponseRowModel<PersonalDataConfigurationViewModel>
			{
				Rows = new List<PersonalDataConfigurationViewModel>
				{
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Personal, PersonDataTitle = "ПІБ", PersonDataValue = 1 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Personal, PersonDataTitle = "Дата народження", PersonDataValue = 2 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Personal, PersonDataTitle = "Кодове слово", PersonDataValue = 3 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Personal, PersonDataTitle = "Фінансовий номер", PersonDataValue = 4 },
					
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Identification, PersonDataTitle = "ІПН", PersonDataValue = 5 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Identification, PersonDataTitle = "Номер документа", PersonDataValue = 6 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Identification, PersonDataTitle = "Адреса місця реєстрації", PersonDataValue = 7 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Identification, PersonDataTitle = "Дата видачі документа", PersonDataValue = 8 },
					
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Application, PersonDataTitle = "Код реєстрації в моб. додатку aбо в онлайн банку", PersonDataValue = 9 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Application, PersonDataTitle = "Пароль входу в онлайн банк", PersonDataValue = 10 },
					
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Card, PersonDataTitle = "Номер картки", PersonDataValue = 13 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Card, PersonDataTitle = "Термін дії картки", PersonDataValue = 14 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Card, PersonDataTitle = "CVV / CVC", PersonDataValue = 15 },

					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Code, PersonDataTitle = "SecureCode", PersonDataValue = 11 },
					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Code, PersonDataTitle = "LOOKUP", PersonDataValue = 12 },

					new PersonalDataConfigurationViewModel{ PersonDataType = PersonDataTypeEnum.Other, PersonDataTitle = "Код підтвердження оцифровки картки в системі електронних платежів (Токен)", PersonDataValue = 16 }
				}
			};
		}
	}
}