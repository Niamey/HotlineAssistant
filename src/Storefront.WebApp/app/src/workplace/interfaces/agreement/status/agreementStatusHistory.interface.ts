import { AgreementStatusEnum } from '../../../enums/agreement/agreementStatus.enum';

export interface IHistories {
  isFutureStatus?: boolean;
  status?: AgreementStatusEnum;
  userLogin?: string;
  systemName?: string;
  comment?: string;
  modifyDate?: string;
  isEmployee?: boolean;
}

export interface AgreementStatusHistoryInterface {
  currentStatus?: AgreementStatusEnum;
  dateChangeStatus?: string;
  histories?: Array<IHistories>;
}
