import { LimitsTransactionDto } from '../../../../dto/card/limits';

export class LimitsTransactionModel extends LimitsTransactionDto {
  constructor (dto: LimitsTransactionDto) {
    super();
    Object.assign(this, dto);
  }
}
