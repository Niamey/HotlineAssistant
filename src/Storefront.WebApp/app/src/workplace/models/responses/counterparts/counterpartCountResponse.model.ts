import { CounterpartCountResponseInterface } from '../../../interfaces/responses/client';

export class CounterpartCountResponseModel implements CounterpartCountResponseInterface {
  constructor (value: number) {
    this.totalRows = value;
  }

  public totalRows: number;
}
