import { AccountStatusEnum } from '../../enums/account/accountStatus.enum';
import { i18n } from '../../../boot/i18n';

export class AccountStatusEnumHelper {
  public static getStatusName (status: AccountStatusEnum) : string {
    switch (status) {
      case AccountStatusEnum.Active : return i18n.t('enums.accountStatusEnum.active').toString();
      case AccountStatusEnum.Inactive: return i18n.t('enums.accountStatusEnum.inactive').toString();
      case AccountStatusEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
