import { ChangeSmsInfoStatusInterface } from '../../../interfaces/card/services';
import { ChangeSmsInfoStatusEnum } from '../../../enums/card/services/changeSmsInfoStatus.enum';

export class ChangeSmsInfoStatusDto implements ChangeSmsInfoStatusInterface {
  status?: ChangeSmsInfoStatusEnum;
  message?: string = '';
}
