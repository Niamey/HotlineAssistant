import { LimitsPersonalDataModel } from './limitsPersonalData.model';
import { LimitsCheaterModel } from './limitsCheater.model';
import { CardTokenModel } from '../../token/cardToken.model';

export class ReceivedSMSCodeReasonModel {
  properties?: Array<LimitsPersonalDataModel>;
  isClientToldSMCodeFromSMS?: boolean;
  isNeedDeleteToken?: boolean;
  isCancelAccessCode?: boolean;
  cheater?: LimitsCheaterModel;
  deletedTokens?: Array<CardTokenModel>;
}