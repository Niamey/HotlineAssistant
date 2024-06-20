import { TravelStatusEnum } from '../../enums/travel/travelStatus.enum';
import { CardInterface } from '../card';
import { CountryInterface } from '../country';

export interface TravelInterface {
  travelId: number;
  solarId?: number;
  status?: TravelStatusEnum;
  countries?: Array<CountryInterface>;
  startTravel?: string;
  endTravel?: string;
  contactPhone?: string;
  cards?: Array<CardInterface>;
  cashWithdrawalLimit?: number;
  limitCardTransfers?: number;
  rowNum?: number;
  isClientAbroad?: boolean;
}
