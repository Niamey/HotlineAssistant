import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import { clientApi } from '../../api/client.api';
import { CounterpartInterface } from '../../interfaces/client';
import { SearchClientDetailRequestInterface } from '../../interfaces/requests/client/detail';
import { CounterpartResponseInterface } from '../../interfaces/responses/client';
import root from '../root';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';

class State extends BaseState {
  public detail?: CounterpartInterface = undefined;
  public showHeaderData = false;
}

class ClientDetailGetters extends BaseGetters<State> {
  public get Detail () {
    return this.state.detail;
  }

  public get ShowHeaderData () {
    return this.state.showHeaderData;
  }
}

class ClientDetailMutations extends BaseMutations<State> {
  public updateDetail (payload: CounterpartInterface) {
    this.state.detail = payload;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.detail = {};
  }

  public stopLoading () {
    this.state.isLoading = false;
  }

  public showData () {
    this.state.showHeaderData = true;
  }

  public hideData () {
    this.state.showHeaderData = false;
  }
}

class ClientDetailActions extends Actions<State, ClientDetailGetters, ClientDetailMutations, ClientDetailActions> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public startLoading () : void {
    this.mutations.startLoading();
  }

  public stopLoading () : void {
    this.mutations.stopLoading();
  }

  public showData () {
    this.mutations.showData();
  }

  public hideData () {
    this.mutations.hideData();
  }

  public updateDetail (payload: CounterpartInterface) : void {
    this.mutations.startLoading();
    this.mutations.updateDetail(payload);
    this.mutations.stopLoading();
  }

  public async getDetail (params: SearchClientDetailRequestInterface) : Promise<void> {
    this.mutations.startLoading();
    const promise = clientApi.getDetailAsync(params).then((resp: CounterpartResponseInterface) => {
      this.mutations.updateDetail(resp.item);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const clientDetailStore = new Module({
  namespaced: true,
  state: State,
  getters: ClientDetailGetters,
  mutations: ClientDetailMutations,
  actions: ClientDetailActions
});

export const clientDetailMapper = createMapper(clientDetailStore);

export default clientDetailStore;
