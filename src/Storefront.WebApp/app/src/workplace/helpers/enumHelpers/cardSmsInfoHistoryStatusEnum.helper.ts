import { CardSmsInfoHistoryStatusEnum } from '../../enums/card/cardSmsInfoHistoryStatus.enum';
import { i18n } from '../../../boot/i18n';

export class CardSmsInfoHistoryStatusEnumHelper {
  public static getHistoryStatusName (status: CardSmsInfoHistoryStatusEnum) : string {
    switch (status) {
      case CardSmsInfoHistoryStatusEnum.Inactive : return i18n.t('enums.smsInfoHistoryStatusEnum.inactive').toString();
      case CardSmsInfoHistoryStatusEnum.Active: return i18n.t('enums.smsInfoHistoryStatusEnum.active').toString();
      case CardSmsInfoHistoryStatusEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
