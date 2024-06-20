import { TravelStatusEnum } from '../../enums/travel/travelStatus.enum';
import { TravelInterface } from '../../interfaces/travel';
import { CountryInterface } from '../../interfaces/country';
import { CardInterface } from '../../interfaces/card';

export class TravelDto implements TravelInterface {
  travelId!: number;
  solarId?: number;
  status?: TravelStatusEnum;
  countries?: CountryInterface[];
  startTravel?: string;
  endTravel?: string;
  contactPhone?: string;
  cards?: CardInterface[];
  cashWithdrawalLimit?: number;
  limitCardTransfers?: number;
  rowNum?: number;
}
