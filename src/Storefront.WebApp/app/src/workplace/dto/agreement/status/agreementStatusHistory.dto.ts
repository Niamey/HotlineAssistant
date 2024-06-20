import { AgreementStatusEnum } from '../../../enums/agreement/agreementStatus.enum';
import { AgreementStatusHistoryInterface, IHistories } from '../../../interfaces/agreement/status';

export class AgreementStatusHistoryDto implements AgreementStatusHistoryInterface {
  currentStatus?: AgreementStatusEnum;
  dateChangeStatus?: string = '';
  histories?: Array<IHistories> = [];
}
