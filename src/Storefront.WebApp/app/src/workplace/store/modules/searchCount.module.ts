import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import { CounterpartCountResponseInterface } from '../../interfaces/responses/client';
import { SearchCountCounterpartRequestInterface } from '../../interfaces/requests/client/search';
import { clientApi } from '../../api/client.api';
import root from '../root';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';

class State extends BaseState {
  public totalRows?: number = 0;
}

class SearchCountGetters extends BaseGetters<State> {
  public get TotalRows () {
    return this.state.totalRows;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class SearchCountMutations extends BaseMutations<State> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public setTotalRows (payload: CounterpartCountResponseInterface) {
    this.state.totalRows = payload.totalRows;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.totalRows = 0;
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class SearchCountActions extends Actions<State, SearchCountGetters, SearchCountMutations, SearchCountActions> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public startLoadingTotalRows () {
    this.mutations.startLoading();
  }

  public stopLoadingTotalRows () {
    this.mutations.stopLoading();
  }

  public async findTotalRows (params: SearchCountCounterpartRequestInterface) : Promise<void> {
    const promise = clientApi.getTotalAsync(params).then((resp: CounterpartCountResponseInterface) => {
      this.mutations.setTotalRows(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const searchCountStore = new Module({
  namespaced: true,
  state: State,
  getters: SearchCountGetters,
  mutations: SearchCountMutations,
  actions: SearchCountActions
});

export const searchCountMapper = createMapper(searchCountStore);

export default searchCountStore;
