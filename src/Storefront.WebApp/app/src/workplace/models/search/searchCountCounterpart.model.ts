import { SearchCountCounterpartRequestInterface } from '../../interfaces/requests/client/search';

export default class SearchCountCounterpart implements SearchCountCounterpartRequestInterface {
  public searchType?: string | (string | null)[];
  public searchFilter?: string | (string | null)[];
}
