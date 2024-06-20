import AgreementModel from '../../../../models/agreement/agreement.model';

export interface AgreementListResponseInterface {
  rows: Array<AgreementModel>;
}
