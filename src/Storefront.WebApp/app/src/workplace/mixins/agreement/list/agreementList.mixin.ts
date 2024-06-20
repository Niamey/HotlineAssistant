import { Component, Vue } from 'vue-property-decorator';
import { agreementListMapper } from '../../../store/modules/agreementList.module';
import { Route } from 'vue-router';
import { AgreementFilterInterface } from '../../../interfaces/agreement/agreementFilter.interface';
import { NavigateQueryHelper } from '../../../helpers/navigateQuery.helper';
import constants from '../../../common/constants';

const Mapper = Vue.extend({
  computed: agreementListMapper.mapGetters({
    Agreements: 'List',
    AgreementsLoading: 'Loading',
    AgreementsException: 'Exception',
    AgreementsSuccess: 'Success',
    AgreementsHasError: 'HasError',
    HasMoreAgreement: 'HasMoreAgreement'
  }),
  methods: agreementListMapper.mapActions({
    getAgreements: 'getList',
    startLoadingAgreements: 'startLoading',
    stopLoadingAgreements: 'stopLoading',
    applyFilterAgreements: 'applyFilter'
  })
});

@Component
export default class AgreementListMixin extends Mapper {
  startLoadingAgreements!: () => any;
  stopLoadingAgreements!: () => any;
  applyFilterAgreements!: (filter: AgreementFilterInterface) => any;

  beforeRouteUpdate (to: Route, from: Route, next:any) {
    if (NavigateQueryHelper.agreements.getAgreementId(to) !== NavigateQueryHelper.agreements.getAgreementId(from)) {
      this.$bus.emit('agreement-aggregator-detail-update', undefined);

      if (this.Agreements?.length === 0) {
        this.$bus.emit('agreement-aggregator-list-update');
      }
      this.filteringAgreementList(to);
    } else {
      if (NavigateQueryHelper.agreements.getIsInactive(to) !== NavigateQueryHelper.agreements.getIsInactive(from)) {
        this.filteringAgreementList(to);
      }
    }
    next();
  }

  public async onAgreementListUpdate () {
    this.$bus.emit('agreement-aggregator-detail-update', undefined);
    await this.getAgreementList(parseInt(this.$route.params.id));
  }

  public async getAgreementList (solarId: number): Promise<void> {
    await this.getAgreements({ solarId: solarId });
    this.filteringAgreementList(this.$route);
  }

  private static prepareFilterFromRoute (route: Route) {
    return {
      agreementId: NavigateQueryHelper.agreements.getAgreementId(route),
      isDebt: NavigateQueryHelper.agreements.getIsDebt(route),
      isInactive: NavigateQueryHelper.agreements.getIsInactive(route)
    };
  }

  private filteringAgreementList (route: Route) {
    const searchFilter = AgreementListMixin.prepareFilterFromRoute(route);
    this.applyFilterAgreements(searchFilter);

    if (this.Agreements?.length === 1 || NavigateQueryHelper.agreements.hasAgreementId(route) /* условие с agreement.Id временно так как ключ всегда один */) {
      this.$bus.emit('agreement-aggregator-detail-update', this.Agreements[0]);
      if (!NavigateQueryHelper.agreements.hasAgreementId(route) && NavigateQueryHelper.agreements.hasIsInactive(route)) {
        NavigateQueryHelper.navigate(this, constants.navigateQuery.agreement.agreementId, this.Agreements[0].agreementId);
      }
    }
  }
}
