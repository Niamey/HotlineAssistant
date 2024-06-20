import { TravelModel } from '../../travel';
import { TravelListResponseInterface } from '../../../interfaces/responses/travel';
import { TravelInterface } from '../../../interfaces/travel';
import { TravelDto } from '../../../dto/travel';

export class TravelListResponseModel implements TravelListResponseInterface {
  public rows: Array<TravelModel>;
  public isNextPageAvailable: boolean;

  constructor (items: Array<TravelInterface>, isNextPageAvailable: boolean) {
    this.rows = items.map((travelDto: TravelDto) => new TravelModel(travelDto));
    this.isNextPageAvailable = isNextPageAvailable;
  }
}