import { LimitsVictimInterface } from '../../../interfaces/card/limits';

export class LimitsVictimDto implements LimitsVictimInterface {
  fullName?: string = '';
  cardNumber?: string = '';
  phone?: string = '';
  comments?: string = '';
}
