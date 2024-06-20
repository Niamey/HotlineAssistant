import { ReasonTypeEnum } from '../../../enums/card/limits/reasonType.enum';

export interface CardBlockingInterface<T> {
  solarId?: number;
  cardId?: number;
  reasonType?: ReasonTypeEnum;
  reasonComment?: string;
  reasonInformation?: T
}
