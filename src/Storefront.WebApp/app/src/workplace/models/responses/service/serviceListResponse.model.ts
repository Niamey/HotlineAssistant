import { ServiceListResponseInterface } from '../../../interfaces/responses/service';
import { ServiceDto } from '../../../dto/service';
import ServiceModel from '../../service/service.model';
import { ServiceInterface } from '../../../interfaces/service';

export default class ServiceListResponseModel implements ServiceListResponseInterface {
  constructor (items: Array<ServiceInterface>) {
    this.rows = items.map((serviceDto: ServiceDto) => new ServiceModel(serviceDto));
  }

  public rows: Array<ServiceModel>;
}
