import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { TravelInterface } from '../../interfaces/travel';
import { travelApi } from '../../api/travel.api';
import { TravelDetailResponseInterface } from '../../interfaces/responses/travel';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { TravelDetailRequestInterface } from '../../interfaces/requests/travel';

class State extends BaseState {
  constructor () {
    super();
    this.detail = undefined;
  }

  public detail?: TravelInterface;
}

class TravelDetailGetters extends BaseGetters<State> {
  public get Detail () {
    return this.state.detail;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class TravelDetailMutations extends BaseMutations<State> {
  public updateDetail (payload: TravelInterface) {
    this.state.detail = payload;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.detail = undefined;
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class TravelDetailActions extends Actions<State, TravelDetailGetters, TravelDetailMutations, TravelDetailActions> {
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

  public updateDetail (payload: TravelInterface) : void {
    this.mutations.startLoading();

    this.mutations.updateDetail(payload);

    this.mutations.stopLoading();
  }

  public async getDetail (params: TravelDetailRequestInterface) : Promise<void> {
    const promise = travelApi.getDetailAsync(params).then((resp: TravelDetailResponseInterface) => {
      this.mutations.updateDetail(resp.item);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const travelDetailStore = new Module({
  namespaced: true,
  state: State,
  getters: TravelDetailGetters,
  mutations: TravelDetailMutations,
  actions: TravelDetailActions
});

export const travelDetailMapper = createMapper(travelDetailStore);

export default travelDetailStore;
