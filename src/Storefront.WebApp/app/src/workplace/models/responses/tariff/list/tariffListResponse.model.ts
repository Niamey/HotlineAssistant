import TariffModel from '../../../tariff/tariff.model';
import { TariffListResponseInterface } from '../../../../interfaces/responses/tariff/list';
import { TariffInterface } from '../../../../interfaces/tariff';
import { TariffDto } from '../../../../dto/tariff';

export class TariffListResponseModel implements TariffListResponseInterface {
  constructor (items: Array<TariffInterface>) {
    this.rows = items.map((tariffDto: TariffDto) => new TariffModel(tariffDto));
  }

  public rows: Array<TariffModel>;
}
