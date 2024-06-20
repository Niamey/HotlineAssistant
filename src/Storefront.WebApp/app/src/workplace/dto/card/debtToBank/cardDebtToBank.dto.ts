import { CardDebtToBankInterface } from '../../../interfaces/card/debtToBank/cardDebtToBank.interface';
import { CreditPeriodTypeEnum } from '../../../enums/card/debtToBank/creditPeriodType.enum';

export class CardDebtToBankDto implements CardDebtToBankInterface {
  period?: CreditPeriodTypeEnum = undefined;
  mandatoryPayment?: number = 0;
  mandatoryDate?: Date = undefined;
  preferentialPayment?: number = 0;
  debt?: number = 0;
  overdueLoan?: number = 0;
  interests?: number = 0;
  overdraft?: number = 0;
  penalty?: number = 0;
  fee?: number = 0;
  currencyName?: string = undefined;
  isVacationPeriod?: boolean = undefined;
  vacationPeriodEnd?: Date = undefined;
	debtPercent?: number = 0;
}