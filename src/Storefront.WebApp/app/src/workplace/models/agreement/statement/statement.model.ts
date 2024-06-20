import { StatementDto } from '../../../dto/agreement/statement';

export default class StatementModel extends StatementDto {
  constructor (dto: StatementDto) {
    super();
    Object.assign(this, dto);
  }
}