import BaseState from '../base.state';

export default class BaseFilterState extends BaseState {
  public filtered: Array<any> = [];
  public hasFiltered = false;
}
