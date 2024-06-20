<template>
  <q-card class="q-mb-md">
        <client-agreement-tabs-component v-if="detailShow"
                                         :defaul-tab-data="this.AgreementDetail"
                                         :defaul-tab-has-error="this.AgreementDetailHasError"
                                         :defaul-tab-is-loading="this.AgreementDetailLoading"
        />
        <client-agreement-list-component v-else-if="!detailShow"
                                         :list-data="this.Agreements"
                                         :page-size="10"
                                         :has-error="this.AgreementsHasError"
                                         :is-loading="this.AgreementsLoading"
        />
  </q-card>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';

import ClientAgreementListComponent from './clientAgreementList.component.vue';
import ClientAgreementTabsComponent from './clientAgreementTabs.component.vue';

import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import AgreementDetailMixin from '../../../../mixins/agreement/detail/agreementDetail.mixin';
import AgreementListMixin from '../../../../mixins/agreement/list/agreementList.mixin';
import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';

@Component({
  components: {
    ClientAgreementListComponent,
    ClientAgreementTabsComponent,
    BlockSpinnerComponent,
    NoDataComponent,
    ErrorDataComponent
  }
})
export default class AgreementDetailTableAggregatorComponent extends mixins(AgreementListMixin, AgreementDetailMixin) {
  created () {
    // eslint-disable-next-line @typescript-eslint/unbound-method
    this.$bus.on('agreement-aggregator-detail-update', this.onAgreementDetailUpdate);

    // eslint-disable-next-line @typescript-eslint/unbound-method
    this.$bus.on('agreement-aggregator-list-update', () => {
      void (async () => {
        await this.onAgreementListUpdate();
      })();
    });
  }

  beforeDestroy () {
    this.$bus.off('agreement-aggregator-detail-update');
    this.$bus.off('agreement-aggregator-list-update');
  }

  get detailShow () {
    return NavigateQueryHelper.agreements.hasAgreementId(this.$route) || this.AgreementDetail !== undefined;
  }
}
</script>

<style scoped>

</style>
