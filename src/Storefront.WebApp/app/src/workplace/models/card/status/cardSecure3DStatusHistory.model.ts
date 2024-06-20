import { CardSecure3DStatusHistoryDto } from '../../../dto/card/status';

export class CardSecure3DStatusHistoryModel extends CardSecure3DStatusHistoryDto {
  constructor (dto: CardSecure3DStatusHistoryDto) {
    super();
    Object.assign(this, dto);
  }
}
