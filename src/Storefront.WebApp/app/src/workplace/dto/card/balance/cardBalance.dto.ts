import { CardBalanceInterface } from '../../../interfaces/card/balance';

export class CardBalanceDto implements CardBalanceInterface {
  date?: string = '';
  currencyCode?: string= '';
  currency?: string = '';
  available?: number;
}
