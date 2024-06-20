import { CreditPeriodTypeEnum } from '../../../enums/card/debtToBank/creditPeriodType.enum';

export interface CardDebtToBankInterface {
  period?: CreditPeriodTypeEnum;
  mandatoryPayment?: number;
  mandatoryDate?: Date;
  preferentialPayment?: number;
  debt?: number;
  overdueLoan?: number;
  interests?: number;
  overdraft?: number;
  penalty?: number;
  fee?: number;
  currencyName?: string;
  isVacationPeriod?: boolean;
  vacationPeriodEnd?: Date;
	debtPercent?: number;
}
