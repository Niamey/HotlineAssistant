import {
  MoneyBoxInterface,
  MoneyInterface
} from '../../../interfaces/agreement/moneybox/moneyBox.interface';
import { MoneyBoxStatusEnum } from '../../../enums/agreement/moneyBoxStatus.enum';
export class MoneyBoxDto implements MoneyBoxInterface {
  id!: number;
  savingId?: number;
  name?: string;
  currency?: string;
  amount?: MoneyInterface;
  status?: MoneyBoxStatusEnum;
}