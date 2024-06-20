import CardModel from './../card.model';

export class CardSliderModel {
  public cardSlots: Array<CardModel> = [];
  public mainCardId?: number;

  constructor (cards:Array<CardModel>) {
    this.cardSlots = cards;
  }

  /*
  get cardInfo (): CardModel | undefined {
    if (this.cardSlots && this.cardSlots.length > 0) {
      return this.cardSlots[0];
    }
    return undefined;
  }
  */

  getCardDetail (cardId?: number): CardModel {
    return this.cardSlots.filter(x => x.cardId === cardId)[0];
  }
}
