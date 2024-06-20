import { ClientTransactionsModel } from './clientTransactions.model';
import { LimitsPersonWhoCallsModel } from './limitsPersonWhoCalls.model';

export class ClientIsCheaterReasonModel {
  clientTransactions?: ClientTransactionsModel;
  person?: LimitsPersonWhoCallsModel = {};
}
