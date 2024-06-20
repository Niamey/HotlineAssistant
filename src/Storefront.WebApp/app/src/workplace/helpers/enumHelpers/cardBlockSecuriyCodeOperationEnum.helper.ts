import { SecurityCodeOperationTypeEnum } from '../../enums/card/limits/securityCodeOperationType.enum';
import { i18n } from '../../../boot/i18n';

export class CardBlockSecurityCodeOperationEnumHelper {
  public static getTypeName (status: SecurityCodeOperationTypeEnum) : string {
    switch (status) {
      case SecurityCodeOperationTypeEnum.Other : return i18n.t('enums.cardBlockSecurityCodeOperationTypeEnum.other').toString();
      case SecurityCodeOperationTypeEnum.MobileAppPassword: return i18n.t('enums.cardBlockSecurityCodeOperationTypeEnum.mobileAppPassword').toString();
      case SecurityCodeOperationTypeEnum.CardVerification: return i18n.t('enums.cardBlockSecurityCodeOperationTypeEnum.cardVerification').toString();
      default: return '';
    }
  }
}
