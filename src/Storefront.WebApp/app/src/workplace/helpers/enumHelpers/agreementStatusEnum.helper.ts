import { AgreementStatusEnum } from '../../enums/agreement/agreementStatus.enum';
import { i18n } from '../../../boot/i18n';

export class AgreementStatusEnumHelper {
  public static getStatusName (status: AgreementStatusEnum) : string {
    switch (status) {
      case AgreementStatusEnum.Active : return i18n.t('enums.agreementStatusEnum.active').toString();
      case AgreementStatusEnum.Suspended: return i18n.t('enums.agreementStatusEnum.suspended').toString();
      case AgreementStatusEnum.Frozen: return i18n.t('enums.agreementStatusEnum.frozen').toString();
      case AgreementStatusEnum.Closed: return i18n.t('enums.agreementStatusEnum.closed').toString();
      case AgreementStatusEnum.Reserve: return i18n.t('enums.agreementStatusEnum.reserve').toString();
      case AgreementStatusEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
