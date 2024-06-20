import { CardTokenLastStatusInterface } from '../../../interfaces/card/token';
import { CardTokenInitiatorEnum, CardTokenReasonCodeEnum, CardTokenStatusCodeEnum } from '../../../enums/card/token';

export class CardTokenLastStatusDto implements CardTokenLastStatusInterface {
  public commentText?: string = '';
  public initiator?: CardTokenInitiatorEnum;
  public reasonCode?: CardTokenReasonCodeEnum;
  public statusCode?: CardTokenStatusCodeEnum;
  public statusDateTime?: string = '';
  public statusDescription?: string = '';
}
