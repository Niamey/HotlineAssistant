import { CardSecure3DStatusHistoryModel } from '../../../card/status';
import { CardSecure3DStatusHistoryResponseInterface } from '../../../../interfaces/responses/card/status';
import { CardSecure3DStatusHistoryInterface } from '../../../../interfaces/card/status';

export class CardSecure3DStatusHistoryResponseModel implements CardSecure3DStatusHistoryResponseInterface {
  constructor (data: CardSecure3DStatusHistoryInterface) {
    this.item = new CardSecure3DStatusHistoryModel(data);
  }

  public item: CardSecure3DStatusHistoryModel;
}
