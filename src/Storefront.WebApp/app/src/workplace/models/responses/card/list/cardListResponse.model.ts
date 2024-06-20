import CardModel from '../../../card/card.model';
import { CardListResponseInterface } from '../../../../interfaces/responses/card/list';
import { CardInterface } from '../../../../interfaces/card';
import { CardDto } from '../../../../dto/card';

export class CardListResponseModel implements CardListResponseInterface {
  constructor (items: Array<CardInterface>) {
    this.rows = items.map((cardDto: CardDto) => new CardModel(cardDto));
  }

  public rows: Array<CardModel>;
}
