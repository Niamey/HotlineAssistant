import { Vue } from 'vue-property-decorator';
import Component from 'vue-class-component';
import { serviceListMapper } from '../../store/modules/serviceList.module';
import ServiceListRequestModel from '../../models/requests/service/serviceListRequest.model';

const Mapper = Vue.extend({
  computed: serviceListMapper.mapGetters({
    ServiceList: 'Services',
    ServiceListLoading: 'Loading',
    ServiceListException: 'Exception',
    ServiceListHasError: 'HasError',
    ServiceListSuccess: 'Success',
    ServiceListValidationError: 'ValidationError'
  }),
  methods: serviceListMapper.mapActions({
    getServiceList: 'getServiceList'
  })
});

@Component
export default class ServiceListMixin extends Mapper {
  startLoadingServices!: () => any;
  public async getServicesList (count: number): Promise<void> {
    const searchParams = new ServiceListRequestModel();
    searchParams.count = count;
    await this.getServiceList(searchParams);
  }
}
