import { TransactionFinancialModel } from '../../transaction';
import { TransactionListFinancialResponseInterface } from '../../../interfaces/responses/transaction';
import { TransactionFinancialInterface } from '../../../interfaces/transaction';
import { TransactionFinancialDto } from '../../../dto/transaction';

export class TransactionListFinancialResponseModel implements TransactionListFinancialResponseInterface {
  public rows: Array<TransactionFinancialModel>;
  public isNextPageAvailable: boolean;

  constructor (items: Array<TransactionFinancialInterface>, isNextPageAvailable: boolean) {
    this.rows = items.map((transactionDto: TransactionFinancialDto) => new TransactionFinancialModel(transactionDto));
    this.isNextPageAvailable = isNextPageAvailable;
  }
}
