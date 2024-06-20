import { PersonalDataInterface } from '../../../interfaces/modules';

import { PersonDataTypeEnumHelper } from '../../../helpers/enumHelpers/personDataTypeEnum.helper';
import { PersonDataTypeEnum } from '../../../enums/modules';

export class PersonalDataListResponseModel {
  public rows: Array<any>;

  constructor (items: Array<PersonalDataInterface>) {
    // group by type
    const grouped = items.reduce((acc:any, val:any) => {
      const type:PersonDataTypeEnum = val.personDataType;
      acc[type] = acc[type] || [];
      acc[type].push(val);
      return acc;
    }, {});

    this.rows = Object.keys(grouped).map(type => {
      return {
        type: type,
        title: PersonDataTypeEnumHelper.getTypeName(<PersonDataTypeEnum>type),
        values: grouped[type].map((i:PersonalDataInterface) => {
          return {
            id: i.personDataValue,
            title: i.personDataTitle,
            value: false
          };
        })
      };
    });
  }
}
