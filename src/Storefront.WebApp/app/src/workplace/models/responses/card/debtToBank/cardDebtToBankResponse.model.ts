import { CardDebtToBankModel } from '../../../card/debtToBank/cardDebtToBank.model';
import { CardDebtToBankResponseInterface } from '../../../../interfaces/responses/card/debtToBank/cardDebtToBankResponse.interface';
import { CardDebtToBankInterface } from '../../../../interfaces/card/debtToBank/cardDebtToBank.interface';

export class CardDebtToBankResponseModel implements CardDebtToBankResponseInterface {
  constructor (data: CardDebtToBankInterface) {
    this.item = new CardDebtToBankModel(data);
  }

  public item: CardDebtToBankModel;
}