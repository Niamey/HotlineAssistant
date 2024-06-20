import { CardTokenStatusEnum } from '../../enums/card/token';
import { i18n } from '../../../boot/i18n';

export class CardTokenStatusEnumHelper {
  public static getStatusName (status: CardTokenStatusEnum) : string {
    switch (status) {
      case CardTokenStatusEnum.Active : return i18n.t('enums.cardToken.statusEnum.active').toString();
      case CardTokenStatusEnum.Deleted: return i18n.t('enums.cardToken.statusEnum.deleted').toString();
      case CardTokenStatusEnum.Inactive: return i18n.t('enums.cardToken.statusEnum.inactive').toString();
      case CardTokenStatusEnum.Suspended: return i18n.t('enums.cardToken.statusEnum.suspended').toString();
      case CardTokenStatusEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
