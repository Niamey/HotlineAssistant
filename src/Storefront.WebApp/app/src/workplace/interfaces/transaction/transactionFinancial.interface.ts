import { /* EntryTypeEnum, */ TxnStatusEnum } from '../../enums/transaction';

export interface TransactionFinancialInterface {
  txnId: number,
  rowId?: string,
  cardId?: number,
  cardNum?: string,
  agreementId?: number,
  agreementNum?: string,
  // entryType?: EntryTypeEnum,
  transactionTypeName?: string,
  transactionDate?: string,
  transactionAmount?: number,
  transactionCurrency?: string,
  billingAmount?: number,
  billingCurrency?: string,
  merchantName?: string,
  merchantCity?: string,
  merchantState?: string,
  merchantCountry?: string,
  txnStatus?: TxnStatusEnum
}
