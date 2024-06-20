import { ChangeSmsInfoStatusDto } from '../../../dto/card/services';

export class ChangeSmsInfoStatusModel extends ChangeSmsInfoStatusDto {
  constructor (dto: ChangeSmsInfoStatusDto) {
    super();
    Object.assign(this, dto);
  }
}
