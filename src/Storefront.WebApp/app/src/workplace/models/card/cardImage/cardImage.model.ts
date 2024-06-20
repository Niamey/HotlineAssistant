import { CardImageDto } from '../../../dto/card/cardImage/cardImage.dto';

export default class CardImageModel extends CardImageDto {
  constructor (dto: CardImageDto) {
    super();
    Object.assign(this, dto);
  }
}