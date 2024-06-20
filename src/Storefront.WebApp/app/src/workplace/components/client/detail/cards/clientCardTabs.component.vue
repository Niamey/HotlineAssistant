<template>
  <block-spinner-component v-if="defaulTabIsLoading" />
  <no-data-component v-else-if="!defaulTabData" />
  <error-data-component v-else-if="defaulTabHasError" />
  <div v-else class="q-pa-md">
    <div class="row items-center">
      <q-icon name="arrow_back" style="font-size: 24px; cursor: pointer;" class="q-mr-md" @click="clearFilter" />
      <div class="text-h6" v-if="this.defaulTabData">{{ $t('components.client.detail.cards.clientCardTabs.title') }} {{this.defaulTabData.number | formatCardNumber }}</div>
    </div>
    <div class="q-mt-md" v-if="this.tab">
      <q-tabs
        v-if="this.defaulTabData"
        v-model="tab"
        dense
        class=""
        active-color="primary"
        indicator-color="primary"
        align="left"
        no-caps
        inline-label
        outside-arrows
        mobile-arrows
        @input="changeTab"
      >
        <q-tab v-for="(val, key) in this.tabList" :name="key" :key="key" :label="val" />
      </q-tabs>

      <q-separator />

      <q-tab-panels v-model="tab" animated>
        <q-tab-panel name="detail">
          <client-card-detail-component :detailData="this.defaulTabData"
                                        :hasError="this.defaulTabHasError"
                                        :isLoading="this.defaulTabIsLoading" />
        </q-tab-panel>

        <q-tab-panel name="stop-list">
          <component :is="stopListComponent" v-if="this.stopListComponent && this.defaulTabData" :cardId="this.defaulTabData.cardId" />
        </q-tab-panel>

        <q-tab-panel name="limitation">
          <card-limits-component v-if="this.defaulTabData" :detailData="this.defaulTabData"/>
        </q-tab-panel>

        <q-tab-panel name="tokens">
          <client-card-token-list-component v-if="this.defaulTabData" :cardId="this.defaulTabData.cardId" />
        </q-tab-panel>

        <q-tab-panel name="services">
          <client-card-services-component v-if="this.defaulTabData" :cardId="this.defaulTabData.cardId" />
        </q-tab-panel>

        <q-tab-panel name="balance">
          <card-full-balance-component v-if="this.defaulTabData" :cardId="this.defaulTabData.cardId" />
        </q-tab-panel>

        <q-tab-panel name="replenishment">
          <div class="text-h6">{{ $t('components.client.detail.cards.clientCardTabs.replenishment') }}</div>
          <q-banner class="bg-primary text-white">
            {{ $t('components.temporary.notImplement.label') }}
          </q-banner>
        </q-tab-panel>

        <q-tab-panel name="debt">
          <client-card-debt-to-bank-component v-if="this.defaulTabData" :cardId="this.defaulTabData.cardId" />
        </q-tab-panel>
      </q-tab-panels>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';
import RouteHelper from '../../../../mixins/routing/router.mixin';

import ClientCardDetailComponent from '../cards/clientCardDetail.component.vue';
import ClientCardServicesComponent from '../cards/clientCardServices.component.vue';
import ClientCardTokenListComponent from '../cards/tokens/clientCardTokenList.component.vue';
import ClientCardDebtToBankComponent from '../cards/clientCardDebtToBank.component.vue';
import CardFullBalanceComponent from '../cards/cardFullBalance.component.vue';
import CardCannotUnlockedComponent from './stopList/cardCannotUnlocked.component.vue';
import CardMightUnlockedComponent from './stopList/cardMightUnlocked.component.vue';
import CardLimitsComponent from '../cards/limits/cardLimits.component.vue';
import CardStopListComponent from '../cards/stopList/cardStopList.component.vue';
import { CardStatusEnum } from '../../../../enums/card/cardStatus.enum';
import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';

import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';

