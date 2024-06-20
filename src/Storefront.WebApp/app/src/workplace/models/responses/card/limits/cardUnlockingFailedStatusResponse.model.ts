import { CardUnlockingFailedStatusResponseInterface } from '../../../../interfaces/responses/card/limits/';

export class CardUnlockingFailedStatusResponseModel {
  constructor (data: CardUnlockingFailedStatusResponseInterface) {
    this.message = data.message;
    this.success = data.success;
  }
  public success?: boolean;
  public message?: string;
}
