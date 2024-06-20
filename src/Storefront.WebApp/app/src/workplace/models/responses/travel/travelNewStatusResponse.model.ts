import { TravelNewStatusResponseInterface } from '../../../interfaces/responses/travel';

export class TravelNewStatusResponseModel {
  constructor (data: TravelNewStatusResponseInterface) {
    this.message = data.message;
    this.success = data.success;
  }
  public success?: boolean;
  public message?: string;
}