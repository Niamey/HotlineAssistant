import { TravelModel } from '../../../models/travel';

export interface TravelListResponseInterface {
  rows: Array<TravelModel>;
  isNextPageAvailable: boolean;
}
