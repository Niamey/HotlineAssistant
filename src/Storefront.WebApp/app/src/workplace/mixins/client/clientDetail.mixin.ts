import { Component, Vue } from 'vue-property-decorator';
import { clientDetailMapper } from '../../store/modules/clientDetail.module';
import CounterpartModel from '../../models/client/counterpart.model';

const Mapper = Vue.extend({
  computed: clientDetailMapper.mapGetters({
    ClientDetail: 'Detail',
    ShowClientHeaderData: 'ShowHeaderData',
    Loading: 'Loading',
    Success: 'Success',
    HasError: 'HasError'
  }),
  methods: clientDetailMapper.mapActions({
    updateClientDetail: 'updateDetail',
    getClientDetail: 'getDetail',
    showClientData: 'showData',
    hideClientData: 'hideData'
  })
});
@Component
export default class ClientDetailMixin extends Mapper {
  public onSelectClient (data: CounterpartModel) {
    // @ts-ignore
    void this.updateClientDetail(data);
  }

  public async loadDetail (solarId: number) : Promise<void> {
    if (this.ClientDetail?.solarId !== solarId) {
      await this.getClientDetail({ solarId: solarId });
    }
  }
}
