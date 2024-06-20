import AgreementStatusHistoryModel from '../../../agreement/status/agreementStatusHistory.model';
import { AgreementStatusHistoryResponseInterface } from '../../../../interfaces/responses/agreement/status';
import { AgreementStatusHistoryInterface } from '../../../../interfaces/agreement/status';

export class AgreementStatusHistoryResponseModel implements AgreementStatusHistoryResponseInterface {
  constructor (data: AgreementStatusHistoryInterface) {
    this.item = new AgreementStatusHistoryModel(data);
  }

  public item: AgreementStatusHistoryModel;
}
