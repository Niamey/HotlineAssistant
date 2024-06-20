import { TariffInterface } from '../../interfaces/tariff';

export class TariffDto implements TariffInterface {
  tariffId!: number;
  tariffName?: string | undefined;
  tariffStart?: string | undefined;
  tariffEnd?: string | undefined;
  tariffUrl?: string | undefined;
}
