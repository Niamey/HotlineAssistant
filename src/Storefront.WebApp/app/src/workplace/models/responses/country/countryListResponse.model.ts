import { CountryModel } from '../../country/country.model';
import { CountryListResponseInterface } from '../../../interfaces/responses/country';
import { CountryInterface } from '../../../interfaces/country/country.interface';
import { CountryDto } from '../../../dto/country/country.dto';

export class CountryListResponseModel implements CountryListResponseInterface {
  public rows: Array<CountryModel>;

  constructor (items: Array<CountryInterface>) {
    this.rows = items.map((countryDto: CountryDto) => new CountryModel(countryDto));
  }
}
