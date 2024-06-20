import StatementModel from '../../../agreement/statement/statement.model';
import { StatementResponseInterface } from '../../../../interfaces/responses/agreement/statement';
import { StatementInterface } from '../../../../interfaces/agreement/statement';

export class StatementResponseModel implements StatementResponseInterface {
  constructor (data: StatementInterface) {
    this.item = new StatementModel(data);
  }

  public item: StatementModel;
}