import { SenderNameTypeEnum } from '../../enums/card/limits/senderNameType.enum';
import { i18n } from '../../../boot/i18n';

export class CardBlockSenderNameEnumHelper {
  public static getName (status: SenderNameTypeEnum) : string {
    switch (status) {
      case SenderNameTypeEnum.Other : return i18n.t('enums.cardBlockSenderNameTypeEnum.other').toString();
      case SenderNameTypeEnum.BankVostok: return 'BANKVOSTOK';
      default: return '';
    }
  }
}
