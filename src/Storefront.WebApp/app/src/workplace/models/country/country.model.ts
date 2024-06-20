import { CountryDto } from '../../dto/country/country.dto';

export class CountryModel extends CountryDto {
  constructor (dto: CountryDto) {
    super();
    Object.assign(this, dto);
  }
}
