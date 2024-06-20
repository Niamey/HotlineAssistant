import CardImageModel from '../../../card/cardImage/cardImage.model';
import { CardImageResponseInterface } from '../../../../interfaces/responses/card/cardImage/cardImageResponse.interface';
import { CardImageInterface } from '../../../../interfaces/card/cardImage/cardImage.interface';

export class CardImageResponseModel implements CardImageResponseInterface {
  constructor (data: CardImageInterface) {
    this.item = new CardImageModel(data);
  }

  public item: CardImageModel;
}
