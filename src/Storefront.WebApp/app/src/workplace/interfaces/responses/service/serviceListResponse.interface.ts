import ServiceModel from '../../../models/service/service.model';

export interface ServiceListResponseInterface {
  rows: Array<ServiceModel>;
}
