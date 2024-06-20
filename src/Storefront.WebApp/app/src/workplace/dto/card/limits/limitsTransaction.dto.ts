import { LimitsTransactionInterface } from '../../../interfaces/card/limits';

export class LimitsTransactionDto implements LimitsTransactionInterface {
  operationId?: number;
  operationDate?: string = '';
  marchantName?: string = '';
  operationDetails?: string = '';
  amount?: number;
  currency?: string = '';
  status?: string = '';
}
