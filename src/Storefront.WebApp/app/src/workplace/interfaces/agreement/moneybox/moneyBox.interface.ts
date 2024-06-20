import { MoneyBoxStatusEnum } from '../../../enums/agreement/moneyBoxStatus.enum';
export interface MoneyInterface {
  amount?: number;
  currency?: string;
}
export interface MoneyBoxInterface {
  id: number;
  savingId?: number;
  name?: string;
  currency?: string;
  amount?: MoneyInterface;
  status?: MoneyBoxStatusEnum;
}