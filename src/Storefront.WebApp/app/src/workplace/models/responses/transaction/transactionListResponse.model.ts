import { TransactionModel } from '../../transaction';
import { TransactionListResponseInterface } from '../../../interfaces/responses/transaction';
import { TransactionInterface } from '../../../interfaces/transaction';
import { TransactionDto } from '../../../dto/transaction';

export class TransactionListResponseModel implements TransactionListResponseInterface {
  public rows: Array<TransactionModel>;
  public isNextPageAvailable: boolean;

  constructor (items: Array<TransactionInterface>, isNextPageAvailable: boolean) {
    this.rows = items.map((transactionDto: TransactionDto) => new TransactionModel(transactionDto));
    this.isNextPageAvailable = isNextPageAvailable;
  }
}
