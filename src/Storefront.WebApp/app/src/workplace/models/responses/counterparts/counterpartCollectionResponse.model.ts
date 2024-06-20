import CounterpartModel from '../../client/counterpart.model';
import { CounterpartCollectionResponseInterface } from '../../../interfaces/responses/client';
import { CounterpartInterface } from '../../../interfaces/client';
import { CounterpartDto } from '../../../dto/client';

export class CounterpartCollectionResponseModel implements CounterpartCollectionResponseInterface {
  constructor (isPageAvailable: boolean, items: Array<CounterpartInterface>) {
    this.isNextPageAvailable = isPageAvailable;
    this.rows = items.map((counterpartDto: CounterpartDto) => new CounterpartModel(counterpartDto));
  }

  public isNextPageAvailable: boolean;
  public rows: Array<CounterpartModel>;
}
