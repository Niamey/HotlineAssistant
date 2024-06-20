import { PersonDataTypeEnum } from '../../enums/modules';
import { PersonalDataInterface } from '../../interfaces/modules';

export class PersonalDataDto implements PersonalDataInterface {
  personDataValue?: number
  personDataTitle?: string
  personDataType?: PersonDataTypeEnum
}
