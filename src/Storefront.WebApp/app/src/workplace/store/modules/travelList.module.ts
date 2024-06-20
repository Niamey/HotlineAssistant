import { Vue } from 'vue-property-decorator';
import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import { travelApi } from '../../api/travel.api';
import { TravelModel } from '../../models/travel';
import { TravelListResponseInterface, TravelCountResponseInterface } from '../../interfaces/responses/travel';
import { TravelListRequestInterface } from '../../interfaces/requests/travel';
import root from '../root';
import BaseFilterMutations from '../base/filter/baseFilter.mutations';
import BaseFilterState from '../base/filter/baseFilter.state';
import BaseGetters from '../base/base.getters';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';

class State extends BaseFilterState {
  public isNextPageAvailable = false;
  public travels: Array<TravelModel> = [];
  public filtered: Array<TravelModel> = [];
  public totalRows = 0;
}

class TravelGetters extends BaseGetters<State> {
  public get Travels () {
    return this.state.hasFiltered ? this.state.filtered : this.state.travels;
  }

  public get TravelsCount () {
    return this.state.totalRows;
  }

  public get AvailableNextPage () {
    return this.state.isNextPageAvailable;
  }
}

class TravelMutations extends BaseFilterMutations<State> {
  public setTravels (payload: TravelListResponseInterface): void {
    this.state.travels = payload.rows;
    this.state.isNextPageAvailable = payload.isNextPageAvailable;
    super.clearFilter();
  }

  public setTotalRows (payload: TravelCountResponseInterface): void {
    this.state.totalRows = payload.totalRows;
  }

  public startLoading (): void {
    this.state.isLoading = true;
    this.state.travels = [];
    this.state.filtered = [];
  }

  public stopLoading () : void {
    this.state.isLoading = false;
  }

  public setFilter (filter: any) {
    this.state.filtered = this.state.travels.filter(x => (x.travelId === filter.travelId || !filter.travelId));
    this.state.hasFiltered = true;
  }

  public updateListItem (travel: TravelModel) {
    if (this.state.travels) {
      const index = this.state.travels.findIndex(t => t.travelId === travel.travelId);
      if (index > -1) {
        Vue.set(this.state.travels, index, Object.assign(this.state.travels[index], travel));
      }
    }
  }
}

class TravelActions extends Actions<State, TravelGetters, TravelMutations, TravelActions> {
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

  public applyFilter (filter: any) : void {
    this.mutations.setFilter(filter);
  }

  public updateListItem (travel: TravelModel) : void {
    this.mutations.updateListItem(travel);
  }

  public async getList (params: TravelListRequestInterface) : Promise<void> {
    this.mutations.startLoading();

    const promise = travelApi.getTravelListAsync(params).then((resp: TravelListResponseInterface) => {
      this.mutations.setTravels(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }

  public async getTotalRows (params: TravelListRequestInterface) : Promise<void> {
    this.mutations.startLoading();

    const promise = travelApi.getCountAsync(params).then((resp: TravelCountResponseInterface) => {
      this.mutations.setTotalRows(resp);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const travelListStore = new Module({
  namespaced: true,
  state: State,
  getters: TravelGetters,
  mutations: TravelMutations,
  actions: TravelActions
});

export const travelListMapper = createMapper(travelListStore);

export default travelListStore;
