import { Vue, Component } from 'vue-property-decorator';
import { agreementTariffListMapper } from '../../../store/modules/agreementTariffList.module';

const Mapper = Vue.extend({
  computed: agreementTariffListMapper.mapGetters({
    AgreementTariffs: 'List',
    AgreementTariffsLoading: 'Loading',
    AgreementTariffsException: 'Exception',
    AgreementTariffsSuccess: 'Success',
    AgreementTariffsHasError: 'HasError'
  }),
  methods: agreementTariffListMapper.mapActions({
    getAgreementTariffs: 'getList',
    startLoadingAgreementTariffs: 'startLoading',
    stopLoadingAgreementTariffs: 'stopLoading'
  })
});

@Component
// @ts-ignore
export default class AgreementTariffListMixin extends Mapper {
  startLoadingAgreementTariffs!: () => any;
  stopLoadingAgreementTariffs!: () => any;

  public async getAgreementTariffList (solarId: number, agreementId: number): Promise<void> {
    await this.getAgreementTariffs({ solarId, agreementId });
  }
}
