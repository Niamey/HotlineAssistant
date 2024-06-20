import { CardBalanceExtendedInterface } from '../../../../interfaces/card/balance/extended';

export class CardBalanceExtendedDto implements CardBalanceExtendedInterface {
  date?: string = '';
  currency?: string = '';
  available?: number;
  hasCreditLimi?: boolean;
  creditLimit?: number;
  usedCreditLimit?: number;
  minimumBalance?: number;
}
