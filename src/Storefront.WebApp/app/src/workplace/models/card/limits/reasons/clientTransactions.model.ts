import { LimitsTransactionModel } from './limitsTransaction.model';
import { LimitsTransactionDto } from '../../../../dto/card/limits';
import { LimitsDateRangeModel } from './limitsDateRange.model';

export class ClientTransactionsModel {
  dateRange?: LimitsDateRangeModel | string | undefined;
  allTransaction?: boolean = false;
  transactions?: Array<LimitsTransactionModel>

  constructor (dateRange: LimitsDateRangeModel | string | undefined, allTransaction: boolean, transactions: Array<LimitsTransactionModel>) {
    this.dateRange = dateRange;
    this.allTransaction = allTransaction;
    this.transactions = transactions.map((dto: LimitsTransactionDto) => new LimitsTransactionModel(dto));
  }
}
