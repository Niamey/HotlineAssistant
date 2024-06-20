import { AgreementStatusHistoryDto } from '../../../dto/agreement/status';

export default class AgreementStatusHistoryModel extends AgreementStatusHistoryDto {
  constructor (dto: AgreementStatusHistoryDto) {
    super();
    Object.assign(this, dto);
  }
}
