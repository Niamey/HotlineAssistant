
import { CardInterface } from '../../card';
import { CountryInterface } from '../../country';

export interface TravelNewRequestInterface {
  solarId?: number;
  travelId?: number;
  countries?: Array<CountryInterface>;
  startTravel?: string;
  endTravel?: string;
  contactPhone?: string;
  cards?: Array<CardInterface>;
  cashWithdrawalLimit?: number;
  limitCardTransfers?: number;
  isClientAbroad?: boolean;
}
