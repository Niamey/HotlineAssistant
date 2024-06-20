import { CardSmsInfoHistoryStatusModel } from '../../../card/services';
import { CardSmsInfoHistoryStatusInterface } from '../../../../interfaces/card/services';
import { CardSmsInfoHistoryStatusResponseInterface } from '../../../../interfaces/responses/card/services';

export class CardSmsInfoHistoryStatusResponseModel implements CardSmsInfoHistoryStatusResponseInterface {
  constructor (data: CardSmsInfoHistoryStatusInterface) {
    this.item = new CardSmsInfoHistoryStatusModel(data);
  }

  public item: CardSmsInfoHistoryStatusModel;
}
