<template>
  <div>
    <div class="q-py-md">
      <div><strong v-for="(country, index) in dynamicData.riskyCountries" v-bind:key="index">{{ countryWithComma(index) }}</strong>
        {{ dynamicData.riskyCountries.length > 1 ? $t('components.client.detail.travels.clientTravelChangeLimit.isAriskyCountries') : $t('components.client.detail.travels.clientTravelChangeLimit.isAriskyCountry')}}
      </div>
      <div>{{ dynamicData.riskyCountries.length > 1 ? $t('components.client.detail.travels.clientTravelChangeLimit.limitsInThem') : $t('components.client.detail.travels.clientTravelChangeLimit.limitsInIt')}} {{ $t('components.client.detail.travels.clientTravelChangeLimit.limitsInRiskyCountry')}}</div>

    </div>
    <div class="q-py-md">
      <div>{{$t('components.client.detail.travels.clientTravelChangeLimit.cangeRiskyLimit')}}</div>
      <div class="q-gutter-sm">
            <q-option-group
              class=" row q-ml-none"
              :options="changeLimitOptions"
              type="radio"
              v-model="isChangeLimit"
              @input="changeLimitInput"
            />
      </div>
      <div v-if="isChangeLimit">
          <div>{{$t('components.client.detail.travels.clientTravelChangeLimit.setRiskyLimit')}}</div>
          <div class="row q-gutter-md q-py-md">
            <q-input class="col-3"
              filled
              dense
              stack-label
              :label="$t('components.client.detail.travels.clientTravelChangeLimit.cashWithdrawalLimit')"
              v-model="cashWithdrawalLimit"
              v-money="moneyFormat"
              @input="inputCashWithdrawalLimit"/>
            <q-input class="col-3"
              filled
              dense
              stack-label
              :label="$t('components.client.detail.travels.clientTravelChangeLimit.limitCardTransfers')"
              v-model="limitCardTransfers"
              v-money="moneyFormat"
              @input="inputLimitCardTransfers"/>
          </div>
          <template v-if="this.travelInfo.isClientAbroad">
            <div class="row flex-start q-mb-lg q-pt-md" style="color: grey">
              <q-icon class="col-auto q-mr-xs" name="info" size="20px" />
              <div class="col">{{$t('components.client.detail.travels.clientTravelChangeLimit.redirectCall')}}</div>
            </div>
            <div class="q-py-none">
              <div>{{$t('components.client.detail.travels.clientTravelChangeLimit.hasMaintananceAnswered')}}</div>
              <div class="q-gutter-sm">
                    <q-option-group
                      class=" row q-ml-none"
                      :options="changeHelpOptions"
                      type="radio"
                      v-model="isHelp"
                    />
              </div>
            </div>
          </template>
        </div>
      </div>
  </div>
</template>

<script lang="ts">
// @ts-ignore
import { VMoney } from 'v-money';
import { Component, Prop } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from '../../../../../interfaces/travel/baseStep.interface';
import { travelApi } from '../../../../../api/travel.api';

@Component({
  directives: { money: VMoney }
})
export default class ClientTravelChangeLimitComponent extends BaseStepComponent implements BaseStepInterface {
  @Prop() pageSize?: number;
  private isChangeLimit: boolean;
  private isHelp: boolean | null;
  private changeLimitOptions: Array<any>;
  private changeHelpOptions: Array<any>;
  private moneyFormat: any;
  private cashWithdrawalLimit: number;
  private limitCardTransfers: number;

  constructor () {
    super();
    this.isChangeLimit = false;
    this.isHelp = null;
    this.changeLimitOptions = [
      { label: this.$t('components.client.detail.travels.clientTravelChangeLimit.yes'), value: true },
      { label: this.$t('components.client.detail.travels.clientTravelChangeLimit.no'), value: false }
    ];
    this.changeHelpOptions = [
      { label: this.$t('components.client.detail.travels.clientTravelChangeLimit.yes'), value: true },
      { label: this.$t('components.client.detail.travels.clientTravelChangeLimit.no'), value: false }
    ];
    this.moneyFormat = {
      decimal: '.',
      thousands: ' ',
      suffix: ' UAH',
      precision: 0,
      masked: false
    };

    this.cashWithdrawalLimit = this.dynamicData.travel.cashWithdrawalLimit;
    this.limitCardTransfers = this.dynamicData.travel.limitCardTransfers;
  }

  countryWithComma (index: number) {
    if (this.dynamicData?.riskyCountries) {
      if (index !== this.dynamicData.riskyCountries.length - 1) {
        return `${this.dynamicData.riskyCountries[index].countryName}, `;
      } else {
        return this.dynamicData.riskyCountries[index].countryName;
      }
    } else {
      return '';
    }
  }

  changeLimitInput (value: boolean) {
    if (value) {
      this.cashWithdrawalLimit = 500;
      this.limitCardTransfers = 500;
    } else {
      this.cashWithdrawalLimit = this.limitCardTransfers = 0;
    }
  }

  inputCashWithdrawalLimit (value: any) {
    this.dynamicData.travel.cashWithdrawalLimit = this.clearValue(value);
  }

  inputLimitCardTransfers (value: any) {
    this.dynamicData.travel.limitCardTransfers = this.clearValue(value);
  }

  clearValue (value: string) : number {
    return parseInt(value.replace(/\D+/g, ''));
  }

  validateStep (): string|string[]|undefined {
    const err:string[] = [];
    if (this.isChangeLimit === true) {
      if (this.travelInfo.isClientAbroad === true) {
        if (this.isHelp === null) err.push(this.$t('components.client.detail.travels.clientTravelChangeLimit.errors.requiredIsHelp').toString());
      }
      if (this.dynamicData.travel.cashWithdrawalLimit < 500) err.push(this.$t('components.client.detail.travels.clientTravelChangeLimit.errors.cashWithdrawalLimitLess500UAH').toString());
      if (this.dynamicData.travel.limitCardTransfers < 500) err.push(this.$t('components.client.detail.travels.clientTravelChangeLimit.errors.limitCardTransfersLess500UAH').toString());
    }
    return (err.length > 0) ? err : undefined;
  }

  public async collectData () {
    this.travelInfo.isHelp = this.isHelp;
    this.travelInfo.isChangeLimit = this.isChangeLimit;

    const result = await travelApi.createAsync({
      solarId: parseInt(this.$route.params.id),
      travelId: this.dynamicData.travel.travelId,
      startTravel: this.dynamicData.travel.startTravel,
      endTravel: this.dynamicData.travel.endTravel,
      contactPhone: this.dynamicData.travel.contactPhone.match(/\d+/g).join(''),
      countries: this.dynamicData.travel.countries,
      cards: this.dynamicData.travel.cards,
      cashWithdrawalLimit: this.dynamicData.travel.cashWithdrawalLimit,
      limitCardTransfers: this.dynamicData.travel.limitCardTransfers,
      isClientAbroad: this.dynamicData.travel.isClientAbroad
    });

    return result;
  }
}
</script>

<style scoped>
</style>
