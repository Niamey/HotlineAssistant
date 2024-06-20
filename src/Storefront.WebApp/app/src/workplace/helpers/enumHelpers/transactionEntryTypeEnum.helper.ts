import { EntryTypeEnum } from '../../enums/transaction/entryType.enum';
import { i18n } from '../../../boot/i18n';

export class TransactionEntryTypeEnumHelper {
  public static getEntryTypeName (entryType: EntryTypeEnum) : string {
    switch (entryType) {
      case EntryTypeEnum.Transaction : return i18n.t('enums.transaction.entryTypeEnum.transaction').toString();
      case EntryTypeEnum.Fee: return i18n.t('enums.transaction.entryTypeEnum.fee').toString();
      case EntryTypeEnum.CreditLimit: return i18n.t('enums.transaction.entryTypeEnum.credit_limit').toString();
      case EntryTypeEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
