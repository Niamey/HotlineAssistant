<template>
  <q-card class="q-mb-md">
        <client-card-tabs-component v-if="detailShow"
                                         :defaul-tab-data="this.CardDetail"
                                         :defaul-tab-has-error="this.CardDetailHasError"
                                         :defaul-tab-is-loading="this.CardDetailLoading" />

        <client-card-list-component v-else-if="!detailShow"
                                          :list-data="this.Cards"
                                          :has-error="this.CardsHasError"
                                          :is-loading="this.CardsLoading"
                                          :agreementNumber="getAgreementNumber" />
  </q-card>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';

import CardListMixin from '../../../../mixins/card/list/cardList.mixin';
import CardDetailMixin from '../../../../mixins/card/detail/cardDetail.mixin';

import ClientCardListComponent from './clientCardList.component.vue';
import ClientCardTabsComponent from './clientCardTabs.component.vue';

import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';

@Component({
  components: { ClientCardListComponent, ClientCardTabsComponent }
})
export default class CardDetailTableAggregatorComponent extends mixins(CardListMixin, CardDetailMixin) {
  created () {
    // eslint-disable-next-line @typescript-eslint/unbound-method
    this.$bus.on('card-aggregator-detail-update', this.onCardDetailUpdate);

    // eslint-disable-next-line @typescript-eslint/unbound-method
    this.$bus.on('card-aggregator-list-update', () => {
      void (async () => {
        await this.onCardListUpdate();
      })();
    });
  }

  beforeDestroy () {
    this.$bus.off('card-aggregator-detail-update');
    this.$bus.off('card-aggregator-list-update');
  }

  get detailShow () {
    return NavigateQueryHelper.cards.hasCardId(this.$route) || this.CardDetail !== undefined;
  }

  get getAgreementNumber () {
    return this.getAgreementNumberById(NavigateQueryHelper.agreements.getAgreementId(this.$route));
  }
}
</script>

<style scoped>

</style>
