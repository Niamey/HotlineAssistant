import { CountryInterface } from '../../interfaces/country/country.interface';

export class CountryDto implements CountryInterface {
  countryCode?: string;
  countryName?: string;
  countryA2?: string;
  isRisky?: boolean;
}