import { LimitsVictimDto } from '../../../../dto/card/limits';

export class LimitsVictimModel extends LimitsVictimDto {
  constructor (dto: LimitsVictimDto) {
    super();
    Object.assign(this, dto);
  }
}
