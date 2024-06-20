import { AgreementBalanceInterface } from '../../../interfaces/agreement/balance/agreementBalance.interface';

export class AgreementBalanceDto implements AgreementBalanceInterface {
  date?: string = '';
  currency?: string = '';
  available?: number;
  hasCreditLimit?: boolean;
  creditLimit?: number;
  usedCreditLimit?: number
}