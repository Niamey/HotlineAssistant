<template>
  <div>
    <div class="text-h6">{{ $t('components.client.detail.agreements.ClientAgreementTariff.label') }}</div>
    <tariff-current-component v-bind:detailData="this.AgreementTariffDetail"
                                          v-bind:hasError="this.AgreementTariffDetailHasError"
                                          v-bind:isLoading="this.AgreementTariffDetailLoading"
                                          v-bind:tariffType="this.tariffType"
                                          v-bind:agreementId="this.detailData.agreementId"/>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import AgreementTariffDetailMixin from '../../../../mixins/agreement/detail/agreementTariffDetail.mixin';
import TariffCurrentComponent from '../../tariffs/tariffCurrent.component.vue';
import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';
import { mixins } from 'vue-class-component';
import { AgreementInterface } from '../../../../interfaces/agreement';

@Component({
  components: { TariffCurrentComponent }
})
export default class ClientAgreementTariffComponent extends mixins(AgreementTariffDetailMixin) {
  @Prop({ required: true }) detailData!: AgreementInterface | undefined;
  private tariffType: string;

  constructor () {
    super();
    this.tariffType = 'agreement';
  }

  async created () {
    await this.loadAgreementTariffDetail(parseInt(this.$route.params.id), this.detailData?.agreementId ?? NavigateQueryHelper.agreements.getAgreementId(this.$route) as number);
  }
}
</script>

<style scoped>
</style>
