import { CardFullBalanceDto } from '../../../../dto/card/balance/full';

export default class CardFullBalanceModel extends CardFullBalanceDto {
  constructor (dto: CardFullBalanceDto) {
    super();
    Object.assign(this, dto);
  }
}
