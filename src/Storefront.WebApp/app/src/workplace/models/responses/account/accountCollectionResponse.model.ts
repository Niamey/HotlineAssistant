
import { AccountCollectionResponseInterface } from '../../../interfaces/responses/account';
import { AccountDto } from '../../../dto/account';
import AccountModel from '../../account/account.model';
import { AccountInterface } from '../../../interfaces/account/account.interface';

export class AccountCollectionResponseModel implements AccountCollectionResponseInterface {
  constructor (items: Array<AccountInterface>) {
    this.rows = items.map((accountDto: AccountDto) => new AccountModel(accountDto));
  }

  public rows: Array<AccountModel>;
}
