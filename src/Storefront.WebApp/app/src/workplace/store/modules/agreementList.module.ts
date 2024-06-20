import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import AgreementModel from '../../models/agreement/agreement.model';
import { AgreementListResponseInterface } from '../../interfaces/responses/agreement/list';
import { AgreementListRequestInterface } from '../../interfaces/requests/agreement/agreementListRequest.interface';
import { agreementApi } from '../../api/agreement.api';
import BaseFilterMutations from '../base/filter/baseFilter.mutations';
import BaseFilterState from '../base/filter/baseFilter.state';
import BaseGetters from '../base/base.getters';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { AgreementFilterInterface } from '../../interfaces/agreement/agreementFilter.interface';
import { AgreementStatusEnum } from '../../enums/agreement/agreementStatus.enum';

class State extends BaseFilterState {
  constructor () {
    super();
    this.agreements = [];
    this.filtered = [];
  }

  public agreements: Array<AgreementModel>;
  public filtered: Array<AgreementModel>;
}

class AgreementListGetters extends BaseGetters<State> {
  public get List () {
    return this.state.hasFiltered ? this.state.filtered : this.state.agreements;
  }

  public get HasMoreAgreement () : boolean {
    return (this.state.agreements?.length ?? 0) > 1;
  }
}

class AgreementListMutations extends BaseFilterMutations<State> {
  public setItems (payload: AgreementListResponseInterface) {
    this.state.agreements = payload.rows;
    super.clearFilter();
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.agreements = [];
    this.state.filtered = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }

  public setFilter (filter: AgreementFilterInterface) {
    this.state.filtered = this.state.agreements.filter(x => (x.agreementId === filter.agreementId || !filter.agreementId) &&
      ((x.status === AgreementStatusEnum.Active && !filter.isInactive) || (x.status !== AgreementStatusEnum.Active && filter.isInactive)));
    this.state.hasFiltered = true;
  }
}

class AgreementListActions extends Actions<State, AgreementListGetters, AgreementListMutations, AgreementListActions> {
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

  public applyFilter (filter: AgreementFilterInterface) : void {
    this.mutations.setFilter(filter);
  }

  public async getList (params: AgreementListRequestInterface) : Promise<void> {
    const promise = agreementApi.getAgreementListAsync(params).then((resp: AgreementListResponseInterface) => {
      this.mutations.setItems(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const agreementListStore = new Module({
  namespaced: true,
  state: State,
  getters: AgreementListGetters,
  mutations: AgreementListMutations,
  actions: AgreementListActions
});

export const agreementListMapper = createMapper(agreementListStore);

export default agreementListStore;
