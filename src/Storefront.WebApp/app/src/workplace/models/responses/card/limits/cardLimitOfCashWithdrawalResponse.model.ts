import { CardLimitOfCashWithdrawalModel } from '../../../card/limits';
import { CardLimitOfCashWithdrawalInterface } from '../../../../interfaces/card/limits';
import { CardLimitOfCashWithdrawalResponseInterface } from '../../../../interfaces/responses/card/limits';

export class CardLimitOfCashWithdrawalResponseModel implements CardLimitOfCashWithdrawalResponseInterface {
  constructor (data: CardLimitOfCashWithdrawalInterface) {
    this.item = new CardLimitOfCashWithdrawalModel(data);
  }

  public item: CardLimitOfCashWithdrawalModel;
}
