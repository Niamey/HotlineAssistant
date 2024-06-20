import { CardBlockingDto } from '../../../dto/card/limits';

export class CardBlockingModel<T> extends CardBlockingDto<T> {
  constructor (dto: CardBlockingDto<T>) {
    super();
    Object.assign(this, dto);
  }
}
