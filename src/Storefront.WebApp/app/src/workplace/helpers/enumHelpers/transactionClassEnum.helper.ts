import { ClassEnum } from '../../enums/transaction/class.enum';
import { i18n } from '../../../boot/i18n';

export class TransactionClassEnumHelper {
  public static getClassName (className: ClassEnum) : string {
    switch (className) {
      case ClassEnum.Financial : return i18n.t('enums.transaction.classEnum.financial').toString();
      case ClassEnum.Authorization: return i18n.t('enums.transaction.classEnum.authorization').toString();
      case ClassEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
