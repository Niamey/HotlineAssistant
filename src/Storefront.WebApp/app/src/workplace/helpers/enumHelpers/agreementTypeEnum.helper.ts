import { AgreementTypeEnum } from '../../enums/agreement/agreementType.enum';
import { i18n } from '../../../boot/i18n';

export class AgreementTypeEnumHelper {
  public static getTypeName (type: AgreementTypeEnum) : string {
    switch (type) {
      case AgreementTypeEnum.CardAccount : return i18n.t('enums.agreementTypeEnum.cardAccount').toString();
      case AgreementTypeEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
