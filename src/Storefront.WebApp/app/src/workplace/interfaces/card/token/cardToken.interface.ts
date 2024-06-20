import { CardTokenStatusEnum } from '../../../enums/card/token';

export interface CardTokenInterface {
  tokenId?: string;
  tokenStatus?: CardTokenStatusEnum;
  tokenTime?: string;
  deviceName?: string;
  wallet?: string;
}
