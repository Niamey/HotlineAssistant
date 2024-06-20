import { Vue } from 'vue-property-decorator';
import Component from 'vue-class-component';
import { menuLeftMapper } from '../../store/modules/menuLeftDetail.module';
import { LocalizationEnum } from '../../enums/localization/localization.enum';
import MenuLeftRequestModel from '../../models/requests/menu/menuLeftRequest.model';

const Mapper = Vue.extend({
  computed: menuLeftMapper.mapGetters({
    SideBarItems: 'List',
    SideBarItemsLoading: 'Loading',
    // AccountListException: 'Exception',
    SideBarItemsHasError: 'HasError',
    SideBarItemsSuccess: 'Success',
    SideBarItemsValidationError: 'ValidationError'
  }),
  methods: menuLeftMapper.mapActions({
    getMenuLeftDetail: 'getList',
    startLoadingMenuLeft: 'startLoading'
  })
});

@Component
export default class MenuLeftMixin extends Mapper {
  public async loadMenuLeftDetail (localization: LocalizationEnum): Promise<void> {
    const searchParams = new MenuLeftRequestModel();
    searchParams.localization = localization;
    await this.getMenuLeftDetail(searchParams);
  }
}
