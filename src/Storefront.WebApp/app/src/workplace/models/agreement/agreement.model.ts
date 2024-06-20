import { AgreementDto } from '../../dto/agreement';

export default class AgreementModel extends AgreementDto {
  constructor (dto: AgreementDto) {
    super();
    Object.assign(this, dto);
  }
}
