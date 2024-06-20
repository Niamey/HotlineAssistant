<template>
  <q-layout view="hHh lpR lFf" style="background-color: gray;">
    <block-spinner-component v-if="this.isLoading"
                             v-bind:loaded="this.isLoading"
                             v-bind:size="'5em'"
                             v-bind:label="$t('components.shared.blockSpinner.label')"/>
    <!-- <no-data-component v-else-if="showNoData" /> -->
    <error-data-component v-else-if="this.hasError" />
    <client-tariff-detail-component v-else-if="this.tariff" v-bind:detailData="this.tariff"
                                          v-bind:hasError="this.hasError"
                                          v-bind:isLoading="this.isLoading"
                                          v-bind:tariffType="this.tariffType"/>
  </q-layout>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';

import BlockSpinnerComponent from '../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../components/shared/noData.component.vue';
import ErrorDataComponent from '../../../components/shared/errorData.component.vue';

import AgreementTariffDetailMixin from '../../../mixins/agreement/detail/agreementTariffDetail.mixin';
import CardTariffDetailMixin from '../../../mixins/card/detail/cardTariffDetail.mixin';
import { NavigateQueryHelper } from '../../../helpers/navigateQuery.helper';

import ClientTariffDetailComponent from '../../../components/client/tariffs/tariffDetail.component.vue';

@Component({
  components: {
    ClientTariffDetailComponent,
    BlockSpinnerComponent,
    NoDataComponent,
    ErrorDataComponent
  }
})
export default class ClientTariffDetailPage extends mixins(AgreementTariffDetailMixin, CardTariffDetailMixin) {
  private tariffType: string;
  private tariff: any;
  private isLoading: boolean;
  private hasError: boolean;

  constructor () {
    super();
    this.tariffType = 'unknown';
    this.tariff = null;
    this.isLoading = false;
    this.hasError = false;
  }

  async created () {
    const clientId = NavigateQueryHelper.tariffs.getClientId(this.$route) as number;
    this.tariffType = NavigateQueryHelper.tariffs.getTariffType(this.$route) as string;
    if (this.tariffType === 'agreement') {
      await this.loadAgreementTariffDetail(clientId, NavigateQueryHelper.tariffs.getAgreementId(this.$route) as number);
      this.tariff = this.AgreementTariffDetail;
      this.isLoading = this.AgreementTariffDetailLoading;
      this.hasError = this.AgreementTariffDetailHasError;
    } else {
      if (this.tariffType === 'card') {
        await this.loadCardTariffDetail(clientId, NavigateQueryHelper.tariffs.getCardId(this.$route) as number);
        this.tariff = this.CardTariffDetail;
        this.isLoading = this.CardTariffDetailLoading;
        this.hasError = this.CardTariffDetailHasError;
      }
    }
  }
}
</script>

<style scoped>
</style>
