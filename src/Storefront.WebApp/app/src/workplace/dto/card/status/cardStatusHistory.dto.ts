import { CardStatusEnum } from '../../../enums/card/cardStatus.enum';
import { CardStatusHistoryInterface, IHistories } from '../../../interfaces/card/status';

export class CardStatusHistoryDto implements CardStatusHistoryInterface {
  currentStatus?: CardStatusEnum;
  dateChangeStatus?: string = '';
  histories?: Array<IHistories> = [];
}
