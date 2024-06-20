import { ServiceDto } from '../../dto/service';

export default class ServiceModel extends ServiceDto {
  constructor (dto: ServiceDto) {
    super();
    Object.assign(this, dto);
  }
}
