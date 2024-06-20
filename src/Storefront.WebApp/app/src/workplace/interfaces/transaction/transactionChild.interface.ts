import { CategoryEnum, ClassEnum, DirectionClassEnum, DirectionEnum, TxnStatusEnum } from '../../enums/transaction';
import { TransactionFeeInterface } from './';

export interface TransactionChildInterface {
  txnId: number,
  rowId?: string,
  class?: ClassEnum,
  category?: CategoryEnum,
  direction?: DirectionEnum,
  directionClass?: DirectionClassEnum,
  transactionDetails?: string,
  transactionTypeCode?: string,
  transactionTypeName?: string,
  transactionDate?: string,
  transactionAmount?: number,
  transactionCurrency?: string,
  txnStatus?: TxnStatusEnum,
  responseCode?: string,
  feeAmount?: number,
  feeCurrency?: string,
  fees?: Array<TransactionFeeInterface>,
}
