import { CardSmsInfoHistoryStatusDto } from '../../../dto/card/services';

export class CardSmsInfoHistoryStatusModel extends CardSmsInfoHistoryStatusDto {
  constructor (dto: CardSmsInfoHistoryStatusDto) {
    super();
    Object.assign(this, dto);
  }
}
