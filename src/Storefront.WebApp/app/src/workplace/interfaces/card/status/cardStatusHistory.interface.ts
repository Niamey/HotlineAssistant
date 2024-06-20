import { CardStatusEnum } from '../../../enums/card/cardStatus.enum';
export interface IHistories {
  isFutureStatus: boolean;
  status?: CardStatusEnum;
  userLogin?: string;
  systemName?: string;
  comment?: string;
  modifyDate?: string;
  isEmployee?: boolean;
}

export interface CardStatusHistoryInterface {
  currentStatus?: CardStatusEnum;
  dateChangeStatus?: string;
  histories?: Array<IHistories>;
}
