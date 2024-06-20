import { CardStatusEnum } from '../../../enums/card/cardStatus.enum';

export interface CardCurrentStatusInterface {
  status?: CardStatusEnum;
  dateChangeStatus?: string;
  reason?: string;
  systemName?: string;
  userLogin?: string;
  isEmployee?: boolean;
}
