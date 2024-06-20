import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { CardInterface } from '../../interfaces/card';
import { CardDetailRequestInterface } from '../../interfaces/requests/card/detail';
import { cardApi } from '../../api/card.api';
import { CardDetailResponseInterface } from '../../interfaces/responses/card/detail';
import BaseState from '../base/base.state';
import BaseMutations from '../base/base.mutations';
import BaseGetters from '../base/base.getters';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';

class State extends BaseState {
  constructor () {
    super();
    this.cardDetail = undefined;
  }

  public cardDetail?: CardInterface;
}

class CardDetailGetters extends BaseGetters<State> {
  public get Detail () {
    return this.state.cardDetail;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class CardDetailMutations extends BaseMutations<State> {
  public updateDetail (payload: CardInterface) {
    this.state.cardDetail = payload;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.cardDetail = undefined;
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class CardDetailActions extends Actions<State, CardDetailGetters, CardDetailMutations, CardDetailActions> {
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

  public updateDetail (payload: CardInterface) : void {
    this.mutations.startLoading();
    this.mutations.updateDetail(payload);
    this.mutations.stopLoading();
  }

  public async getDetail (params: CardDetailRequestInterface) : Promise<void> {
    const promise = cardApi.getDetailAsync(params).then((resp: CardDetailResponseInterface) => {
      this.mutations.updateDetail(resp.item);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const cardDetailStore = new Module({
  namespaced: true,
  state: State,
  getters: CardDetailGetters,
  mutations: CardDetailMutations,
  actions: CardDetailActions
});

export const cardDetailMapper = createMapper(cardDetailStore);

export default cardDetailStore;
