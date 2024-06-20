import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import AccountModel from '../../models/account/account.model';
import { AccountCollectionResponseInterface } from '../../interfaces/responses/account';
import { accountApi } from '../../api/account.api';
import BaseFilterMutations from '../base/filter/baseFilter.mutations';
import BaseFilterState from '../base/filter/baseFilter.state';
import BaseGetters from '../base/base.getters';
import SearchAccountFilterModel from '../../models/filters/account/searchAccountFilter.model';
import SearchAccountRequestModel from '../../models/requests/account/searchAccountRequest.model';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';

class State extends BaseFilterState {
  public accounts: Array<AccountModel> = [];
  public filtered: Array<AccountModel> = [];
}

class AccountListGetters extends BaseGetters<State> {
  public get List () {
    return this.state.hasFiltered ? this.state.filtered : this.state.accounts;
  }

  public get HasMoreAccount () : boolean {
    return (this.state.accounts?.length ?? 0) > 1;
  }
}

class AccountListMutations extends BaseFilterMutations<State> {
  public setList (payload: AccountCollectionResponseInterface) {
    this.state.accounts = payload.rows;
    super.clearFilter();
  }

  public setFilter (filter: SearchAccountFilterModel) {
    this.state.filtered = this.state.accounts.filter(x => (x.accountId === filter.accountId || !filter.accountId) &&
      (x.agreementId === filter.agreementId || !filter.agreementId));
    this.state.hasFiltered = true;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.accounts = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class AccountListActions extends Actions<State, AccountListGetters, AccountListMutations, AccountListActions> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public stopLoading () : void {
    this.mutations.stopLoading();
  }

  public startLoading () : void {
    this.mutations.startLoading();
  }

  public async getList (params: SearchAccountRequestModel) : Promise<void> {
    const promise = accountApi.getAccountListAsync(params).then((resp: AccountCollectionResponseInterface) => {
      this.mutations.setList(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }

  public filterList (filter: SearchAccountFilterModel) : void {
    this.mutations.setFilter(filter);
  }
}

const clientAccountListStore = new Module({
  namespaced: true,
  state: State,
  getters: AccountListGetters,
  mutations: AccountListMutations,
  actions: AccountListActions
});

export const clientAccountListMapper = createMapper(clientAccountListStore);

export default clientAccountListStore;
