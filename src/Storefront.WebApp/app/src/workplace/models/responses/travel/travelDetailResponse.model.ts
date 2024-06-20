import { TravelModel } from '../../travel/travel.model';
import { TravelDetailResponseInterface } from '../../../interfaces/responses/travel';
import { TravelInterface } from '../../../interfaces/travel';

export class TravelDetailResponseModel implements TravelDetailResponseInterface {
  constructor (data: TravelInterface) {
    this.item = new TravelModel(data);
  }

  public item: TravelModel;
}
