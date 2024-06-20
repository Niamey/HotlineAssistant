import { Vue } from 'vue-property-decorator';
import { Route } from 'vue-router';
import Component from 'vue-class-component';
import { clientAccountListMapper } from '../../store/modules/accountList.module';
import SearchAccountFilterModel from '../../models/filters/account/searchAccountFilter.model';
import SearchAccountRequestModel from '../../models/requests/account/searchAccountRequest.model';
import { NavigateQueryHelper } from '../../helpers/navigateQuery.helper';

const Mapper = Vue.extend({
  computed: clientAccountListMapper.mapGetters({
    AccountList: 'List',
    AccountListLoading: 'Loading',
    AccountListHasError: 'HasError',
    AccountListSuccess: 'Success',
    AccountListValidationError: 'ValidationError',
    HasMoreAccount: 'HasMoreAccount'
  }),
  methods: clientAccountListMapper.mapActions({
    getAccountList: 'getList',
    startLoadingClientAccounts: 'startLoading',
    filterAccountList: 'filterList'
  })
});

@Component
export default class AccountListMixin extends Mapper {
  startLoadingClientAccounts!: () => any;
  filterAccountList!: (filter: SearchAccountFilterModel) => any;

  beforeRouteUpdate (to:Route, from:Route, next:any) {
    if (NavigateQueryHelper.accounts.getAccountId(to) !== NavigateQueryHelper.accounts.getAccountId(from) ||
        NavigateQueryHelper.agreements.getAgreementId(to) !== NavigateQueryHelper.agreements.getAgreementId(from)) {
      this.filteringClientAccountList(to);
    }

    next();
  }

  public async getClientAccountList (solarId: number): Promise<void> {
    const searchParams = new SearchAccountRequestModel();
    searchParams.solarId = solarId;

    await this.getAccountList(searchParams);
    this.filteringClientAccountList(this.$route);
  }

  private filteringClientAccountList (route:Route) {
    const searchFilter = new SearchAccountFilterModel();
    searchFilter.accountId = NavigateQueryHelper.accounts.getAccountId(route);
    searchFilter.agreementId = NavigateQueryHelper.agreements.getAgreementId(route);

    this.filterAccountList(searchFilter);
  }

  // public logger<T> (data: T): void {
  //   console.log(data);
  // }
}
