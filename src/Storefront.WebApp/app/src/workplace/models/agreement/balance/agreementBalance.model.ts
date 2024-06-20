import { AgreementBalanceDto } from '../../../dto/agreement/balance/agreementBalance.dto';

export default class AgreementBalanceModel extends AgreementBalanceDto {
  constructor (dto: AgreementBalanceDto) {
    super();
    Object.assign(this, dto);
  }
}
