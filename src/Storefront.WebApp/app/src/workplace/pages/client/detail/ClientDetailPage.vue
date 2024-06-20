<template>
  <q-page class="row q-pl-md client-detail">
    <div class="col-8 q-pt-md">
      <q-scroll-area class="fit" :thumb-style="thumbStyle" ref="detailScrollArea" :delay="500">
        <div class="q-pl-xs q-pr-md">
          <client-registration-data-component :detail-data="this.ClientDetail" :has-error="this.HasError" :is-loading="this.Loading" />
          <card-detail-table-aggregator-component />
          <agreement-detail-table-aggregator-component />
          <client-account-list-component :list-data="this.AccountList" :page-size="5" :has-error="this.AccountListHasError" :is-loading="this.AccountListLoading" />
          <travel-detail-table-aggregator-component />
        </div>
        <q-scroll-observer @scroll="scrollHandler" />
      </q-scroll-area>
    </div>
    <div class="col-4 q-pt-xs bg-white">
      <client-activity-component class="full-heigth" />
    </div>
  </q-page>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';

import ClientDetailMixin from '../../../mixins/client/clientDetail.mixin';
import AgreementListMixin from '../../../mixins/agreement/list/agreementList.mixin';
import AgreementDetailMixin from '../../../mixins/agreement/detail/agreementDetail.mixin';
import CardListMixin from '../../../mixins/card/list/cardList.mixin';
import CardDetailMixin from '../../../mixins/card/detail/cardDetail.mixin';
import AccountListMixin from '../../../mixins/account/accountList.mixin';
import TravelListMixin from '../../../mixins/travel/list/travelList.mixin';
import TravelDetailMixin from '../../../mixins/travel/detail/travelDetail.mixin';

import ClientRegistrationDataComponent from '../../../components/client/detail/clientRegistrationData.component.vue';
import ClientAccountListComponent from '../../../components/client/detail/account/clientAccountList.component.vue';
import AgreementDetailTableAggregatorComponent from '../../../components/client/detail/agreements/agreementDetailTableAggregator.component.vue';
import CardDetailTableAggregatorComponent from '../../../components/client/detail/cards/cardDetailTableAggregator.component.vue';
import TravelDetailTableAggregatorComponent from '../../../components/client/detail/travels/travelDetailTableAggregator.component.vue';
import { NavigateQueryHelper } from '../../../helpers/navigateQuery.helper';

import ClientActivityComponent from '../../../components/client/detail/clientActivity/clientActivity.component.vue';
import { QScrollArea } from 'quasar';

@Component({
  components: {
    ClientRegistrationDataComponent,
    CardDetailTableAggregatorComponent,
    AgreementDetailTableAggregatorComponent,
    ClientAccountListComponent,
    ClientActivityComponent,
    TravelDetailTableAggregatorComponent
  }
})
export default class Client extends mixins(ClientDetailMixin,
  AccountListMixin, CardListMixin, CardDetailMixin,
  AgreementListMixin, AgreementDetailMixin, TravelListMixin, TravelDetailMixin) {
  private thumbStyle:any = {};

  constructor () {
    super();

    this.thumbStyle = {
      right: '5px',
      borderRadius: '5px',
      backgroundColor: '#7D7D80',
      width: '5px',
      opacity: 1
    };

    /*
    this.barStyle = {
      right: '2px',
      borderRadius: '9px',
      backgroundColor: '#027be3',
      width: '9px',
      opacity: 0.2
    };
    */
  }

  $refs!: {
    detailScrollArea: QScrollArea;
  }

  created () {
    this.$bus.on('client-contact-data-scroll-to-top', () => {
      this.$refs.detailScrollArea.setScrollPosition(0, 200);
    });

    this.startLoadingCards();
    this.startLoadingCardDetail();
    this.startLoadingAgreements();
    this.startLoadingAgreementDetail();
    this.startLoadingTravels();

    void this.loadDetail(parseInt(this.$route.params.id));

    let card;
    if (NavigateQueryHelper.cards.hasCardId(this.$route)) {
      card = this.loadCardDetail(parseInt(this.$route.params.id), NavigateQueryHelper.cards.getCardId(this.$route) as number);
    } else {
      card = this.getClientCardList(parseInt(this.$route.params.id));
    }

    void card.then(() => {
      this.stopLoadingCards();
      this.stopLoadingCardDetail();
    });

    let agreement;
    if (NavigateQueryHelper.agreements.hasAgreementId(this.$route)) {
      agreement = this.loadAgreementDetail(parseInt(this.$route.params.id), NavigateQueryHelper.agreements.getAgreementId(this.$route) as number);
    } else {
      agreement = this.getAgreementList(parseInt(this.$route.params.id));
    }

    void agreement.then(() => {
      this.stopLoadingAgreements();
      this.stopLoadingAgreementDetail();
    });

    void this.getClientAccountList(parseInt(this.$route.params.id));

    let travel;
    if (NavigateQueryHelper.travels.hasTravelId(this.$route)) {
      travel = this.loadTravelDetail(parseInt(this.$route.params.id), NavigateQueryHelper.travels.getTravelId(this.$route) as number);
    } else {
      travel = this.getTravelList(parseInt(this.$route.params.id));
    }

    void travel.then(() => {
      this.stopLoadingTravels();
      this.stopLoadingTravelDetail();
    });
  }

  private scrollHandler (ev:any) {
    if (ev.position > 50) {
      void this.showClientData();
    } else {
      void this.hideClientData();
    }
  }

  beforeDestroy () {
    this.$bus.off('client-contact-data-scroll-to-top');
  }
}
</script>

<style scoped>
  .client-detail { min-width: 1080px; }
  .border-white { border: 1px solid white !important; }
</style>
