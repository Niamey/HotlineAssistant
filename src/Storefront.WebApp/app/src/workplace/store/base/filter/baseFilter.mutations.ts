import BaseFilterState from './baseFilter.state';
import BaseMutations from '../base.mutations';

export default class BaseFilterMutations<TState extends BaseFilterState> extends BaseMutations<TState> {
  public startLoading () {
    super.startLoading();
    this.state.hasFiltered = false;
    this.state.filtered = [];
  }

  public clearFilter () {
    this.state.hasFiltered = false;
    this.state.filtered = [];
  }
}
