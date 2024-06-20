import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import { clientApi } from '../../api/client.api';
import { SelectOptionType } from '../../types';
import CounterpartModel from '../../models/client/counterpart.model';
import { CounterpartCollectionResponseInterface } from '../../interfaces/responses/client';
import { SearchCounterpartRequestInterface } from '../../interfaces/requests/client/search';
import root from '../root';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';

class State extends BaseState {
  public isNextPageAvailable = false;
  public clients: Array<CounterpartModel> = [];
  public category?: SelectOptionType;
}

class SearchGetters extends BaseGetters<State> {
  public get Clients () {
    return this.state.clients;
  }

  public get AvailableNextPage () {
    return this.state.isNextPageAvailable;
  }

  public get SearchCategory () {
    return this.state.category;
  }
}

class SearchMutations extends BaseMutations<State> {
  public setClients (payload: CounterpartCollectionResponseInterface): void {
    this.state.clients = payload.rows;
    this.state.isNextPageAvailable = payload.isNextPageAvailable;
  }

  public startLoading (): void {
    this.state.isLoading = true;
    this.state.clients = [];
  }

  public stopLoading () : void {
    this.state.isLoading = false;
  }

  public setSearchCategory (payload: SelectOptionType): void {
    this.state.category = payload;
  }
}

class SearchActions extends Actions<State, SearchGetters, SearchMutations, SearchActions> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public startLoading () : void {
    this.mutations.startLoading();
  }

  public async findClients (params: SearchCounterpartRequestInterface) : Promise<void> {
    this.mutations.startLoading();
    const promise = clientApi.getClientListAsync(params).then((resp: CounterpartCollectionResponseInterface) => {
      this.mutations.setClients(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }

  public setCategory (data: SelectOptionType) {
    this.mutations.setSearchCategory(data);
    this.mutations.stopLoading();
  }
}

const searchClientStore = new Module({
  namespaced: true,
  state: State,
  getters: SearchGetters,
  mutations: SearchMutations,
  actions: SearchActions
});

export const searchClientMapper = createMapper(searchClientStore);

export default searchClientStore;
