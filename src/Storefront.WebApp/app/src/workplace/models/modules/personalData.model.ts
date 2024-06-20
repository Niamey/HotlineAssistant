import { PersonalDataDto } from '../../dto/modules';

export class PersonalDataModel extends PersonalDataDto {
  constructor (dto: PersonalDataDto) {
    super();
    Object.assign(this, dto);
  }
}
