import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import MenuLeftModel from '../../models/menu/menuLeft.model';
import { MenuLeftResponseInterface } from '../../interfaces/responses/menu';
import { uiSettingsApi } from '../../api/uiSettings.api';
import BaseState from '../base/filter/baseFilter.state';
import BaseGetters from '../base/base.getters';
import MenuLeftRequestModel from '../../models/requests/menu/menuLeftRequest.model';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import BaseMutations from '../base/base.mutations';

class State extends BaseState {
  public details: Array<MenuLeftModel> = [];
}

class MenuLeftGetters extends BaseGetters<State> {
  public get List () {
    return this.state.details;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class MenuLeftMutations extends BaseMutations<State> {
  public setList (payload: MenuLeftResponseInterface) {
    this.state.details = payload.rows;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.details = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class MenuLeftActions extends Actions<State, MenuLeftGetters, MenuLeftMutations, MenuLeftActions> {
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

  public async getList (params: MenuLeftRequestModel) : Promise<void> {
    const promise = uiSettingsApi.getMenuLeftAsync(params).then((resp: MenuLeftResponseInterface) => {
      this.mutations.setList(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const menuLeftStore = new Module({
  namespaced: true,
  state: State,
  getters: MenuLeftGetters,
  mutations: MenuLeftMutations,
  actions: MenuLeftActions
});

export const menuLeftMapper = createMapper(menuLeftStore);

export default menuLeftStore;
