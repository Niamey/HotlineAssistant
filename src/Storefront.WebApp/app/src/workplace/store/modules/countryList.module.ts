import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import { CountryInterface } from '../../interfaces/country';
import { countryApi } from '../../api/country.api';
import BaseState from '../base/base.state';
import BaseGetters from '../base/base.getters';
import BaseMutations from '../base/base.mutations';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { CountryListRequestInterface } from '../../interfaces/requests/country';
import { CountryListResponseInterface } from '../../interfaces/responses/country';

class State extends BaseState {
  constructor () {
    super();
    this.countries = [];
  }

  public countries: Array<CountryInterface>;
}

class CountryListGetters extends BaseGetters<State> {
  public get Countries () {
    return this.state.countries;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class CountryListMutations extends BaseMutations<State> {
  public setCountries (payload: CountryListResponseInterface) {
    this.state.countries = payload.rows;
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.countries = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class CountryListActions extends Actions<State, CountryListGetters, CountryListMutations, CountryListActions> {
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

  public async getCountryList (params: CountryListRequestInterface) : Promise<void> {
    const promise = countryApi.getCountryListAsync(params).then((resp: CountryListResponseInterface) => {
      this.mutations.setCountries(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const countryListStore = new Module({
  namespaced: true,
  state: State,
  getters: CountryListGetters,
  mutations: CountryListMutations,
  actions: CountryListActions
});

export const countryListMapper = createMapper(countryListStore);

export default countryListStore;
