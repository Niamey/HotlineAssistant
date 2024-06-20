import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { TariffInterface } from '../../interfaces/tariff';
import { cardApi } from '../../api/card.api';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { CardTariffListRequestInterface } from '../../interfaces/requests/card/tariff/cardTariffListRequest.interface';
import { TariffListResponseInterface } from '../../interfaces/responses/tariff/list';

class State extends BaseState {
  constructor () {
    super();
    this.cardTariffs = [];
  }

  public cardTariffs: Array<TariffInterface>;
}

class CardTariffListGetters extends BaseGetters<State> {
  public get List () {
    return this.state.cardTariffs;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class CardTariffListMutations extends BaseMutations<State> {
  public setItems (payload: TariffListResponseInterface) {
    this.state.cardTariffs = payload.rows;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.cardTariffs = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class CardTariffListActions extends Actions<State, CardTariffListGetters, CardTariffListMutations, CardTariffListActions> {
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

  public async getList (params: CardTariffListRequestInterface) : Promise<void> {
    const promise = cardApi.getAllTariffAsync(params).then((resp: TariffListResponseInterface) => {
      this.mutations.setItems(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const cardTariffListStore = new Module({
  namespaced: true,
  state: State,
  getters: CardTariffListGetters,
  mutations: CardTariffListMutations,
  actions: CardTariffListActions
});

export const cardTariffListMapper = createMapper(cardTariffListStore);

export default cardTariffListStore;
