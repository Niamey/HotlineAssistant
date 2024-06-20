import { CardStatusEnum, CardCategoryEnum, CardViewTypeEnum, CardTypeEnum, CardPushInfoStatusEnum } from '../../enums/card';
import {
  ICardTariff,
  ICardBranch,
  CardInterface
} from '../../interfaces/card';

export class CardDto implements CardInterface {
  cardId!: number;
  solarId?: number;
  agreementId?: number;
  agreementNumber?: string = '';
  cardCode?: string = '';
  number?: string = '';
  smsInfoPhone?: string = '';
  pushInfo?: CardPushInfoStatusEnum;
  securePhone?: string = '';
  inStopList?: boolean = false;
  expired?: string = '';
  tariff?: ICardTariff = {};
  embossedName?: string = '';
  status?: CardStatusEnum;
  type?: CardTypeEnum;
  kinde?: number;
  category?: CardCategoryEnum;
  hasChip?: boolean = false;
  isVirtual?: boolean = false;
  barcode?: string = '';
  branch?: ICardBranch = {};
  viewType?: CardViewTypeEnum;
}
