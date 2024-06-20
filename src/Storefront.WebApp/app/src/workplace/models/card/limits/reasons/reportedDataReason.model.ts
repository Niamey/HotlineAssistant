import { ClientTransactionsModel } from './clientTransactions.model';
import { LimitsPersonalDataModel } from './limitsPersonalData.model';
import { LimitsPersonWhoCallsModel } from './limitsPersonWhoCalls.model';
import { LimitsCheaterModel } from './limitsCheater.model';
import { CardTokenModel } from '../../token/cardToken.model';

export class ReportedDataReasonModel {
  clientTransactions?: ClientTransactionsModel;
  properties?: Array<LimitsPersonalDataModel>;
  isClientCall?: boolean;
  isNeedDeleteToken?: boolean;
  isCancelAccessCode?: boolean;
  person?: LimitsPersonWhoCallsModel;
  cheater?: LimitsCheaterModel;
  deletedTokens?: Array<CardTokenModel>;
}
