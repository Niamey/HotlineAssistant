import { ReasonTypeEnum } from '../../../../enums/card/limits';

export interface CardBlockingRequestInterface {
  solarId?: number;
  cardId?: number;
  reasonType?: ReasonTypeEnum;
  reasonInformation?: any;
  reasonComment?: string;
}
