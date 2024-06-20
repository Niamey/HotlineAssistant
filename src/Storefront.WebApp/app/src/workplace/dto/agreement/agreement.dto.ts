import { AgreementStatusEnum } from '../../enums/agreement/agreementStatus.enum';
import { AgreementViewTypeEnum } from '../../enums/agreement/agreementViewType.enum';
import { AgreementInterface } from '../../interfaces/agreement';
import { AgreementClassifierInterface } from '../../interfaces/agreement/agreementClassifier.interface';

export class AgreementDto implements AgreementInterface {
  agreementId!: number;
  solarId?: number | undefined;
  clientName?: string | undefined;
  agreementNumber?: string | undefined;
  productName?: string | undefined;
  currency?: string | undefined;
  bonus?: string | undefined;
  type?: string | undefined;
  debt?: number | undefined;
  status?: AgreementStatusEnum | undefined;
  viewType?: AgreementViewTypeEnum | undefined;
  classifiers?: Array<AgreementClassifierInterface>
}
