import { Card3DSecureInterface } from '../../../interfaces/card/services';
import { Card3DSecureStatusEnum } from '../../../enums/card/services/card3DSecureStatus.enum';

export class Card3DSecureDto implements Card3DSecureInterface {
  status?: Card3DSecureStatusEnum;
  phone?: string = '';
  tariff?: string = '';
  isFinancePhone?: boolean = false;
}
