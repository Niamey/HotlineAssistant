import { CardTokenInterface } from '../../../interfaces/card/token';
import { CardTokenStatusEnum } from '../../../enums/card/token';

export class CardTokenDto implements CardTokenInterface {
  public tokenId?: string = '';
  public requestorName?: string = '';
  public tokenStatus?: CardTokenStatusEnum;
  public tokenTime?: string = '';
  public deviceName?: string = ''
  public wallet?: string = '';
}
