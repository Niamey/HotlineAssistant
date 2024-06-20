import { MoneyBoxDto } from '../../../dto/agreement/moneybox';

export default class MoneyBoxModel extends MoneyBoxDto {
  constructor (dto: MoneyBoxDto) {
    super();
    Object.assign(this, dto);
  }
}