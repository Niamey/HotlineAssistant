import { CardLimitStatusInterface } from '../../../interfaces/card/limits/cardLimitStatus.interface';

export class CardLimitStatusDto implements CardLimitStatusInterface {
  success?: boolean
	message?: string
}
