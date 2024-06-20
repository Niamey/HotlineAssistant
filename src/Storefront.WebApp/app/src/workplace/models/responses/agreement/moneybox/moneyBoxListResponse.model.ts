import MoneyBoxModel from '../../../agreement/moneybox/moneyBox.model';
import { MoneyBoxListResponseInterface } from '../../../../interfaces/responses/agreement/moneybox/moneyBoxListResponse.interface';
import { MoneyBoxInterface } from '../../../../interfaces/agreement/moneybox/moneyBox.interface';
import { MoneyBoxDto } from '../../../../dto/agreement/moneybox/moneyBox.dto';

export class MoneyBoxListResponseModel implements MoneyBoxListResponseInterface {
  constructor (items: Array<MoneyBoxInterface>) {
    this.rows = items.map((moneyboxDto: MoneyBoxDto) => new MoneyBoxModel(moneyboxDto));
  }

  public rows: Array<MoneyBoxModel>;
}