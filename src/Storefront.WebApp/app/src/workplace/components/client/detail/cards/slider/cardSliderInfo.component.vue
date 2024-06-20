<template>
  <q-card class="card no-box-shadow" v-if="currentCard">
      <q-card-section v-if="showHeader">
        <div  class="card-title cursor-pointer" @click="navigateToAgr">{{ $t('components.client.detail.cards.slider.cardSliderInfo.agreement') }} № {{ currentCard.agreementNumber }}</div>
      </q-card-section>
      <q-card-section class="q-pt-none row justify-center" :class="{'q-mt-md': !showHeader}">
          <div class="col-2" v-if="slideData.cardSlots.length > 1">
            <!-- vertical carousel -->
            <card-slider-slot-component :slotData="slideData" :cardCode="currentCard.cardCode" :currentCard="currentCard" @changeCard="onChangeCard" />
            <!-- end vertical carousel -->
          </div>
          <div class="col img-container" :class="{ 'flex flex-center': slideData.cardSlots.length == 1 }">
            <card-image-view-component spinner-color="white" class="card-img cursor-pointer" @navigate="navigate"
                                                                          v-bind:detailData="this.currentCard"
                                                                          v-bind:hasError="this.hasError"
                                                                          v-bind:isLoading="this.isLoading"/>
          </div>
      </q-card-section>
      <q-card-section class="q-pt-none">
        <div class="card-info cursor-pointer" @click="navigate">{{ $t('components.client.detail.cards.clientCardList.cardInfo') }}</div>
      </q-card-section>
      <q-card-section class="q-pt-none q-pb-sm">
        <div class="row tbl q-gutter-y-sm">
          <div class="col-5">{{ $t('components.client.detail.cards.clientCardList.balance') }}:</div>
          <div class="col-7">
            <card-balance-component v-if="currentCard.cardId" :cardId="currentCard.cardId" sizeIcon="20px"/>
          </div>
          <div class="col-5">Push-info:</div>
          <div class="col-7">{{ getPushInfoStatus(currentCard.pushInfo) }}</div>
          <div class="col-5">3D-Secure:</div>
          <div class="col-7 row items-center">
            <template v-if="currentCard.securePhone !== null">
              {{ currentCard.securePhone }}
               <q-badge :class="[this.currentCard.financePhone == this.currentCard.securePhone ? 'badge-positive' : 'badge-negative']" class="q-ml-sm text-body2 text-weight-medium ">ФН</q-badge>
            </template>
            <span v-else>{{ $t('components.client.detail.cards.clientCardList.notActive') }}</span>
          </div>
          <div v-if="false">
            <div class="col-5">{{ $t('components.client.detail.cards.clientCardList.restrictions') }}:</div>
            <div class="col-7"><span  :class="[currentCard.inStopList ? 'text-link cursor-pointer': 'inactiveLink']" @click="navigateToStopList">{{ getLimit(currentCard.inStopList) }}</span></div>
          </div>
          <div class="col-5">{{ $t('components.client.detail.cards.clientCardList.status') }}:</div>
          <div class="col-7">
            <card-status-component v-if="currentCard.cardId" :cardId="currentCard.cardId" :status="currentCard.status" :currentCard="currentCard" />
          </div>
        </div>
      </q-card-section>
    </q-card>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';
import CardSliderSlotComponent from './cardSliderSlot.component.vue';
import { CardSliderModel } from '../../../../../models/card/slider';
import { FormatDateFilter, FormatCardNumberFilter } from '../../../../../filters';
import RouteHelper from '../../../../../mixins/routing/router.mixin';
import CardBalanceComponent from '../../../../shared/cardBalance.component.vue';
import { NavigateQueryHelper } from '../../../../../helpers/navigateQuery.helper';
import constants from '../../../../../common/constants';
import CardStatusComponent from '../audit/cardStatus.component.vue';
import { ClientCardTabsEnum } from '../../../../../enums/client/clientCardTabs.enum';
import { CardPushInfoStatusEnum } from '../../../../../enums/card';
import { CardPushInfoStatusEnumHelper } from '../../../../../helpers/enumHelpers/cardPushInfoStatusEnum.helper';
import CardImageViewComponent from '../../../../../components/client/detail/cards/cardImageView.component.vue';
import BaseDetailComponent from '../../../../base/baseDetail.component.vue';
import { mixins } from 'vue-class-component';

