import { TariffSendingResponseInterface } from '../../../../interfaces/responses/tariff/detail';

export class TariffSendingResponseModel implements TariffSendingResponseInterface {
  constructor (data :TariffSendingResponseInterface) {
    this.success = data.success;
    this.message = data.message;
  }

  public success: boolean;
  public message?: string;
}
