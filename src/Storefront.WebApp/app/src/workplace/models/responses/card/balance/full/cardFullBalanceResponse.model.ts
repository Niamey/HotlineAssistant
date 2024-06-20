import CardFullBalanceModel from '../../../../card/balance/full/cardFullBalance.model';
import { CardFullBalanceResponseInterface } from '../../../../../interfaces/responses/card/balance/full/cardFullBalanceResponse.interface';
import { CardFullBalanceInterface } from '../../../../../interfaces/card/balance/full/cardFullBalance.interface';

export class CardFullBalanceResponseModel implements CardFullBalanceResponseInterface {
  constructor (data: CardFullBalanceInterface) {
    this.item = new CardFullBalanceModel(data);
  }

  public item: CardFullBalanceModel;
}
