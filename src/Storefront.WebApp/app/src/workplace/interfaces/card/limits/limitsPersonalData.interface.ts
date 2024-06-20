import { PersonDataTypeEnum } from '../../../enums/modules';

export interface LimitsPersonalDataInterface {
  personDataType?: PersonDataTypeEnum;
  personDataValue?: number;
  personDataTitle?: string;
}
