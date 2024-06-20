import { LimitsPersonWhoCallsDto } from '../../../../dto/card/limits';

export class LimitsPersonWhoCallsModel extends LimitsPersonWhoCallsDto {
  constructor (dto: LimitsPersonWhoCallsDto) {
    super();
    Object.assign(this, dto);
  }
}
