import { CardStatusEnum, CardCategoryEnum, CardViewTypeEnum, CardTypeEnum, CardPushInfoStatusEnum } from '../../enums/card';

export interface ICardTariff {
  name?: string;
  date?: string;
}

export interface ICardBranch {
  branchId?: number
  name?: string;
  address?: string;
  phone?: string;
}

export interface CardInterface {
  cardId: number;
  solarId?: number;
  agreementId?: number;
  agreementNumber?: string;
  cardCode?: string;
  number?: string;
  smsInfoPhone?: string;
  pushInfo?: CardPushInfoStatusEnum;
  securePhone?: string;
  inStopList?: boolean;
  expired?: string;
  tariff?: ICardTariff;
  embossedName?: string;
  status?: CardStatusEnum;
  type?: CardTypeEnum;
  kinde?: number;
  category?: CardCategoryEnum;
  hasChip?: boolean;
  isVirtual?: boolean;
  barcode?: string;
  branch?: ICardBranch;
  viewType?: CardViewTypeEnum;
}
