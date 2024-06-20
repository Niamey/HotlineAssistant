import CardModel from '../../../card/card.model';
import { CardDetailResponseInterface } from '../../../../interfaces/responses/card/detail';
import { CardInterface } from '../../../../interfaces/card';

export class CardDetailResponseModel implements CardDetailResponseInterface {
  constructor (data: CardInterface) {
    this.item = new CardModel(data);
  }

  public item: CardModel;
}
