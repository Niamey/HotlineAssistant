import { CardStatusEnum } from '../../../enums/card/cardStatus.enum';
import { CardCurrentStatusInterface } from '../../../interfaces/card/status';

export class CardCurrentStatusDto implements CardCurrentStatusInterface {
  status?: CardStatusEnum;
  dateChangeStatus?: string = '';
  reason?: string = '';
  systemName?: string = '';
  userLogin?: string = '';
  isEmployee?: boolean;
}
