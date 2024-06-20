import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { cardApi } from '../../api/card.api';
import BaseState from '../base/base.state';
import BaseMutations from '../base/base.mutations';
import BaseGetters from '../base/base.getters';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { CardFullBalanceInterface } from '../../interfaces/card/balance/full/cardFullBalance.interface';
import { CardFullBalanceResponseInterface } from '../../interfaces/responses/card/balance/full/cardFullBalanceResponse.interface';
import { CardFullBalanceRequestInterface } from '../../interfaces/requests/card/balance/full/cardFullBalanceRequest.interface';

class State extends BaseState {
  constructor () {
    super();
    this.cardFullBalance = undefined;
  }

  public cardFullBalance?: CardFullBalanceInterface;
}

class CardFullBalanceGetters extends BaseGetters<State> {
  public get FullBalance () {
    return this.state.cardFullBalance;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class CardFullBalanceMutations extends BaseMutations<State> {
  public updateFullBalance (payload: CardFullBalanceInterface) {
    this.state.cardFullBalance = payload;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.cardFullBalance = undefined;
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class CardFullBalanceActions extends Actions<State, CardFullBalanceGetters, CardFullBalanceMutations, CardFullBalanceActions> {
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

  public updateFullBalance (payload: CardFullBalanceInterface) : void {
    this.mutations.startLoading();
    this.mutations.updateFullBalance(payload);
    this.mutations.stopLoading();
  }

  public async getFullBalance (params: CardFullBalanceRequestInterface) : Promise<void> {
    const promise = cardApi.getFullBalanceAsync(params).then((resp: CardFullBalanceResponseInterface) => {
      this.mutations.updateFullBalance(resp.item);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const cardFullBalanceStore = new Module({
  namespaced: true,
  state: State,
  getters: CardFullBalanceGetters,
  mutations: CardFullBalanceMutations,
  actions: CardFullBalanceActions
});

export const cardDetailBalanceMapper = createMapper(cardFullBalanceStore);

export default cardFullBalanceStore;
