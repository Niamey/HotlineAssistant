import { DirectionEnum } from '../../enums/transaction/direction.enum';
import { i18n } from '../../../boot/i18n';

export class TransactionDirectionEnumHelper {
  public static getDirectionName (direction: DirectionEnum) : string {
    switch (direction) {
      case DirectionEnum.Original : return i18n.t('enums.transaction.directionEnum.original').toString();
      case DirectionEnum.Adjustment: return i18n.t('enums.transaction.directionEnum.adjustment').toString();
      case DirectionEnum.Reverse: return i18n.t('enums.transaction.directionEnum.reverse').toString();
      case DirectionEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
