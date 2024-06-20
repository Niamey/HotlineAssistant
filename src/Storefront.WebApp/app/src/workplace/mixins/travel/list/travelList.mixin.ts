import { Component, Vue } from 'vue-property-decorator';
import { travelListMapper } from '../../../store/modules/travelList.module';
import constants from '../../../common/constants';
import TravelListRequestModel from '../../../models/requests/travel/travelListRequest.model';
import { SortDirectionEnum } from '../../../enums/client/search/sortDirection.enum';
import { Route } from 'vue-router';
import { NavigateQueryHelper } from '../../../helpers/navigateQuery.helper';
import { TravelModel } from '../../../models/travel';

const Mapper = Vue.extend({
  computed: travelListMapper.mapGetters({
    Travels: 'Travels',
    TravelsCount: 'TravelsCount',
    AvailableNextPage: 'AvailableNextPage',
    TravelsLoading: 'Loading',
    TravelsHasError: 'HasError',
    TravelsSuccess: 'Success'
  }),
  methods: travelListMapper.mapActions({
    getTravels: 'getList',
    getTravelsCount: 'getTotalRows',
    startLoadingTravels: 'startLoading',
    stopLoadingTravels: 'stopLoading',
    applyFilterTravels: 'applyFilter',
    updateTravelListItem: 'updateListItem'
  })
});

@Component
export default class TravelListMixin extends Mapper {
  startLoadingTravels!: () => any;
  stopLoadingTravels!: () => any;

  private searchParams: TravelListRequestModel = new TravelListRequestModel();

  prepareParams (solarId: number, currentPage?: number, pageSize?: number, sortColumn?: string, sortDirection?: SortDirectionEnum) {
    this.searchParams.solarId = solarId;
    if (currentPage !== undefined && currentPage !== null) {
      this.searchParams.pageIndex = currentPage;
    } else {
      this.searchParams.pageIndex = constants.paging.pageIndex;
    }
    if (pageSize !== undefined && pageSize !== null) {
      this.searchParams.pageSize = pageSize;
    } else {
      this.searchParams.pageSize = constants.paging.pageSize;
    }
    if (sortColumn !== undefined && sortColumn !== null) {
      this.searchParams.sortColumn = sortColumn;
    }
    if (sortDirection !== undefined && sortDirection !== null) {
      this.searchParams.sortDirection = sortDirection;
    }
  }

  beforeRouteUpdate (to: Route, from: Route, next:any) {
    if (NavigateQueryHelper.travels.getTravelId(to) !== NavigateQueryHelper.travels.getTravelId(from)) {
      this.$bus.emit('travel-aggregator-detail-update', undefined);

      if (this.Travels?.length === 0) {
        this.$bus.emit('travel-aggregator-list-update');
      }
    }

    this.filteringTravelList(to);

    next();
  }

  public async onTravelListUpdate () {
    if (!NavigateQueryHelper.travels.hasTravelId(this.$route)) {
      this.$bus.emit('travel-aggregator-detail-update', undefined);
    }
    await this.getTravelList(parseInt(this.$route.params.id));
  }

  public async updateTravelList (travel: TravelModel) {
    await this.updateTravelListItem(travel);
  }

  public async getTravelList (solarId: number, currentPage?: number,
    pageSize?: number, sortColumn?: string, sortDirection?: SortDirectionEnum): Promise<void> {
    this.prepareParams(solarId, currentPage, pageSize, sortColumn, sortDirection);
    await this.getTravelsCount({ solarId: this.searchParams.solarId });
    await this.getTravels(this.searchParams);
  }

  async getTravelsByPage (currentPage: number, solarId: number) {
    this.prepareParams(solarId);
    this.searchParams.pageIndex = currentPage;
    await this.getTravels(this.searchParams);
  }

  private static prepareFilterFromRoute (route: Route) {
    return {
      travelId: NavigateQueryHelper.travels.getTravelId(route)
    };
  }

  private filteringTravelList (route: Route) {
    const searchFilter = TravelListMixin.prepareFilterFromRoute(route);
    void this.applyFilterTravels(searchFilter);

    if (this.Travels?.length === 1) {
      this.$bus.emit('travel-aggregator-detail-update', this.Travels[0]);
    }
  }
}
