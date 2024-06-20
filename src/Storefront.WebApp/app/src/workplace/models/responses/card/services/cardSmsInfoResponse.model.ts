import { CardSmsInfoModel } from '../../../card/services';
import { CardSmsInfoResponseInterface } from '../../../../interfaces/responses/card/services';
import { CardSmsInfoInterface } from '../../../../interfaces/card/services';

export class CardSmsInfoResponseModel implements CardSmsInfoResponseInterface {
  constructor (data: CardSmsInfoInterface) {
    this.item = new CardSmsInfoModel(data);
  }

  public item: CardSmsInfoModel;
}
