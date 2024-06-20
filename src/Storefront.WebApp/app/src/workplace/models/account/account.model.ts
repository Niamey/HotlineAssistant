import { AccountDto } from '../../dto/account';

export default class AccountModel extends AccountDto {
  constructor (dto: AccountDto) {
    super();
    Object.assign(this, dto);
  }
}
