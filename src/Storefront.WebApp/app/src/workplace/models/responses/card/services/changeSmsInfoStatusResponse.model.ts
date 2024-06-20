import { ChangeSmsInfoStatusModel } from '../../../card/services';
import { ChangeSmsInfoStatusResponseInterface } from '../../../../interfaces/responses/card/services';
import { ChangeSmsInfoStatusInterface } from '../../../../interfaces/card/services';

export class ChangeSmsInfoStatusResponseModel implements ChangeSmsInfoStatusResponseInterface {
  constructor (data: ChangeSmsInfoStatusInterface) {
    this.item = new ChangeSmsInfoStatusModel(data);
  }

  public item: ChangeSmsInfoStatusModel;
}
