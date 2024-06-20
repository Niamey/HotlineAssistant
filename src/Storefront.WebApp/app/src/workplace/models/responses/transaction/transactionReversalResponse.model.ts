import { TransactionModel } from '../../transaction';
import { TransactionReversalResponseInterface } from '../../../interfaces/responses/transaction';
import { TransactionInterface } from '../../../interfaces/transaction';

export class TransactionReversalResponseModel implements TransactionReversalResponseInterface {
  constructor (data: TransactionInterface) {
    this.item = new TransactionModel(data);
  }

  public item: TransactionModel;
}
