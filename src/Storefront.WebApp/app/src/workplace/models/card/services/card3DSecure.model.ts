import { Card3DSecureDto } from '../../../dto/card/services';

export class Card3DSecureModel extends Card3DSecureDto {
  constructor (dto: Card3DSecureDto) {
    super();
    Object.assign(this, dto);
  }
}
