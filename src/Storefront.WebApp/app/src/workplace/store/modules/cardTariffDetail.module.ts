import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { TariffInterface } from '../../interfaces/tariff';
import { cardApi } from '../../api/card.api';
import { TariffDetailResponseInterface } from '../../interfaces/responses/tariff/detail';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { CardTariffDetailRequestInterface } from '../../interfaces/requests/card/tariff/cardTariffDetailRequest.interface';

class State extends BaseState {
  constructor () {
    super();
    this.detail = undefined;
  }

  public detail?: TariffInterface;
}

class CardTariffDetailGetters extends BaseGetters<State> {
  public get Detail () {
    return this.state.detail;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class CardTariffDetailMutations extends BaseMutations<State> {
  public updateDetail (payload: TariffInterface) {
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

class CardTariffDetailActions extends Actions<State, CardTariffDetailGetters, CardTariffDetailMutations, CardTariffDetailActions> {
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

  public updateDetail (payload: TariffInterface) : void {
    this.mutations.startLoading();
    this.mutations.updateDetail(payload);
    this.mutations.stopLoading();
  }

  public async getDetail (params: CardTariffDetailRequestInterface) : Promise<void> {
    const promise = cardApi.getCurrentTariffAsync(params).then((resp: TariffDetailResponseInterface) => {
      this.mutations.updateDetail(resp.item);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const cardTariffDetailStore = new Module({
  namespaced: true,
  state: State,
  getters: CardTariffDetailGetters,
  mutations: CardTariffDetailMutations,
  actions: CardTariffDetailActions
});

export const cardTariffDetailMapper = createMapper(cardTariffDetailStore);

export default cardTariffDetailStore;
