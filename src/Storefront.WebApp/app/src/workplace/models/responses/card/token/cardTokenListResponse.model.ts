import { CardTokenDto } from '../../../../dto/card/token';
import { CardTokenInterface } from '../../../../interfaces/card/token';
import { CardTokenListResponseInterface } from '../../../../interfaces/responses/card/token';
import { CardTokenModel } from '../../../card/token';

export class CardTokenListResponseModel implements CardTokenListResponseInterface {
  public rows: Array<CardTokenModel>;

  constructor (items: Array<CardTokenInterface>) {
    this.rows = items.map((dto: CardTokenDto) => new CardTokenModel(dto));
  }
}
