import { CardLimitInterface } from '../../../interfaces/card/limits/cardLimit.interface';

export interface CardLimitOfCashWithdrawalInterface {
  limitOfCashWithdrawal?: boolean;
  internalTransactions?: CardLimitInterface;
  transferTransactions?: CardLimitInterface;
  systemLimit?: CardLimitInterface;
  products?: CardLimitInterface;
}
