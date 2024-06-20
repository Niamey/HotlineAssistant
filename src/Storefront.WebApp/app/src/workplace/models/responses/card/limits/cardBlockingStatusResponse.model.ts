
import { CardBlockingStatusResponseInterface } from '../../../../interfaces/responses/card/limits/cardBlockingStatusResponse.interface';

export class CardBlockingStatusResponseModel {
  constructor (data: CardBlockingStatusResponseInterface) {
    this.message = data.message;
    this.success = data.success;
  }
  public success?: boolean;
  public message?: string;
}
