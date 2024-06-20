import { CardSmsInfoInterface } from '../../../interfaces/card/services';
import { CardSmsInfoStatusEnum } from '../../../enums/card/services/cardSmsInfoStatus.enum';

export class CardSmsInfoDto implements CardSmsInfoInterface {
  status?: CardSmsInfoStatusEnum;
  phone?: string = '';
  tariff?: string = '';
  isFinancePhone?: boolean = false;
}
