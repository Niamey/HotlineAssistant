import { Card3DSecureStatusEnum } from '../../../enums/card/services/card3DSecureStatus.enum';

export interface Card3DSecureInterface {
  status?: Card3DSecureStatusEnum;
  phone?: string;
  tariff?: string;
  isFinancePhone?: boolean;
}
