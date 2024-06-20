import { SearchCounterpartRequestInterface } from '../../interfaces/requests/client/search';
import { SortDirectionEnum } from '../../enums/client/search/sortDirection.enum';

export default class SearchCounterpart implements SearchCounterpartRequestInterface {
  constructor (pageIndex: number, pageSize: number) {
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;
  }

  public searchFilter?: string | (string | null)[];
  public searchType?: string | (string | null)[];
  public pageIndex?: number;
  public pageSize?: number;
  public sortColumn?: string;
  public sortDirection?: SortDirectionEnum;
}
