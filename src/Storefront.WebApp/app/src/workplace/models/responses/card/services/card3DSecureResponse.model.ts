import { Card3DSecureModel } from '../../../card/services';
import { Card3DSecureResponseInterface } from '../../../../interfaces/responses/card/services';
import { Card3DSecureInterface } from '../../../../interfaces/card/services';

export class Card3DSecureResponseModel implements Card3DSecureResponseInterface {
  constructor (data: Card3DSecureInterface) {
    this.item = new Card3DSecureModel(data);
  }

  public item: Card3DSecureModel;
}
