import { CardBalanceExtendedDto } from '../../../../dto/card/balance/extended';

export default class CardBalanceExtendedModel extends CardBalanceExtendedDto {
  constructor (dto: CardBalanceExtendedDto) {
    super();
    Object.assign(this, dto);
  }
}
