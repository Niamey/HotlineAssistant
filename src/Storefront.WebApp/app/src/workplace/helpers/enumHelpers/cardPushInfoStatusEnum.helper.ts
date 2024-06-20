import { CardPushInfoStatusEnum } from '../../enums/card/cardPushInfoStatus.enum';
import { i18n } from '../../../boot/i18n';

export class CardPushInfoStatusEnumHelper {
  public static getPushInfoStatusName (status: CardPushInfoStatusEnum) : string {
    switch (status) {
      case CardPushInfoStatusEnum.Active : return i18n.t('enums.turnOn').toString();
      case CardPushInfoStatusEnum.Inactive: return i18n.t('enums.turnOff').toString();
      case CardPushInfoStatusEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
