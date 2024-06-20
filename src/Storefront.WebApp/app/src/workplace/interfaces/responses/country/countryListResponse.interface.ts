import { CountryModel } from '../../../models/country/country.model';

export interface CountryListResponseInterface {
  rows: Array<CountryModel>;
}
