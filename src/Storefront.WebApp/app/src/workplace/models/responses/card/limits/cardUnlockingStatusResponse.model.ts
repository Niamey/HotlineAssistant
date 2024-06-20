import { CardUnlockingStatusResponseInterface } from '../../../../interfaces/responses/card/limits/';

export class CardUnlockingStatusResponseModel {
  constructor (data: CardUnlockingStatusResponseInterface) {
    this.message = data.message;
    this.success = data.success;
  }
  public success?: boolean;
  public message?: string;
}