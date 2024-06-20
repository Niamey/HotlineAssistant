import { TransactionTypeEnum } from '../../enums/card/limits/transactionType.enum';
import { i18n } from '../../../boot/i18n';

export class CardBlockTransactionEnumHelper {
  public static getTypeName (status: TransactionTypeEnum) : string {
    switch (status) {
      case TransactionTypeEnum.Other : return i18n.t('enums.cardBlockTransactionTypeEnum.other').toString();
      case TransactionTypeEnum.MobileApp: return i18n.t('enums.cardBlockTransactionTypeEnum.mobileApp').toString();
      default: return '';
    }
  }
}
