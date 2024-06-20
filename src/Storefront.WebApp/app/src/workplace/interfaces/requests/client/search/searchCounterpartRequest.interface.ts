import { SortDirectionEnum } from '../../../../enums/client/search/sortDirection.enum';
import { SearchCountCounterpartRequestInterface } from './searchCountCounterpartRequest.interface';

export interface SearchCounterpartRequestInterface extends SearchCountCounterpartRequestInterface {
  pageIndex?: number;
  pageSize?: number;
  sortColumn?: string;
  sortDirection?: SortDirectionEnum;
}
