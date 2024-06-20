<template>
    <block-spinner-component v-if="showLoading" :loaded="showLoading" />
    <no-data-component v-else-if="showNoData" />
    <error-data-component v-else-if="showError" />
    <div class="row q-mt-sm" v-else-if="detailData">
      <div class="col-12 q-mb-md" v-if="stopListComponent && getIsCannotUnlockComopnent">
        <card-cannot-unlocked-component :statusData="statusData"  />
      </div>
      <div class="col-3">
        <card-image-view-component  v-bind:detailData="this.detailData"
                                    v-bind:hasError="this.hasError"
                                    v-bind:isLoading="this.isLoading"/>

        <div class="card-action">
          <block-spinner-component size="2em" label="" v-if="isLoadingStatus" :loaded="isLoadingStatus" />
          <component :is="stopListComponent" v-else-if="stopListComponent && !getIsCannotUnlockComopnent" :statusData="statusData" />
        </div>

        <tariff-current-component   v-bind:detailData="this.CardTariffDetail"
                                    v-bind:hasError="this.CardTariffDetailHasError"
                                    v-bind:isLoading="this.CardTariffDetailLoading"
                                    v-bind:tariffType="this.tariffType"
                                    v-bind:cardId="this.detailData.cardId"/>
      </div>
      <div class="col q-ml-xs">
        <div class="detail-wrapper rounded-borders q-px-lg q-pb-lg q-pt-md">
          <div class="text-subtitle1 text-weight-medium q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.label')}}</div>
          <div class="row tbl text-body2">
            <template v-if="detailData.embossedName">
              <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.fullName')}}</div>
              <div class="col-7 q-mb-sm">{{ detailData.embossedName }}</div>
            </template>

            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.status.label') }}</div>
            <div class="col-7 q-mb-sm blue">
              <card-status-component v-if="detailData.cardId" :cardId="detailData.cardId" :status="detailData.status" :currentCard="detailData" />
            </div>

            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.category')}}</div>
            <div class="col-7 q-mb-sm">{{ getCategory }}</div>

            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.type')}}</div>
            <div class="col-7 q-mb-sm hide">{{ detailData.type }}</div>

            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.hasChip.label')}}</div>
            <div class="col-7 q-mb-sm">{{ getChipInfo(detailData.hasChip) }}</div>

            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.barcode')}}</div>
            <div class="col-7 q-mb-sm">{{ detailData.barcode }}</div>

            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.view')}}</div>
            <div class="col-7 q-mb-sm">{{ getView }}</div>
            <!-- <template v-if="singleCard" class="row tbl q-gutter-y-sm" > -->
            <div class="col-5 q-mb-sm">Push-info:</div>
            <div class="col-7 q-mb-sm">{{ getPushInfoStatus(detailData.pushInfo) }}</div>
            <div class="col-5 q-mb-sm">3D-Secure:</div>
            <div class="col-7 q-mb-sm row items-center">
              <template v-if="detailData.securePhone !== null">
                {{ detailData.securePhone }}
                <q-badge :class="[this.detailData.financePhone == this.detailData.securePhone ? 'badge-positive' : 'badge-negative']" class="q-ml-sm text-body2 text-weight-medium ">ФН</q-badge>
              </template>
              <span v-else>{{ $t('components.client.detail.cards.clientCardList.notActive') }}</span>
            </div>
            <div v-if="false">
              <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardList.restrictions') }}:</div>
              <div class="col-7 q-mb-sm"><span  :class="[detailData.inStopList ? 'text-link cursor-pointer': 'inactiveLink']" @click="navigateToStopList">{{ getLimit(detailData.inStopList) }}</span></div>
            </div>
            <!-- </template > -->
          </div>
        </div>
      </div>

      <div class="col q-py-xs">
        <div class="q-pa-sm">
          <card-balance-extended-component  v-if="detailData.cardId" :cardId="detailData.cardId"/>
          <template v-if="false"> <!--v-if="singleCard"-->
            <div class="text-h6 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.registrationPlace')}}</div>
            <div class="row tbl text-body2">
              <template v-if="detailData.branch.name">
                <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.department') }}</div>
                <div class="col-7 q-mb-sm">{{ detailData.branch.name }}</div>
              </template>
              <template v-if="detailData.branch.address">
                <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.address') }}</div>
                <div class="col-7 q-mb-sm">{{ detailData.branch.address }}</div>
              </template>
              <template v-if="detailData.branch.phone">
                <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.phone')}}</div>
                <div class="col-7 q-mb-sm">{{ detailData.branch.phone }}</div>
              </template>
            </div>
          </template>
        </div>
      </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';

