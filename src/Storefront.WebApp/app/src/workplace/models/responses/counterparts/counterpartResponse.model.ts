import CounterpartModel from '../../client/counterpart.model';
import { CounterpartResponseInterface } from '../../../interfaces/responses/client';
import { CounterpartInterface } from '../../../interfaces/client';

export class CounterpartResponseModel implements CounterpartResponseInterface {
  constructor (data: CounterpartInterface) {
    this.item = new CounterpartModel(data);
  }

  public item: CounterpartModel;
}
