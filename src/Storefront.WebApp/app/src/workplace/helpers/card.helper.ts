import CardModel from '../models/card/card.model';

export class CardHelper {
  public static sortCardsByStatus (cards: CardModel[]) : CardModel[]  {
    const sortArr = (a: CardModel, b: CardModel) => (a.status! > b.status! ? 1 : -1);
    return cards.sort(sortArr);
  }
}