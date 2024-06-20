import { CardTokenInitiatorEnum } from '../../enums/card/token';
import { i18n } from '../../../boot/i18n';

export class CardTokenInitiatorEnumHelper {
  public static getInitiatorName (initiator: CardTokenInitiatorEnum) : string {
    switch (initiator) {
      case CardTokenInitiatorEnum.Issuer : return i18n.t('enums.cardToken.initiatorEnum.issuer').toString();
      case CardTokenInitiatorEnum.TokenRequestorWalletProvider: return i18n.t('enums.cardToken.initiatorEnum.tokenRequestorWalletProvider').toString();
      case CardTokenInitiatorEnum.Cardholder: return i18n.t('enums.cardToken.initiatorEnum.cardholder').toString();
      case CardTokenInitiatorEnum.MobPINValidService: return i18n.t('enums.cardToken.initiatorEnum.mobPINValidService').toString();
      case CardTokenInitiatorEnum.MobPINChangeValidService: return i18n.t('enums.cardToken.initiatorEnum.mobPINChangeValidService').toString();
      case CardTokenInitiatorEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
