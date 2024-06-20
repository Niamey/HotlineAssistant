import { Actions, Context, createMapper, Module } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import MoneyBoxModel from '../../models/agreement/moneybox/moneyBox.model';
import { MoneyBoxListResponseInterface } from '../../interfaces/responses/agreement/moneybox/moneyBoxListResponse.interface';
import MoneyBoxListRequestModel from '../../models/requests/agreement/moneyBoxListRequest.model';
import { agreementApi } from '../../api/agreement.api';

class State extends BaseState {
  public moneyBoxes: Array<MoneyBoxModel> = [];
}

class MoneyBoxListGetters extends BaseGetters<State> {
  public get MoneyBoxes () {
    return this.state.moneyBoxes;
  }
}

class MoneyBoxListMutations extends BaseMutations<State> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public setMoneyBoxes (payload: MoneyBoxListResponseInterface) {
    this.state.moneyBoxes = payload.rows;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.moneyBoxes = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class MoneyBoxListActions extends Actions<State, MoneyBoxListGetters, MoneyBoxListMutations, MoneyBoxListActions> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public startLoading () {
    this.mutations.startLoading();
  }

  public stopLoading () {
    this.mutations.stopLoading();
  }

  public async getMoneyBoxList (params: MoneyBoxListRequestModel): Promise<void> {
    const promise = agreementApi.getMoneyBoxesAsync(params).then((resp: MoneyBoxListResponseInterface) => {
      this.mutations.setMoneyBoxes(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const moneyBoxListStore = new Module({
  namespaced: true,
  state: State,
  getters: MoneyBoxListGetters,
  mutations:  MoneyBoxListMutations,
  actions:  MoneyBoxListActions
});

export const moneyBoxListMapper = createMapper(moneyBoxListStore);

export default moneyBoxListStore;