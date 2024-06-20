import { AccountTypeEnum } from '../../enums/account/accountType.enum';
import { AccountStatusEnum } from '../../enums/account/accountStatus.enum';
import { AccountInterface } from '../../interfaces/account/account.interface';

export class AccountDto implements AccountInterface {
  accountId!: number;
  solarId?: number;
  agreementId?: number;
  number?: string;
  currency?: string;
  type?: AccountTypeEnum;
  status?: AccountStatusEnum;
}
