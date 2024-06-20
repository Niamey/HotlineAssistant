import { CardCurrentStatusDto } from '../../../dto/card/status';

export class CardCurrentStatusModel extends CardCurrentStatusDto {
  constructor (dto: CardCurrentStatusDto) {
    super();
    Object.assign(this, dto);
  }
}
