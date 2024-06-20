import { DirectionClassEnum } from '../../enums/transaction/directionClass.enum';
import { i18n } from '../../../boot/i18n';

export class TransactionDirectionClassEnumHelper {
  public static getDirectionClassName (directionClass: DirectionClassEnum) : string {
    switch (directionClass) {
      case DirectionClassEnum.Debit : return i18n.t('enums.transaction.directionClassEnum.debit').toString();
      case DirectionClassEnum.Credit: return i18n.t('enums.transaction.directionClassEnum.credit').toString();
      case DirectionClassEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
