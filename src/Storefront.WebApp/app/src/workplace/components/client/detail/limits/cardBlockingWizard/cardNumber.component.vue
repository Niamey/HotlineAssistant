<template>
  <div>
    <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.enterCardNumberForCheck') }}</div>
    <div class="row">
      <q-input class="col-auto" filled dense v-model="cardNumber" />
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';
import { LimitsCheaterModel } from '../../../../../models/card/limits';

@Component({
  name: 'CardNumberComponent'
})
export default class CardNumberComponent extends BaseStepComponent implements BaseStepInterface {
  private cardNumber: string;

  constructor () {
    super();
    this.cardNumber = '';
  }

  created () {
    this.setInitData();
  }

  public validateStep () {
    if (this.cardNumber.trim() === '') return this.$t('components.client.wizard.wizard.errors.cardNumber.requiredCardNumber').toString();

    return undefined;
  }

  public collectData () {
    const localReasonInfo = Object.assign({}, this.reasonInfo);

    if (localReasonInfo.victim !== undefined) {
      localReasonInfo.victim = {};
    }
    localReasonInfo.cheater = new LimitsCheaterModel({ cardNumber: this.cardNumber });
    this.$emit('update:reason-info', localReasonInfo);
  }

  private setInitData () {
    if (this.reasonInfo.cheater !== undefined && Object.keys(this.reasonInfo.cheater).length > 0) {
      this.cardNumber = this.reasonInfo.cheater.cardNumber;
    }
  }
}
</script>

<style scoped>