import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import { CardInterface } from '../../../../interfaces/card';
import BaseDetailComponent from '../../../base/baseDetail.component.vue';
import { FormatCardNumberFilter, FormatDateFilter, FormatMoneyFilter } from '../../../../filters';
import CardStatusComponent from './audit/cardStatus.component.vue';
import CardTariffDetailMixin from '../../../../mixins/card/detail/cardTariffDetail.mixin';
import CardListMixin from '../../../../mixins/card/list/cardList.mixin';
import CardImageViewComponent from '../../../../components/client/detail/cards/cardImageView.component.vue';
import { CardPushInfoStatusEnum } from '../../../../enums/card';
import { ClientCardTabsEnum } from '../../../../enums/client/clientCardTabs.enum';
import { CardPushInfoStatusEnumHelper } from '../../../../helpers/enumHelpers/cardPushInfoStatusEnum.helper';
import RouteHelper from '../../../../mixins/routing/router.mixin';

import CardBalanceExtendedComponent from '../../../shared/cardBalanceExtended.component.vue';
import TariffCurrentComponent from '../../tariffs/tariffCurrent.component.vue';
import constants from '../../../../common/constants';
import { mixins } from 'vue-class-component';

import { CardCategoryEnumHelper } from '../../../../helpers/enumHelpers/cardCategoryEnum.helper';

import { CardStatusEnum } from '../../../../enums/card/cardStatus.enum';
import CardMightUnlockedComponent from './stopList/cardMightUnlocked.component.vue';
import CardStopListComponent from './stopList/cardStopList.component.vue';
import CardCannotUnlockedComponent from './stopList/cardCannotUnlocked.component.vue';

import { CardCurrentStatusRequestInterface } from '../../../../interfaces/requests/card/status';
import { CardCurrentStatusResponseInterface } from '../../../../interfaces/responses/card/status';
import { CardCurrentStatusModel } from '../../../../models/card/status';
import { Store } from 'vuex';
import { cardApi } from '../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../models/api/apiResult.model';

@Component({
  components: {
    BlockSpinnerComponent,
    NoDataComponent,
    ErrorDataComponent,
    CardBalanceExtendedComponent,
    CardStatusComponent,
    TariffCurrentComponent,
    CardImageViewComponent,
    CardMightUnlockedComponent,
    CardStopListComponent,
    CardCannotUnlockedComponent
  },
  filters: { FormatCardNumberFilter, FormatMoneyFilter, FormatDateFilter }
})
export default class ClientCardDetailComponent extends mixins(CardTariffDetailMixin, BaseDetailComponent, CardListMixin, RouteHelper) {
  @Prop({ required: true }) declare detailData: CardInterface;
  private isBlured: boolean;
  private tariffType: string;
  private formatting: any;
  private statusData: CardCurrentStatusModel | null;
  private isLoadingStatus: boolean;

  private stopListComponent: any;

  constructor () {
    super();
    this.statusData = null;
    this.isLoadingStatus = false;
    this.isBlured = false;
    this.formatting = constants.formatting;
    this.tariffType = 'card';

    this.stopListComponent = null;
  }

  /*
  async created () {
    if (this.detailData) {
      await this.loadCardTariffDetail(parseInt(this.$route.params.id), this.detailData.cardId);
    }
  }
  */

  @Watch('detailData', { immediate: true })
  private async OnDetailDataUpdate (newVal:any) {
    if (newVal) {
      this.stopListComponent = await this.getStopListComponent();
      await this.loadCardTariffDetail(parseInt(this.$route.params.id), newVal.cardId);
    }
  }

  private async getStopListComponent () {
    if (this.detailData) {
      await this.getCardCurrentStatus();

      switch (this.statusData?.status as CardStatusEnum) {
        case CardStatusEnum.Suspend:
          return CardMightUnlockedComponent;
        case CardStatusEnum.Blocked:
          if (this.canUnblockBlockedCard) {
            return CardMightUnlockedComponent;
          } else {
            return CardCannotUnlockedComponent;
          }
        case CardStatusEnum.Active:
          return CardStopListComponent;
        default:
          return CardCannotUnlockedComponent;
      }
    } else return null;
  }

  get getIsCannotUnlockComopnent () {
    return !!(this.stopListComponent === CardCannotUnlockedComponent);
  }

  // get singleCard () {
  //   return this.CardsCount < 2;
  // }

  get getCategory () {
    if (!this.detailData.category) return '';
    return CardCategoryEnumHelper.getCategoryName(this.detailData.category);
  }

