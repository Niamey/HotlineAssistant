import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { TariffInterface } from '../../interfaces/tariff';
import { agreementApi } from '../../api/agreement.api';
import { TariffDetailResponseInterface } from '../../interfaces/responses/tariff/detail';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { AgreementTariffDetailRequestInterface } from '../../interfaces/requests/agreement/tariff/agreementTariffDetailRequest.interface';

class State extends BaseState {
  constructor () {
    super();
    this.detail = undefined;
  }

  public detail?: TariffInterface;
}

class AgreementTariffDetailGetters extends BaseGetters<State> {
  public get Detail () {
    return this.state.detail;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class AgreementTariffDetailMutations extends BaseMutations<State> {
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

class AgreementTariffDetailActions extends Actions<State, AgreementTariffDetailGetters, AgreementTariffDetailMutations, AgreementTariffDetailActions> {
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

  public async getDetail (params: AgreementTariffDetailRequestInterface) : Promise<void> {
    const promise = agreementApi.getCurrentTariffAsync(params).then((resp: TariffDetailResponseInterface) => {
      this.mutations.updateDetail(resp.item);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const agreementTariffDetailStore = new Module({
  namespaced: true,
  state: State,
  getters: AgreementTariffDetailGetters,
  mutations: AgreementTariffDetailMutations,
  actions: AgreementTariffDetailActions
});

export const agreementTariffDetailMapper = createMapper(agreementTariffDetailStore);

export default agreementTariffDetailStore;
