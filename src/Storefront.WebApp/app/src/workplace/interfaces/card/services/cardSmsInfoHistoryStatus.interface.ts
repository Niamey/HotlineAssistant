import { CardSmsInfoHistoryStatusEnum } from '../../../enums/card/cardSmsInfoHistoryStatus.enum';
export interface CardSmsInfoHistoryStatusInterface {
  currentStatus?: CardSmsInfoHistoryStatusEnum;
  currentDate?: string;
  comment?: string;
}
