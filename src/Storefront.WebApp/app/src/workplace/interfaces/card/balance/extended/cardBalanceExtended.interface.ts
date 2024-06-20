export interface CardBalanceExtendedInterface {
  date?: string;
  currency?: string;
  available?: number;
  hasCreditLimi?: boolean;
  creditLimit?: number;
  usedCreditLimit?: number;
  minimumBalance?: number;
}
