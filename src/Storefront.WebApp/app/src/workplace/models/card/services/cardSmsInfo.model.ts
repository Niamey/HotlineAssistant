import { CardSmsInfoDto } from '../../../dto/card/services';

export class CardSmsInfoModel extends CardSmsInfoDto {
  constructor (dto: CardSmsInfoDto) {
    super();
    Object.assign(this, dto);
  }
}
