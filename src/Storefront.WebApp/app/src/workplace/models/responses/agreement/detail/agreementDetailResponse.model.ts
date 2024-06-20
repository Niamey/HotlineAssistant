import AgreementModel from '../../../agreement/agreement.model';
import { AgreementDetailResponseInterface } from '../../../../interfaces/responses/agreement/detail';
import { AgreementInterface } from '../../../../interfaces/agreement';

export class AgreementDetailResponseModel implements AgreementDetailResponseInterface {
  constructor (data: AgreementInterface) {
    this.item = new AgreementModel(data);
  }

  public item: AgreementModel;
}
