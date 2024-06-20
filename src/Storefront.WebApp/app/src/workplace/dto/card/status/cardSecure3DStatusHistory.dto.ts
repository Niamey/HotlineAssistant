import { CardSecure3DStatusHistoryEnum } from '../../../enums/card/cardSecure3DStatusHistory.enum';
import { CardSecure3DStatusHistoryInterface } from '../../../interfaces/card/status';

export class CardSecure3DStatusHistoryDto implements CardSecure3DStatusHistoryInterface {
  currentStatus?: CardSecure3DStatusHistoryEnum;
  dateChangeStatus?: string;
  comment?: string;
}
