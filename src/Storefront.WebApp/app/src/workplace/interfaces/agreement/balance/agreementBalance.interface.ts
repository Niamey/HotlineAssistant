export interface AgreementBalanceInterface {
  date?: string;
  currency?: string;
  available?: number;
  hasCreditLimit?: boolean;
  creditLimit?: number;
  usedCreditLimit?: number
}