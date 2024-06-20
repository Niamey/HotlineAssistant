import { LimitsPersonWhoCallsInterface } from '../../../interfaces/card/limits';

export class LimitsPersonWhoCallsDto implements LimitsPersonWhoCallsInterface {
  fullName?: string = '';
  phone?: string = '';
  comments?: string = '';
  isRefusedToProvideInfo?: boolean = false;
}
