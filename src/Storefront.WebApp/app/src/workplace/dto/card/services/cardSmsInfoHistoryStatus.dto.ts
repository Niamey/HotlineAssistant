import { CardSmsInfoHistoryStatusEnum } from '../../../enums/card/cardSmsInfoHistoryStatus.enum';
import { CardSmsInfoHistoryStatusInterface } from '../../../interfaces/card/services/cardSmsInfoHistoryStatus.interface';

export class CardSmsInfoHistoryStatusDto implements CardSmsInfoHistoryStatusInterface {
  currentStatus?: CardSmsInfoHistoryStatusEnum;
  currentDate?: string;
  comment?: string;
}
