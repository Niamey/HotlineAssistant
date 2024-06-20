import { CardSmsInfoStatusEnum } from '../../../enums/card/services/cardSmsInfoStatus.enum';

export interface CardSmsInfoInterface {
  status?: CardSmsInfoStatusEnum;
  phone?: string;
  tariff?: string;
  isFinancePhone?: boolean;
}
