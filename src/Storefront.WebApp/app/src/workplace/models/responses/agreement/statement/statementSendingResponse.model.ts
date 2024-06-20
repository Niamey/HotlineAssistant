import { StatementSendingResponseInterface } from '../../../../interfaces/responses/agreement/statement';

export class StatementSendingResponseModel implements StatementSendingResponseInterface {
  constructor (data :StatementSendingResponseInterface) {
    this.success = data.success;
    this.message = data.message;
  }

  public success: boolean;
  public message?: string;
}