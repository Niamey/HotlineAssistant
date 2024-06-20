import { CardBalanceDto } from '../../../dto/card/balance';

export default class CardBalanceModel extends CardBalanceDto {
  constructor (dto: CardBalanceDto) {
    super();
    Object.assign(this, dto);
  }
}
