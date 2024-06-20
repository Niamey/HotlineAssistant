import { CardLimitOfCashWithdrawalInterface } from '../../../interfaces/card/limits/cardLimitOfCashWithdrawal.interface';
import { CardLimitInterface } from '../../../interfaces/card/limits/cardLimit.interface';

export class CardLimitOfCashWithdrawalDto implements CardLimitOfCashWithdrawalInterface {
  limitOfCashWithdrawal?: boolean;
  internalTransactions?: CardLimitInterface;
  transferTransactions?: CardLimitInterface;
  systemLimit?: CardLimitInterface;
  products?: CardLimitInterface;
}
