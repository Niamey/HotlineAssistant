import { ReasonTypeEnum } from '../../../enums/card/limits/reasonType.enum';
import { CardBlockingInterface } from '../../../interfaces/card/limits/cardBlocking.interface';

export class CardBlockingDto<T> implements CardBlockingInterface<T> {
  solarId?: number;
  cardId?: number;
  reasonType?: ReasonTypeEnum;
  reasonComment?: string;
  reasonInformation?: T;
}
