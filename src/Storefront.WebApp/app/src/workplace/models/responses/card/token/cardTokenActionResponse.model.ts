import { CardTokenActionModel } from '../../../card/token';
import { CardTokenActionResponseInterface } from '../../../../interfaces/responses/card/token';
import { CardTokenActionInterface } from '../../../../interfaces/card/token';

export class CardTokenActionResponseModel implements CardTokenActionResponseInterface {
  constructor (data: CardTokenActionInterface) {
    this.item = new CardTokenActionModel(data);
  }

  public item: CardTokenActionModel;
}
