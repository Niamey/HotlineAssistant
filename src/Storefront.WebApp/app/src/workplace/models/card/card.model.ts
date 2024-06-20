import { CardDto } from '../../dto/card';

export default class CardModel extends CardDto {
  constructor (dto: CardDto) {
    super();
    Object.assign(this, dto);
  }
}
