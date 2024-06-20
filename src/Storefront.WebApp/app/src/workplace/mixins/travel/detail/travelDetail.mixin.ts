import { Component, Vue } from 'vue-property-decorator';
import { travelDetailMapper } from '../../../store/modules/travelDetail.module';
import { TravelModel } from '../../../models/travel/travel.model';

const Mapper = Vue.extend({
  computed: travelDetailMapper.mapGetters({
    TravelDetail: 'Detail',
    TravelDetailLoading: 'Loading',
    TravelDetailException: 'Exception',
    TravelDetailSuccess: 'Success',
    TravelDetailHasError: 'HasError'
  }),
  methods: travelDetailMapper.mapActions({
    getTravelDetail: 'getDetail',
    updateDetail: 'updateDetail',
    startLoadingTravelDetail: 'startLoading',
    stopLoadingTravelDetail: 'stopLoading'
  })
});

@Component
export default class TravelDetailMixin extends Mapper {
  stopLoadingTravelDetail!: () => any;
  startLoadingTravelDetail!: () => any;

  public onTravelDetailUpdate (payload : TravelModel) {
    void this.updateDetail(payload);
  }

  public async loadTravelDetail (solarId: number, travelId: number) : Promise<void> {
    if (this.TravelDetail?.solarId !== solarId || this.TravelDetail?.travelId !== travelId) {
      await this.getTravelDetail({ solarId: solarId, travelId: travelId });
    }
  }
}
