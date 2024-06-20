import { CardTokenReasonCodeEnum } from '../../enums/card/token';
import { i18n } from '../../../boot/i18n';

export class CardTokenReasonCodeEnumHelper {
  public static getReasonCodeName (reason: CardTokenReasonCodeEnum) : string {
    switch (reason) {
      case CardTokenReasonCodeEnum.Auth : return i18n.t('enums.cardToken.reasonCodeEnum.auth').toString();
      case CardTokenReasonCodeEnum.Lost: return i18n.t('enums.cardToken.reasonCodeEnum.lost').toString();
      case CardTokenReasonCodeEnum.Stolen: return i18n.t('enums.cardToken.reasonCodeEnum.stolen').toString();
      case CardTokenReasonCodeEnum.Closed: return i18n.t('enums.cardToken.reasonCodeEnum.closed').toString();
      case CardTokenReasonCodeEnum.Found: return i18n.t('enums.cardToken.reasonCodeEnum.found').toString();
      case CardTokenReasonCodeEnum.Transaction: return i18n.t('enums.cardToken.reasonCodeEnum.transaction').toString();
      case CardTokenReasonCodeEnum.Other: return i18n.t('enums.cardToken.reasonCodeEnum.other').toString();
      case CardTokenReasonCodeEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }

  public static getDeleteReasonCodeName (reason: CardTokenReasonCodeEnum) : string {
    switch (reason) {
      case CardTokenReasonCodeEnum.Lost: return i18n.t('enums.cardToken.deleteReasonCodeEnum.lost').toString();
      case CardTokenReasonCodeEnum.Stolen: return i18n.t('enums.cardToken.deleteReasonCodeEnum.stolen').toString();
      case CardTokenReasonCodeEnum.Closed: return i18n.t('enums.cardToken.deleteReasonCodeEnum.closed').toString();
      case CardTokenReasonCodeEnum.Transaction: return i18n.t('enums.cardToken.deleteReasonCodeEnum.transaction').toString();
      case CardTokenReasonCodeEnum.Other: return i18n.t('enums.cardToken.deleteReasonCodeEnum.other').toString();
      case CardTokenReasonCodeEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
