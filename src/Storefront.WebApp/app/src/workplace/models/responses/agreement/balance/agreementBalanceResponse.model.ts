import AgreementBalanceModel from '../../../agreement/balance/agreementBalance.model';
import { AgreementBalanceResponseInterface } from '../../../../interfaces/responses/agreement/balance/agreementBalanceResponse.interface';
import { AgreementBalanceInterface } from '../../../../interfaces/agreement/balance/agreementBalance.interface';

export class AgreementBalanceResponseModel implements AgreementBalanceResponseInterface {
  constructor (data: AgreementBalanceInterface) {
    this.item = new AgreementBalanceModel(data);
  }

  public item: AgreementBalanceModel;
}
