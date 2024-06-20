import { CardLimitStatusDto } from '../../../dto/card/limits';

export class CardLimitStatusModel extends CardLimitStatusDto {
  constructor (dto: CardLimitStatusDto) {
    super();
    Object.assign(this, dto);
  }
}