  get getView () {
    if (!this.detailData) return '';
    return (this.detailData.isVirtual)
      ? this.$t('components.client.detail.cards.clientCardDetail.viewIsVirtual.virtual').toString()
      : this.$t('components.client.detail.cards.clientCardDetail.viewIsVirtual.plastic').toString();
  }

  async navigateToStopList () {
    await this.navigateBySolarId('clientDetail', this.detailData.solarId as number,
      {
        [constants.navigateQuery.card.cardId]: this.detailData.cardId.toString(),
        [constants.navigateQuery.agreement.agreementId]: this.detailData.agreementId?.toString(),
        [constants.navigateQuery.card.tabName]: ClientCardTabsEnum.StopList
      });
  }

  getPushInfoStatus (value: CardPushInfoStatusEnum) {
    return CardPushInfoStatusEnumHelper.getPushInfoStatusName(value);
  }

  getLimit (val:boolean) {
    return val ? this.$t('components.client.detail.cards.slider.cardSliderInfo.limit.yes').toString()
      : this.$t('components.client.detail.cards.slider.cardSliderInfo.limit.no').toString();
  }

  private getChipInfo (val: boolean) {
    return val ? this.$t('components.client.detail.cards.clientCardDetail.hasChip.yes').toString()
      : this.$t('components.client.detail.cards.clientCardDetail.hasChip.no').toString();
  }

  async getCardCurrentStatus () {
    if (!this.detailData?.cardId) return;

    this.isLoadingStatus = true;

    const result = await this.loadCardCurrentStatus(this.$store, { cardId: this.detailData?.cardId });
    if (result instanceof ApiResultModel && !result.hasError) {
      // @ts-ignore
      this.statusData = result.result?.item;
    }

    this.isLoadingStatus = false;
  }

  private async loadCardCurrentStatus (store: Store<any>, params:CardCurrentStatusRequestInterface) : Promise<void | ApiResultModel<CardCurrentStatusResponseInterface>> {
    const promise = cardApi.getCurrentStatusAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardCurrentStatusResponseInterface>(store, promise);
  }

  get canUnblockBlockedCard () {
    if (this.statusData?.status === CardStatusEnum.Blocked) {
      const comment = this.statusData?.reason?.toLowerCase();
      if (comment) {
        if (comment.includes(constants.blockingReasonText.cardCompromise) ||
            comment.includes(constants.blockingReasonText.checkWithClient)) {
          return true;
        }
      }
    }
    return false;
  }

  showAmount () {
    this.isBlured = false;
  }

  hideAmount () {
    this.isBlured = true;
  }
}
</script>

<style scoped>
  .tbl {
    font-size: 14px;
  }
  .inactiveLink {
    pointer-events: none;
    cursor: default;
  }
  .text-link {
    color: #0078D4;
  }
  .hide {
    overflow: hidden;
    text-overflow: ellipsis;
  }
  .hide:hover {
    overflow: visible;
  }
  .caption {
    /* background: linear-gradient(180deg, rgba(224, 88, 74, 0.8) 0%, rgba(223, 88, 75, 0.8) 6.67%, rgba(218, 88, 77, 0.8) 13.33%, rgba(210, 88, 80, 0.8) 20%, rgba(199, 88, 85, 0.8) 26.67%, rgba(185, 88, 92, 0.8) 33.33%, rgba(168, 88, 99, 0.8) 40%, rgba(149, 88, 108, 0.8) 46.67%, rgba(129, 87, 116, 0.8) 53.33%, rgba(110, 87, 125, 0.8) 60%, rgba(93, 87, 132, 0.8) 66.67%, rgba(79, 87, 139, 0.8) 73.33%, rgba(68, 87, 144, 0.8) 80%, rgba(60, 87, 147, 0.8) 86.67%, rgba(55, 87, 149, 0.8) 93.33%, rgba(54, 87, 150, 0.8) 100%); */
    background:rgba(0, 0, 0, 0.15);
    backdrop-filter: blur(7px);
    border-radius: 0 0 8px 8px;
    padding: 10px;
  }
  .card-num {
    font-size: 12px;
    line-height: 20px;
  }
  .card-name, .card-exp {
    font-size: 10px;
    line-height: 18px;
  }

  .blue {
    color: #0078D4;
  }
  .col.text-subtitle1.blue {
    line-height: 16px;
  }
  .detail-wrapper {
    background-color: #F8F9F9;
  }
  .tbl > div:nth-child(2n+1),
  .gray {
    color: #7D7D80
  }
  .blured {
    filter: blur(3px);
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

  .card-action {
    margin-top: 10px;
    max-width: 241px;
    /*
    padding-left: 12px;
    background-color: #F8F9F9;
    height: 44px;
    */
  }
</style>
