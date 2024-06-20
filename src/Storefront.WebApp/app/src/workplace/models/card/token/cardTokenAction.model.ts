import { CardTokenActionDto } from '../../../dto/card/token';

export class CardTokenActionModel {
  constructor (dto: CardTokenActionDto) {
    Object.assign(this, dto);
  }
}
