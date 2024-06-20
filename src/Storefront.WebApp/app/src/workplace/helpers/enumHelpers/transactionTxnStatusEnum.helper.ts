import { TxnStatusEnum } from '../../enums/transaction/txnStatus.enum';
import { i18n } from '../../../boot/i18n';

export class TransactionTxnStatusHelper {
  public static getStatusName (status: TxnStatusEnum) : string {
    switch (status) {
      case TxnStatusEnum.New : return i18n.t('enums.transaction.txnStatusEnum.new').toString();
      case TxnStatusEnum.Loaded : return i18n.t('enums.transaction.txnStatusEnum.loaded').toString();
      case TxnStatusEnum.LoadError : return i18n.t('enums.transaction.txnStatusEnum.loadError').toString();
      case TxnStatusEnum.Prepared : return i18n.t('enums.transaction.txnStatusEnum.prepared').toString();
      case TxnStatusEnum.Finished : return i18n.t('enums.transaction.txnStatusEnum.finished').toString();
      case TxnStatusEnum.Rejected : return i18n.t('enums.transaction.txnStatusEnum.rejected').toString();
      case TxnStatusEnum.Closed: return i18n.t('enums.transaction.txnStatusEnum.closed').toString();
      case TxnStatusEnum.PreProcessing: return i18n.t('enums.transaction.txnStatusEnum.preProcessing').toString();
      case TxnStatusEnum.Shaded: return i18n.t('enums.transaction.txnStatusEnum.shaded').toString();
      case TxnStatusEnum.PreAppliedAsFin: return i18n.t('enums.transaction.txnStatusEnum.preAppliedAsFin').toString();
      case TxnStatusEnum.Undefined: return i18n.t('enums.undefined').toString();
      default: return '';
    }
  }
}
