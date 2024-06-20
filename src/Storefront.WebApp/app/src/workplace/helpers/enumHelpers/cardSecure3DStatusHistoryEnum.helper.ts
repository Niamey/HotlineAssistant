import { CardSecure3DStatusHistoryEnum } from '../../enums/card/cardSecure3DStatusHistory.enum';
import { i18n } from '../../../boot/i18n';

export class CardSecure3DStatusHistoryEnumHelper {
  public static getStatusHistoryName (status: CardSecure3DStatusHistoryEnum) : string {
    switch (status) {
      case CardSecure3DStatusHistoryEnum.Inactive : return i18n.t('enums.cardSecure3dStatusHistoryEnum.inactive').toString();
      case CardSecure3DStatusHistoryEnum.Active: return i18n.t('enums.cardSecure3dStatusHistoryEnum.active').toString();
      case CardSecure3DStatusHistoryEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
