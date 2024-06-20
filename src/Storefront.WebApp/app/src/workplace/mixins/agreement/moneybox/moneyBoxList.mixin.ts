import { Vue } from 'vue-property-decorator';
import Component from 'vue-class-component';
import { moneyBoxListMapper } from '../../../store/modules/moneyBoxList.module';
import MoneyBoxListRequestModel from '../../../models/requests/agreement/moneyBoxListRequest.model';

const Mapper = Vue.extend({
  computed: moneyBoxListMapper.mapGetters({
    MoneyBoxList: 'MoneyBoxes',
    MoneyBoxListLoading: 'Loading',
    MoneyBoxListException: 'Exception',
    MoneyBoxListHasError: 'HasError',
    MoneyBoxListSuccess: 'Success',
    MoneyBoxListValidationError: 'ValidationError'
  }),
  methods: moneyBoxListMapper.mapActions({
    getMoneyBoxList: 'getMoneyBoxList'
  })
});

@Component
export default class MoneyBoxListMixin extends Mapper {
  startLoadingMoneyBoxes!: () => any;
  public async getMoneyBoxesList(agreementId: number): Promise<void> {
    const searchParams = new MoneyBoxListRequestModel();
    searchParams.agreementId = agreementId;
    await this.getMoneyBoxList(searchParams);
  }
}
