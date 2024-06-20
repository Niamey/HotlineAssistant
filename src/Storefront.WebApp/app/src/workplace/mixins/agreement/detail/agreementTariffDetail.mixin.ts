import { Vue, Component } from 'vue-property-decorator';
import { agreementTariffDetailMapper } from '../../../store/modules/agreementTariffDetail.module';

const Mapper = Vue.extend({
  computed: agreementTariffDetailMapper.mapGetters({
    AgreementTariffDetail: 'Detail',
    AgreementTariffDetailLoading: 'Loading',
    AgreementTariffDetailException: 'Exception',
    AgreementTariffDetailSuccess: 'Success',
    AgreementTariffDetailHasError: 'HasError'
  }),
  methods: agreementTariffDetailMapper.mapActions({
    getAgreementTariffDetail: 'getDetail',
    startLoadingAgreementTariffDetail: 'startLoading',
    stopLoadingAgreementTariffDetail: 'stopLoading'
  })
});

@Component
// @ts-ignore
export default class AgreementTariffDetailMixin extends Mapper {
  stopLoadingAgreementTariffDetail!: () => any;
  startLoadingAgreementTariffDetail!: () => any;

  public async loadAgreementTariffDetail (solarId: number, agreementId: number) : Promise<void> {
    await this.getAgreementTariffDetail({ solarId, agreementId });
  }
}
