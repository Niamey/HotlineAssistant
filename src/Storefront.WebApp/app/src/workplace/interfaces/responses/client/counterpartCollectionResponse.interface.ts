import CounterpartModel from '../../../models/client/counterpart.model';

export interface CounterpartCollectionResponseInterface {
  isNextPageAvailable: boolean;
  rows: Array<CounterpartModel>;
}
