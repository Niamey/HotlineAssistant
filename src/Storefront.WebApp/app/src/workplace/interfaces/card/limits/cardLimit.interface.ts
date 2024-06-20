import { AmountCardLimitInterface } from '../../../interfaces/card/limits/amountCardLimit.interface';

export interface CardLimitInterface {
  enabled?: boolean;
	limits?: Array<AmountCardLimitInterface>;
}
