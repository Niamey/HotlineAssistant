import TariffModel from '../../../../models/tariff/tariff.model';

export interface TariffListResponseInterface {
  rows: Array<TariffModel>;
}
