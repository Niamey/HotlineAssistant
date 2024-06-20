import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { TariffInterface } from '../../interfaces/tariff';
import { agreementApi } from '../../api/agreement.api';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { AgreementTariffListRequestInterface } from '../../interfaces/requests/agreement/tariff/agreementTariffListRequest.interface';
import { TariffListResponseInterface } from '../../interfaces/responses/tariff/list';

class State extends BaseState {
  constructor () {
    super();
    this.agreementTariffs = [];
  }

  public agreementTariffs: Array<TariffInterface>;
}

class AgreementTariffListGetters extends BaseGetters<State> {
  public get List () {
    return this.state.agreementTariffs;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class AgreementTariffListMutations extends BaseMutations<State> {
  public setItems (payload: TariffListResponseInterface) {
    this.state.agreementTariffs = payload.rows;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.agreementTariffs = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class AgreementTariffListActions extends Actions<State, AgreementTariffListGetters, AgreementTariffListMutations, AgreementTariffListActions> {
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

  public async getList (params: AgreementTariffListRequestInterface) : Promise<void> {
    const promise = agreementApi.getAllTariffAsync(params).then((resp: TariffListResponseInterface) => {
      this.mutations.setItems(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const agreementTariffListStore = new Module({
  namespaced: true,
  state: State,
  getters: AgreementTariffListGetters,
  mutations: AgreementTariffListMutations,
  actions: AgreementTariffListActions
});

export const agreementTariffListMapper = createMapper(agreementTariffListStore);

export default agreementTariffListStore;
