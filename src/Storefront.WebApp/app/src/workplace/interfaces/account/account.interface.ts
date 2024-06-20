import { AccountStatusEnum } from '../../enums/account/accountStatus.enum';
import { BaseCollectionInterface } from '../base/baseCollection.interface';
import { AccountTypeEnum } from '../../enums/account/accountType.enum';

export interface AccountInterface extends BaseCollectionInterface {
  accountId: number,
  solarId?: number,
  agreementId?: number,
  number?: string,
  currency?: string,
  type?: AccountTypeEnum;
  status?: AccountStatusEnum;
}
