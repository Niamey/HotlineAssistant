import { MoneyBoxStatusEnum } from '../../enums/agreement/moneyBoxStatus.enum';
import { i18n } from '../../../boot/i18n';

export class MoneyBoxStatusEnumHelper {
  public static getStatusName (status: MoneyBoxStatusEnum) : string {
    switch (status) {
      case MoneyBoxStatusEnum.Active : return i18n.t('enums.moneyBoxStatusEnum.active').toString();
      case MoneyBoxStatusEnum.Inactive: return i18n.t('enums.moneyBoxStatusEnum.inactive').toString();
      case MoneyBoxStatusEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}