import { CardTokenReasonCodeEnum } from '../../../../enums/card/token';

export interface CardTokenDeleteRequestInterface {
  solarId: number;
  cardId: number;
  tokenUniqueReference: string;
  commentText: string;
  reasonCode: CardTokenReasonCodeEnum;
  blockMobApp: boolean;
}
