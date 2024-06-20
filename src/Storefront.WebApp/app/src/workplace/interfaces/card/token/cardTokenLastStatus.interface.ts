import { CardTokenInitiatorEnum, CardTokenReasonCodeEnum, CardTokenStatusCodeEnum } from '../../../enums/card/token';

export interface CardTokenLastStatusInterface {
  commentText?: string;
  initiator?: CardTokenInitiatorEnum;
  reasonCode?: CardTokenReasonCodeEnum;
  statusCode?: CardTokenStatusCodeEnum;
  statusDateTime?: string;
  statusDescription?: string;
}
