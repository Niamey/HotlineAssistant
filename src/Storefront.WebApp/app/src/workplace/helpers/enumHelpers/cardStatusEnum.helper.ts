import { CardStatusEnum } from '../../enums/card/cardStatus.enum';
import { i18n } from '../../../boot/i18n';

export class CardStatusEnumHelper {
  public static getStatusName (status: CardStatusEnum) : string {
    switch (status) {
      case CardStatusEnum.Active: return i18n.t('enums.cardStatusEnum.active').toString();
      case CardStatusEnum.Blocked: return i18n.t('enums.cardStatusEnum.blocked').toString();
      case CardStatusEnum.Canceled: return i18n.t('enums.cardStatusEnum.canceled').toString();
      case CardStatusEnum.Lost: return i18n.t('enums.cardStatusEnum.lost').toString();
      case CardStatusEnum.Stolen: return i18n.t('enums.cardStatusEnum.stolen').toString();
      case CardStatusEnum.Suspend: return i18n.t('enums.cardStatusEnum.suspend').toString();
      case CardStatusEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
