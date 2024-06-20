import { TariffDto } from '../../dto/tariff';

export default class TariffModel extends TariffDto {
  constructor (dto: TariffDto) {
    super();
    Object.assign(this, dto);
  }
}