import { FormatCardNumberFilter } from '../../../../filters';
import constants from '../../../../common/constants';

@Component({
  components: {
    ClientCardDetailComponent,
    ClientCardServicesComponent,
    ClientCardTokenListComponent,
    CardFullBalanceComponent,
    CardCannotUnlockedComponent,
    CardMightUnlockedComponent,
    CardLimitsComponent,
    CardStopListComponent,
    BlockSpinnerComponent,
    NoDataComponent,
    ErrorDataComponent,
    ClientCardDebtToBankComponent
  },
  filters: { FormatCardNumberFilter }
})
export default class ClientCardTabsComponent extends RouteHelper {
  @Prop({ required: true }) defaulTabData!: any;
  @Prop({ required: true }) defaulTabIsLoading!: boolean;
  @Prop({ required: true }) defaulTabHasError!: boolean;
  private stopListComponent: any;
  private tab: string;
  private tabList: any;

  constructor () {
    super();
    this.stopListComponent = this.defaulTabData ? this.getStopListComponent : null;
    this.tabList = {
      detail: this.$t('components.client.detail.cards.clientCardTabs.detail').toString(),
      // 'stop-list': this.$t('components.client.detail.cards.clientCardTabs.stopList').toString(),
      limitation: this.$t('components.client.detail.cards.clientCardTabs.limitation').toString(),
      tokens: this.$t('components.client.detail.cards.clientCardTabs.tokens').toString(),
      // services: this.$t('components.client.detail.cards.clientCardTabs.services').toString(),
      balance: this.$t('components.client.detail.cards.clientCardTabs.balance').toString(),
      // replenishment: this.$t('components.client.detail.cards.clientCardTabs.replenishment').toString(),
      debt: this.$t('components.client.detail.cards.clientCardTabs.debt').toString()
    };

    this.tab = this.getTabName;
  }

  get getTabName () {
    if (NavigateQueryHelper.cards.hasTabName(this.$route)) {
      const tab = NavigateQueryHelper.cards.getTabName(this.$route) as string;
      return (tab in this.tabList) ? tab : 'detail';
    } else {
      return 'detail';
    }
  }

  @Watch('getTabName', { immediate: true })
  private onChangeTabName (newTab: string) {
    if (newTab && newTab in this.tabList) {
      this.tab = newTab;
    }
  }

  private async changeTab (value:string) {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id),
      {
        [constants.navigateQuery.card.cardId]: NavigateQueryHelper.cards.getCardId(this.$route)?.toString(),
        [constants.navigateQuery.account.accountId]: NavigateQueryHelper.accounts.getAccountId(this.$route)?.toString(),
        [constants.navigateQuery.agreement.agreementId]: NavigateQueryHelper.agreements.getAgreementId(this.$route)?.toString(),
        [constants.navigateQuery.agreement.tabName]: NavigateQueryHelper.agreements.getTabName(this.$route)?.toString(),
        [constants.navigateQuery.card.tabName]: value
      });
  }

  get getStopListComponent () {
    if (this.defaulTabData) {
      switch (this.defaulTabData.status as CardStatusEnum) {
        case CardStatusEnum.Undefined:
        case CardStatusEnum.Stolen:
        case CardStatusEnum.Lost:
        case CardStatusEnum.Canceled:
        case CardStatusEnum.Blocked:
          return CardCannotUnlockedComponent;
        case CardStatusEnum.Suspend:
          return CardMightUnlockedComponent;
        case CardStatusEnum.Active:
          return CardStopListComponent;
        default:
          return CardCannotUnlockedComponent;
      }
    } else return null;
  }

  @Watch('defaulTabIsLoading')
  private IsLoading (value: boolean) {
    if (!value && !this.defaulTabHasError) {
      this.stopListComponent = this.getStopListComponent;
    }
  }

  created () {
    this.stopListComponent = this.getStopListComponent;
  }

  async clearFilter () {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id), {});
  }
}

</script>

<style scoped>

</style>
