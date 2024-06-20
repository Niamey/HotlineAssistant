<template>
  <div class="q-pa-md">
    <div class="text-h6 row items-center" v-if="defaulTabData" >
        <q-icon v-if="showBack" name="arrow_back" style="font-size: 24px; cursor: pointer;" class="q-mr-md" @click="clearFilter" />
        <div>{{ $t('components.client.detail.agreements.clientAgreementTabs.title') }} <span class="cursor-pointer" @click="filterAgreement(defaulTabData.agreementId)">№ {{defaulTabData.agreementNumber}}</span></div>
    </div>
    <div class="text-h6 row items-center" v-else>
        <q-icon v-if="showBack" name="arrow_back" style="font-size: 24px; cursor: pointer;" class="q-mr-md" @click="clearFilter" />
        <div>{{ $t('components.client.detail.agreements.clientAgreementTabs.title') }} </div>
    </div>
    <div class="q-mt-md">
      <q-tabs
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
        <client-agreement-detail-component :detailData="this.defaulTabData"
                                           :hasError="this.defaulTabHasError"
                                           :isLoading="this.defaulTabIsLoading"/>
      </q-tab-panel>

      <q-tab-panel name="moneyBox">
        <money-box-list-component v-if="this.defaulTabData" :agreementId="this.defaulTabData.agreementId"/>
      </q-tab-panel>

      <q-tab-panel name="statement">
        <div class="text-h6">Виписка</div>
        <q-banner class="bg-primary">К сожалению, раздел недоступен.</q-banner>
        <!-- <client-extract-from-contract-component /> -->
      </q-tab-panel>

      <q-tab-panel name="tariffs">
        <client-agreement-tariff-component :detailData="this.defaulTabData"
                                           :hasError="this.defaulTabHasError"
                                           :isLoading="this.defaulTabIsLoading"/>
      </q-tab-panel>
    </q-tab-panels>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';
import ClientAgreementDetailComponent from '../agreements/clientAgreementDetail.component.vue';
import ClientExtractFromContractComponent from '../agreements/clientExtractFromContract.component.vue';
import ClientAgreementTariffComponent from '../agreements/clientAgreementTariff.component.vue';
import MoneyBoxListComponent from '../agreements/moneyBox/moneyBoxList.component.vue';
import RouteHelper from '../../../../mixins/routing/router.mixin';
import constants from '../../../../common/constants';
import AgreementListMixin from '../../../../mixins/agreement/list/agreementList.mixin';
import { mixins } from 'vue-class-component';
import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';

@Component({
  components: { ClientAgreementDetailComponent, ClientExtractFromContractComponent, ClientAgreementTariffComponent, MoneyBoxListComponent }
})
export default class ClientAgreementTabsComponent extends mixins(RouteHelper, AgreementListMixin) {
  @Prop({ required: true }) defaulTabData!: any;
  @Prop({ required: true }) defaulTabIsLoading!: boolean;
  @Prop({ required: true }) defaulTabHasError!: boolean;
  private tab: string;
  private tabList: any;

  constructor () {
    super();

    this.tabList = {
      detail: this.$t('components.client.detail.agreements.clientAgreementTabs.detail').toString(),
      moneyBox: this.$t('components.client.detail.agreements.clientAgreementTabs.moneyBox').toString()
      // statement: this.$t('components.client.detail.agreements.clientAgreementTabs.statement').toString(),
      // tariffs: this.$t('components.client.detail.agreements.clientAgreementTabs.tariffs').toString()
    };

    this.tab = this.getTabName;
  }

  async clearFilter () {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id), {});
  }

  async filterAgreement (agreementId: number) {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id), {
      [constants.navigateQuery.agreement.agreementId]: agreementId.toString()
    });
  }

  protected get showBack () : boolean {
    return this.HasMoreAgreement;
  }

  get getTabName () {
    if (NavigateQueryHelper.agreements.hasTabName(this.$route)) {
      const tab = NavigateQueryHelper.agreements.getTabName(this.$route) as string;
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
        [constants.navigateQuery.card.tabName]: NavigateQueryHelper.cards.getTabName(this.$route)?.toString(),
        [constants.navigateQuery.agreement.tabName]: value
      });
  }
}

</script>

<style scoped>

</style>
