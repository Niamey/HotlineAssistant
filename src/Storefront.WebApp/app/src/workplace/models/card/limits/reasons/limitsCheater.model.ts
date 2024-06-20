import { LimitsCheaterDto } from '../../../../dto/card/limits';

export class LimitsCheaterModel extends LimitsCheaterDto {
  // cardNumber?: string;

  constructor (dto: LimitsCheaterDto) {
    super();
    Object.assign(this, dto);
  }
}
