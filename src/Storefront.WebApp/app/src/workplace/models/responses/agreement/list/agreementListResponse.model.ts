import AgreementModel from '../../../agreement/agreement.model';
import { AgreementListResponseInterface } from '../../../../interfaces/responses/agreement/list';
import { AgreementInterface } from '../../../../interfaces/agreement';
import { AgreementDto } from '../../../../dto/agreement';

export class AgreementListResponseModel implements AgreementListResponseInterface {
  constructor (items: Array<AgreementInterface>) {
    this.rows = items.map((agreementDto: AgreementDto) => new AgreementModel(agreementDto));
  }

  public rows: Array<AgreementModel>;
}
