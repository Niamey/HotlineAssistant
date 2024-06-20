import { TransactionFinancialModel } from '../../../models/transaction';

export interface TransactionListFinancialResponseInterface {
  rows: Array<TransactionFinancialModel>;
  isNextPageAvailable: boolean;
}
