import { TravelDto } from '../../dto/travel';

export class TravelModel extends TravelDto {
  constructor (dto: TravelDto) {
    super();
    Object.assign(this, dto);
  }
}
