import { TravelDeleteStatusResponseInterface } from '../../../interfaces/responses/travel';

export class TravelDeleteStatusResponseModel {
  constructor (data: TravelDeleteStatusResponseInterface) {
    this.message = data.message;
    this.success = data.success;
  }
  public success?: boolean;
  public message?: string;
}