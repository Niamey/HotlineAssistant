import { LimitsPersonalDataDto } from '../../../../dto/card/limits';

export class LimitsPersonalDataModel extends LimitsPersonalDataDto {
  constructor (dto: LimitsPersonalDataDto) {
    super();
    Object.assign(this, dto);
  }
}
