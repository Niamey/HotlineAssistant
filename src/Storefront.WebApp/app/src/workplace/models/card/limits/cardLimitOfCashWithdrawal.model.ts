import { CardLimitOfCashWithdrawalDto } from '../../../dto/card/limits';

export class CardLimitOfCashWithdrawalModel extends CardLimitOfCashWithdrawalDto {
  constructor (dto: CardLimitOfCashWithdrawalDto) {
    super();
    Object.assign(this, dto);
  }
}
