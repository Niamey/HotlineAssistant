import { Actions, Context, createMapper, Module } from 'vuex-smart-module';
import { Store } from 'vuex';
import ServiceModel from '../../models/service/service.model';
import root from '../root';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { ServiceListResponseInterface } from '../../interfaces/responses/service';
import ServiceListRequestModel from '../../models/requests/service/serviceListRequest.model';
import { uiSettingsApi } from '../../api/uiSettings.api';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';

class State extends BaseState {
  public services: Array<ServiceModel> = [];
}

class ServiceListGetters extends BaseGetters<State> {
  public get Services () {
    return this.state.services;
  }
}

class ServiceListMutations extends BaseMutations<State> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public setServices (payload: ServiceListResponseInterface) {
    this.state.services = payload.rows;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.services = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class ServiceListActions extends Actions<State, ServiceListGetters, ServiceListMutations, ServiceListActions> {
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

  public async getServiceList (params: ServiceListRequestModel): Promise<void> {
    const promise = uiSettingsApi.getServiceListAsync(params).then((resp: ServiceListResponseInterface) => {
      this.mutations.setServices(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const serviceListStore = new Module({
  namespaced: true,
  state: State,
  getters: ServiceListGetters,
  mutations: ServiceListMutations,
  actions: ServiceListActions
});

export const serviceListMapper = createMapper(serviceListStore);

export default serviceListStore;
