import { SortDirectionEnum } from '../../../enums/client/search/sortDirection.enum';
import { TravelListRequestInterface } from '../../../interfaces/requests/travel/travelListRequest.interface';

export default class TravelListRequestModel implements TravelListRequestInterface {
  solarId!: number;
  pageIndex?: number;
  pageSize?: number;
  sortColumn?: string;
  sortDirection?: SortDirectionEnum;
}