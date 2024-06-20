import { SortDirectionEnum } from '../../../enums/client/search/sortDirection.enum';
import { SearchCounterpartRequestInterface } from '../../../interfaces/requests/client/search';

export interface TravelListRequestInterface extends SearchCounterpartRequestInterface {
  solarId: number;
}
