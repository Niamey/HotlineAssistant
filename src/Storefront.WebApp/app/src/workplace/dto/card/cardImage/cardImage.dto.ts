import { CardImageInterface } from '../../../interfaces/card/cardImage/cardImage.interface';

export class CardImageDto implements CardImageInterface {
  cardCode?: string = '';
  frontUrl?: string = '';
}
