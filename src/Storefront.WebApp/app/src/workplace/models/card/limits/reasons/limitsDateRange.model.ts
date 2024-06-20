import { LimitsDateRangeDto } from '../../../../dto/card/limits';

export class LimitsDateRangeModel extends LimitsDateRangeDto {
  constructor (dto: LimitsDateRangeDto) {
    super();
    Object.assign(this, dto);
  }
}
