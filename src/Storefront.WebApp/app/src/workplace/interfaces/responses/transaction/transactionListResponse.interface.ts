import { TransactionModel } from '../../../models/transaction';

export interface TransactionListResponseInterface {
  rows: Array<TransactionModel>;
  isNextPageAvailable: boolean;
}