@Component({
  components: { CardSliderSlotComponent, CardBalanceComponent, CardStatusComponent, CardImageViewComponent, BaseDetailComponent },
  filters: { FormatDateFilter, FormatCardNumberFilter }
})
export default class CardSliderInfoComponent extends mixins(RouteHelper, BaseDetailComponent) {
  @Prop({ required: true }) slideData!: CardSliderModel;
  private currentCard:any = {};
  private formatting:any;

  constructor () {
    super();
    this.formatting = constants.formatting;
  }

  @Watch('slideData', {
    immediate: true
  })
  slideDataChanged (newVal: CardSliderModel) {
    if (newVal) {
      this.currentCard = newVal.getCardDetail(newVal.mainCardId);
    }
  }

  get showHeader () {
    return !NavigateQueryHelper.agreements.hasAgreementId(this.$route);
  }

  async navigateToStopList () {
    await this.navigateBySolarId('clientDetail', this.currentCard.solarId as number,
      {
        [constants.navigateQuery.card.cardId]: this.currentCard.cardId.toString(),
        [constants.navigateQuery.account.accountId]: this.currentCard.accountId?.toString(),
        [constants.navigateQuery.agreement.agreementId]: this.currentCard.agreementId?.toString(),
        [constants.navigateQuery.card.tabName]: ClientCardTabsEnum.StopList
      });
  }

  getLimit (val:boolean) {
    return val ? this.$t('components.client.detail.cards.slider.cardSliderInfo.limit.yes').toString()
      : this.$t('components.client.detail.cards.slider.cardSliderInfo.limit.no').toString();
  }

  getPushInfoStatus (value: CardPushInfoStatusEnum) {
    return CardPushInfoStatusEnumHelper.getPushInfoStatusName(value);
  }

  onChangeCard (cardId:number) {
    this.currentCard = this.slideData.getCardDetail(cardId);
  }

  async navigate () {
    await this.navigateBySolarId('clientDetail', this.currentCard.solarId as number,
      {
        [constants.navigateQuery.card.cardId]: this.currentCard.cardId.toString(),
        [constants.navigateQuery.account.accountId]: this.currentCard.accountId?.toString(),
        [constants.navigateQuery.agreement.agreementId]: this.currentCard.agreementId?.toString(),
        [constants.navigateQuery.card.tabName]: ClientCardTabsEnum.Detail
      });
  }

  async navigateToAgr () {
    await this.navigateBySolarId('clientDetail', this.currentCard.solarId as number,
      {
        [constants.navigateQuery.agreement.agreementId]: this.currentCard.agreementId.toString()
      });
  }
}
</script>

<style scoped>
  .inactiveLink {
    pointer-events: none;
    cursor: default;
  }
  .img-container {
    padding-left: 28px;
  }

  .flex.img-container {
    padding: 0;
  }

  .card-img {
    width: 200px !important;
    /*margin-left: 28px;*/
  }
  .card-num { font-size: 12px; line-height: 20px; text-shadow: 1px 1px 2px black; }
  .card-name, .card-exp { font-size: 10px; line-height: 18px; text-shadow: 1px 1px 2px black; }

  @media (min-width: 1920px) {
    .card-img {
      width: 232px !important;
    }
    .card-num { font-size: 16px; line-height: 28px; }
    .card-name, .card-exp { font-size: 12px; line-height: 20px; }
  }
  .card-img .caption {
    background:rgba(0, 0, 0, 0.15);
    backdrop-filter: blur(7px);
    border-radius: 0 0 8px 8px;
    padding: 10px;
  }

  .card { min-width: 320px; }
  @media (min-width: 1366px) {
    .card { min-width: 350px; }
  }
  @media (min-width: 1920px) {
    .card { min-width: 352px; }
  }

  .card:hover {
    box-shadow: 0 1px 5px rgba(0, 0, 0, 0.2), 0 2px 2px rgba(0, 0, 0, 0.14), 0 3px 1px -2px rgba(0, 0, 0, 0.12) !important;
  }

  .card-title {
    font-size:16px;
  }
  .card-info {
    font-size:14px;
  }
  @media (min-width: 1920px) {
    .card-title,
    .card-info  {
      font-size:18px;
    }
  }

  .tbl {
    font-size: 14px;
  }
  .q-badge {
    height: 16px;
    padding-top:3px;
  }
  @media (min-width: 1920px) {
    .tbl  {
      font-size:16px;
    }
    .q-badge {
      height: 20px;
      padding-top:4px;
    }
  }

  .badge-positive { background-color: #107C10; }
  .badge-negative { background-color: #D83B01; }

  .tbl > div:nth-child(2n+1) {
    color: #7D7D80
  }

  .text-link {
    color: #0078D4;
  }
</style>
