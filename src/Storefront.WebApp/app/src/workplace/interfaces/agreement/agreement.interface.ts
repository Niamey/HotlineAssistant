import { AgreementStatusEnum } from '../../enums/agreement/agreementStatus.enum';
import { AgreementViewTypeEnum } from '../../enums/agreement/agreementViewType.enum';
import { AgreementClassifierInterface } from './agreementClassifier.interface';

export interface AgreementInterface {
  agreementId: number,
  solarId?: number,
  clientName?: string,
  agreementNumber?: string,
  productName?: string,
  currency?: string,
  bonus?: string,
  type?: string,
  debt?: number
  status?: AgreementStatusEnum,
  viewType?: AgreementViewTypeEnum,
  classifiers?: Array<AgreementClassifierInterface>
}
