import { CardSecure3DStatusHistoryEnum } from '../../../enums/card/cardSecure3DStatusHistory.enum';

export interface CardSecure3DStatusHistoryInterface {
  currentStatus?: CardSecure3DStatusHistoryEnum;
  dateChangeStatus?: string;
  comment?: string;
}
