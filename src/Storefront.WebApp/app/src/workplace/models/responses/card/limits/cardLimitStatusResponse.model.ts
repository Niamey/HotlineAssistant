import { CardLimitStatusModel } from '../../../card/limits';
import { CardLimitStatusInterface } from '../../../../interfaces/card/limits';
import { CardLimitStatusResponseInterface } from '../../../../interfaces/responses/card/limits';

export class CardLimitStatusResponseModel implements CardLimitStatusResponseInterface {
  constructor (data: CardLimitStatusInterface) {
    this.item = new CardLimitStatusModel(data);
  }

  public item: CardLimitStatusModel;
}
