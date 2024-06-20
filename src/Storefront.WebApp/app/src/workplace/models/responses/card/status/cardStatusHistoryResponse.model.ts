import { CardStatusHistoryModel } from '../../../card/status';
import { CardStatusHistoryResponseInterface } from '../../../../interfaces/responses/card/status';
import { CardStatusHistoryInterface } from '../../../../interfaces/card/status';

export class CardStatusHistoryResponseModel implements CardStatusHistoryResponseInterface {
  constructor (data: CardStatusHistoryInterface) {
    this.item = new CardStatusHistoryModel(data);
  }

  public item: CardStatusHistoryModel;
}
