import { CardUnlockingLockStatusResponseInterface } from '../../../../interfaces/responses/card/limits/';

export class CardUnlockingLockStatusResponseModel {
  constructor (data: CardUnlockingLockStatusResponseInterface) {
    this.message = data.message;
    this.success = data.success;
  }
  public success?: boolean;
  public message?: string;
}