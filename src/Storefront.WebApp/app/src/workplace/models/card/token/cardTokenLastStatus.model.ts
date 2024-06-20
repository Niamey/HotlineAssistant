import { CardTokenLastStatusDto } from '../../../dto/card/token';

export class CardTokenLastStatusModel {
  constructor (dto: CardTokenLastStatusDto) {
    Object.assign(this, dto);
  }
}
