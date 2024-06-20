import { CardStatusHistoryDto } from '../../../dto/card/status';

export class CardStatusHistoryModel extends CardStatusHistoryDto {
  constructor (dto: CardStatusHistoryDto) {
    super();
    Object.assign(this, dto);
  }
}
