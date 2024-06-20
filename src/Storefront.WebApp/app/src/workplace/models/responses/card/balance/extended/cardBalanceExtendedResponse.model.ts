import CardBalanceExtendedModel from '../../../../card/balance/extended/cardBalanceExtended.model';
import { CardBalanceExtendedResponseInterface } from '../../../../../interfaces/responses/card/balance/extended/cardBalanceExtendedResponse.interface';
import { CardBalanceExtendedInterface } from '../../../../../interfaces/card/balance/extended/cardBalanceExtended.interface';

export class CardBalanceExtendedResponseModel implements CardBalanceExtendedResponseInterface {
  constructor (data: CardBalanceExtendedInterface) {
    this.item = new CardBalanceExtendedModel(data);
  }

  public item: CardBalanceExtendedModel;
}
