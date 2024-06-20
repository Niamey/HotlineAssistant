import CardBalanceModel from '../../../card/balance/cardBalance.model';
import { CardBalanceResponseInterface } from '../../../../interfaces/responses/card/balance';
import { CardBalanceInterface } from '../../../../interfaces/card/balance';

export class CardBalanceResponseModel implements CardBalanceResponseInterface {
  constructor (data: CardBalanceInterface) {
    this.item = new CardBalanceModel(data);
  }

  public item: CardBalanceModel;
}
