import { CardTokenLastStatusModel } from '../../../card/token';
import { CardTokenLastStatusResponseInterface } from '../../../../interfaces/responses/card/token';
import { CardTokenLastStatusInterface } from '../../../../interfaces/card/token';

export class CardTokenLastStatusResponseModel implements CardTokenLastStatusResponseInterface {
  constructor (data: CardTokenLastStatusInterface) {
    this.item = new CardTokenLastStatusModel(data);
  }

  public item: CardTokenLastStatusModel;
}
