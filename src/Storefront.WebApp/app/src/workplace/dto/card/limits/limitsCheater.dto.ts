import { LimitsCheaterInterface } from '../../../interfaces/card/limits';

export class LimitsCheaterDto implements LimitsCheaterInterface {
  cardNumber?: string;
  fullName?: string;
  phone?: string;
  dateCall?: string;
  comments?: string;
}
