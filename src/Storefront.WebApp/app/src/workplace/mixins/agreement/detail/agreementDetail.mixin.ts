import { Component, Vue } from 'vue-property-decorator';
import { agreementDetailMapper } from '../../../store/modules/agreementDetail.module';
import AgreementModel from '../../../models/agreement/agreement.model';

const Mapper = Vue.extend({
  computed: agreementDetailMapper.mapGetters({
    AgreementDetail: 'Detail',
    AgreementDetailLoading: 'Loading',
    AgreementDetailException: 'Exception',
    AgreementDetailSuccess: 'Success',
    AgreementDetailHasError: 'HasError'
  }),
  methods: agreementDetailMapper.mapActions({
    getAgreementDetail: 'getDetail',
    updateDetail: 'updateDetail',
    startLoadingAgreementDetail: 'startLoading',
    stopLoadingAgreementDetail: 'stopLoading'
  })
});

@Component
export default class AgreementDetailMixin extends Mapper {
  stopLoadingAgreementDetail!: () => any;
  startLoadingAgreementDetail!: () => any;

  public onAgreementDetailUpdate (payload : AgreementModel) {
    void this.updateDetail(payload);
  }

  public async loadAgreementDetail (solarId: number, agreementId: number) : Promise<void> {
    if (this.getAgreementDetail === undefined || this.AgreementDetail?.solarId !== solarId || this.AgreementDetail?.agreementId !== agreementId) {
      await this.getAgreementDetail({ solarId, agreementId });
    }
  }
}
