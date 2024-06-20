import TariffModel from '../../../tariff/tariff.model';
import { TariffDetailResponseInterface } from '../../../../interfaces/responses/tariff/detail';
import { TariffInterface } from '../../../../interfaces/tariff';

export class TariffDetailResponseModel implements TariffDetailResponseInterface {
  constructor (data: TariffInterface) {
    this.item = new TariffModel(data);
  }

  public item: TariffModel;
}
