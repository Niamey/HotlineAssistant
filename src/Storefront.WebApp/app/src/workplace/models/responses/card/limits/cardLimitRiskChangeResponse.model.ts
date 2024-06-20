import { CardLimitRiskChangeResponseInterface } from '../../../../interfaces/responses/card/limits';

export class CardLimitRiskChangeResponseModel implements CardLimitRiskChangeResponseInterface {
  constructor (data: CardLimitRiskChangeResponseInterface) {
    this.success = data.success;
    this.message = data.message;
  }

  public success?: boolean;
  public message?: string;
}