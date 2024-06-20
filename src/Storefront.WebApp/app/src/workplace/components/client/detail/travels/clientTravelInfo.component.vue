<template>
  <block-spinner-component v-if="showLoading" :loaded="showLoading" />
  <no-data-component v-else-if="showNoData" />
  <error-data-component v-else-if="showError" />
  <div v-else class="q-pa-md">
    <div class="row items-center">
      <q-icon name="arrow_back" style="font-size: 24px; cursor: pointer;" class="q-mr-md" @click="clearFilter" />
      <div class="text-h6">{{ $t('components.client.detail.travels.clientTravelInfo.title') }} {{ getRowNum }}</div>
      <q-space />
      <q-btn :label="$t('components.client.detail.travels.clientTravelList.createNewTravel')" class="float-right col-auto q-px-sm" dense color="primary" no-caps @click="newTravelWizard" />
    </div>
    <div class="q-mt-md">
      <div class="row tbl text-body2">
          <div class="col-4 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelInfo.status') }}</div>
          <div class="col-8 q-mb-sm">
            <span :class="this.isStatusActive ? 'green' : ''"> {{ getTravelStatus(detailData.status) }}</span>
          </div>

          <div class="col-4 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelInfo.countries')}}</div>
          <div class="col-8 q-mb-sm">
            <span v-for="(country, index) in detailData.countries" v-bind:key="index">
              {{ countryWithComma(index) }}
            </span>
          </div>

          <div class="col-4 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelInfo.period')}}</div>
          <div class="col-8 q-mb-sm hide">{{ detailData.startTravel | FormatDateFilter(formatting.dateVue) }} - {{ detailData.endTravel | FormatDateFilter(formatting.dateVue) }}</div>

          <div class="col-4 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelInfo.contactPhone')}}</div>
          <div class="col-8 q-mb-sm">{{ detailData.contactPhone }}</div>

          <div class="col-4 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelInfo.cardMasks')}}</div>
          <div class="col-8 q-mb-sm">
            <span class="row" v-for="(card, index) in detailData.cards" v-bind:key="index">
              {{ cardWithComma(index) }}
            </span>
          </div>
      </div>
      <div class="q-px-lg q-pb-lg q-pt-md q-mt-lg rounded-borders limits-wrapper">
        <template v-if="showLimits">
          <div class="text-subtitle1 text-weight-medium q-mb-sm">{{ $t('components.client.detail.travels.clientTravelInfo.setRiskyLimit')}}</div>
          <div class="row tbl text-body2">
            <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.limits.cardLimits.cashWithdrawalLimit') }}</div>
            <div class="col-5 q-mb-sm">
                {{ detailData.cashWithdrawalLimit | FormatMoneyFilter }}
                <small class="currency q-ml-xs">{{ 'UAH' }}</small>
            </div>

            <div class="col-7 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelInfo.p2pLimit')}}</div>
            <div class="col-5 q-mb-sm">
                {{ detailData.limitCardTransfers | FormatMoneyFilter }}
                <small class="currency q-ml-xs">{{ 'UAH' }}</small>
            </div>
          </div>
        </template>
        <template v-else>
          <div class="text-subtitle1 text-weight-medium q-mb-sm">{{$t('components.client.detail.travels.clientTravelInfo.notChangeLimits')}}</div>
        </template>
      </div>
    </div>
    <client-travel-process-component :isShow="isShowDialog" @hide="hideDialog"/>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import RouteHelper from '../../../../mixins/routing/router.mixin';

import { TravelInterface } from '../../../../interfaces/travel';
import { TravelStatusEnum } from '../../../../enums/travel/travelStatus.enum';
import constants from '../../../../common/constants';
import ClientTravelProcessComponent from './clientTravelProcess.component.vue';

import { FormatMoneyFilter, FormatDateFilter, FormatCardNumberFilter } from '../../../../filters';
import { TravelStatusEnumHelper } from '../../../../helpers/enumHelpers/travelStatusEnum.helper';

import BaseDetailComponent from '../../../base/baseDetail.component.vue';
import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import { mixins } from 'vue-class-component';

@Component({
  components: {
    BaseDetailComponent,
    BlockSpinnerComponent,
    NoDataComponent,
    ErrorDataComponent,
    ClientTravelProcessComponent
  },
  filters: { FormatMoneyFilter, FormatDateFilter, FormatCardNumberFilter }
})
export default class ClientTravelInfoComponent extends mixins(BaseDetailComponent, RouteHelper) {
  @Prop() declare detailData: TravelInterface;
  private formatting: any;
  private isShowDialog: boolean;

  constructor () {
    super();
    this.formatting = constants.formatting;
    this.isShowDialog = false;
  }

  private get getRowNum () {
    const rowNum = this.detailData?.rowNum?.toString() ?? '';
    return parseInt(rowNum) > 0 ? rowNum.padStart(5, '0') : this.detailData?.travelId.toString();
  }

  private getTravelStatus (value: TravelStatusEnum) {
    return TravelStatusEnumHelper.getStatusName(value);
  }

  private get isStatusActive () {
    return this.detailData.status === TravelStatusEnum.Active;
  }

  countryWithComma (index: number) {
    if (this.detailData?.countries) {
      if (index !== this.detailData.countries.length - 1) {
        return `${this.detailData.countries[index].countryName},`;
      } else {
        return this.detailData.countries[index].countryName;
      }
    } else {
      return '';
    }
  }

  cardWithComma (index: number) {
    if (this.detailData?.cards) {
      if (index !== this.detailData.cards.length - 1) {
        return `${FormatCardNumberFilter(this.detailData.cards[index].number)},`;
      } else {
        return FormatCardNumberFilter(this.detailData.cards[index].number);
      }
    } else {
      return '';
    }
  }

  private get showLimits () {
    if (this.detailData) {
      if ((this.detailData.cashWithdrawalLimit && this.detailData.cashWithdrawalLimit > 0) ||
      (this.detailData.limitCardTransfers && this.detailData.limitCardTransfers > 0)) {
        return true;
      }
    }
    return false;
  }

  async clearFilter () {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id), {
      [constants.navigateQuery.travel.pageIndex]: this.getPageIndex(constants.navigateQuery.travel.pageIndex).toString()
    });
  }

  newTravelWizard () {
    this.isShowDialog = !this.isShowDialog;
  }

  hideDialog () {
    this.isShowDialog = false;
  }
}
</script>

<style scoped>
.tbl {
  font-size: 14px;
}
.tbl > div:nth-child(2n+1),
.currency {
  color: #7D7D80
}
.limits-wrapper {
  width: 400px;
  background: #F8F9F9;
}
.green {
  color: #107C10;
}
</style>
