import { CardTokenDto } from '../../../dto/card/token';

export class CardTokenModel {
  tokenId?: string;
  constructor (dto: CardTokenDto) {
    Object.assign(this, dto);
  }
}
