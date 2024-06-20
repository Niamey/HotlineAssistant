import { PersonDataTypeEnum } from '../../../enums/modules';
import { LimitsPersonalDataInterface } from '../../../interfaces/card/limits';

export class LimitsPersonalDataDto implements LimitsPersonalDataInterface {
  personDataType?: PersonDataTypeEnum;
  personDataValue?: number;
  personDataTitle?: string = '';
}
