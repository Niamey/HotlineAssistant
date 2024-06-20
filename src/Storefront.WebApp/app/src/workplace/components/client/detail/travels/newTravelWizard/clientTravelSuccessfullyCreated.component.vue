<template>
  <div>
    <div class="row green flex-start">
      <q-icon class="col-auto q-mr-xs" name="done" size="20px" />
      <div class="q-pb-md">{{$t('components.client.detail.travels.clientTravelSuccessfullyCreated.successfullyCreated')}}</div>
    </div>
    <div class="q-py-md" v-if="this.travelInfo.isChangeLimit">{{$t('components.client.detail.travels.clientTravelSuccessfullyCreated.limitsWillBeChange')}}</div>
    <div class="q-py-md">
      <div class="row tbl text-body2">

        <div class="col-5 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelSuccessfullyCreated.countries')}}</div>
        <div class="col-7 q-mb-sm hide"><span v-for="(country, index) in dynamicData.travel.countries" v-bind:key="index">{{ countryWithComma(index) }}</span></div>

        <div class="col-5 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelSuccessfullyCreated.period')}}</div>
        <div class="col-7 q-mb-sm">{{ getPeriod }}</div>

        <div class="col-5 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelSuccessfullyCreated.contactPhone')}}</div>
        <div class="col-7 q-mb-sm">{{ dynamicData.travel.contactPhone }}</div>

        <div class="col-5 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelSuccessfullyCreated.cardMask')}}</div>
        <div class="col-7 q-mb-sm">
          <span class="row" v-for="(card, index) in dynamicData.travel.cards" v-bind:key="index">
              {{ cardWithComma(index) }}
          </span>
        </div>
      </div>
      <div class="q-pa-md rounded-borders limits-wrapper">
        <template v-if="this.travelInfo.isChangeLimit">
          <div class="text-subtitle1 text-weight-medium q-mb-sm">{{$t('components.client.detail.travels.clientTravelSuccessfullyCreated.setLimitInRiskyCountry')}}</div>
          <div class="row tbl text-body2">
            <div class="col-7 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelSuccessfullyCreated.cashWithdrawallLimit') }}</div>
            <div class="col-5 q-mb-sm">
                {{ dynamicData.travel.cashWithdrawalLimit | FormatMoneyFilter }}
                <small class="currency q-ml-xs">{{ 'UAH' }}</small>
            </div>
            <div class="col-7 q-mb-sm">{{ $t('components.client.detail.travels.clientTravelSuccessfullyCreated.p2pLimit')}}</div>
            <div class="col-5 q-mb-sm">
                {{ dynamicData.travel.limitCardTransfers | FormatMoneyFilter }}
                <small class="currency q-ml-xs">{{ 'UAH' }}</small>
            </div>
          </div>
        </template>
        <template v-else>
          <div class="text-subtitle1 text-weight-medium q-mb-sm">{{$t('components.client.detail.travels.clientTravelSuccessfullyCreated.notChangeLimits')}}</div>
        </template>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from '../../../../../interfaces/travel/baseStep.interface';
import { FormatMoneyFilter, FormatCardNumberFilter, FormatDateFilter } from '../../../../../filters';
import constants from '../../../../../common/constants';

@Component({
  filters: { FormatMoneyFilter, FormatCardNumberFilter, FormatDateFilter }
})
export default class ClientTravelSuccessfullyCreatedComponent extends BaseStepComponent implements BaseStepInterface {
  countryWithComma (index: number) {
    if (this.dynamicData?.travel?.countries) {
      if (index !== this.dynamicData.travel.countries.length - 1) {
        return `${this.dynamicData.travel.countries[index].countryName}, `;
      } else {
        return this.dynamicData.travel.countries[index].countryName;
      }
    } else {
      return '';
    }
  }

  cardWithComma (index: number) {
    if (this.dynamicData?.travel?.cards) {
      if (index !== this.dynamicData.travel.cards.length - 1) {
        return `${FormatCardNumberFilter(this.dynamicData.travel.cards[index].number)},`;
      } else {
        return FormatCardNumberFilter(this.dynamicData.travel.cards[index].number);
      }
    } else {
      return '';
    }
  }

  private get getPeriod () {
    if (this.dynamicData.travel.startTravel === this.dynamicData.travel.endTravel) {
      return FormatDateFilter(this.dynamicData.travel.startTravel, constants.formatting.dateVue);
    } else {
      return `${FormatDateFilter(this.dynamicData.travel.startTravel, constants.formatting.dateVue)} - ${FormatDateFilter(this.dynamicData.travel.endTravel, constants.formatting.dateVue)}`;
    }
  }

  validateStep (): string|string[]|undefined {
    // throw new Error('Method not implemented.');
    return undefined;
  }

  collectData () : void {
    // throw new Error('Method not implemented.');
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
  background: #F8F9F9;
}
.green {
  color: #107C10;
}
</style>
