import { TravelCountResponseInterface } from '../../../interfaces/responses/travel';

export class TravelCountResponseModel implements TravelCountResponseInterface {
  constructor (value: number) {
    this.totalRows = value;
  }

  public totalRows: number;
}
