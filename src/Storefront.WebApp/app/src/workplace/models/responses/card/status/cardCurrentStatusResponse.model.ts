import { CardCurrentStatusModel } from '../../../card/status';
import { CardCurrentStatusResponseInterface } from '../../../../interfaces/responses/card/status';
import { CardCurrentStatusInterface } from '../../../../interfaces/card/status';

export class CardCurrentStatusResponseModel implements CardCurrentStatusResponseInterface {
  constructor (data: CardCurrentStatusInterface) {
    this.item = new CardCurrentStatusModel(data);
  }

  public item: CardCurrentStatusModel;
}
