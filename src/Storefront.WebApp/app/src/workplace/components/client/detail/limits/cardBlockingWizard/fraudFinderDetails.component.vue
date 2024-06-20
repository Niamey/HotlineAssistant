<template>
  <div>
       <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.enterDataPersonWhoCall') }}</div>
       <div class="col">
          <div class='text-grey-7 text-caption'>{{ $t('components.client.wizard.wizard.fullName') }}</div>
          <q-input v-model="fullName" dense filled/>
       </div>
       <div class="row">
          <div class="col-6 q-pr-xs">
              <div class='text-grey-7 text-caption'>{{ $t('components.client.wizard.wizard.cardNumber') }}</div>
              <q-input v-model="cardNumber" dense filled/>
          </div>
          <div class="col-6 q-pl-xs">
              <div class='text-grey-7 text-caption'>{{ $t('components.client.wizard.wizard.phone') }}</div>
              <vue-tel-input :enabledFlags="false" placeholder="" :onlyCountries="['UA']" v-model="phone"></vue-tel-input>
          </div>
       </div>

        <q-input v-model="description"
                filled
                type="textarea"
                class="q-my-md"
                :placeholder="$t('components.client.wizard.wizard.fraudDescription')"
        />
        <div class="q-mt-sm row text-grey-7 flex-center">
          <q-icon class="col-auto q-mr-xs" name="o_info" />
          <div class="col text-caption">{{ $t('components.client.wizard.wizard.allFieldsRequired') }}</div>
        </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
// @ts-ignore
import { VueTelInput } from 'vue-tel-input';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';
import { LimitsVictimModel } from '../../../../../models/card/limits';

@Component({
  name: 'FraudFinderDetailsComponent',
  components: { VueTelInput }
})
export default class FraudFinderDetailsComponent extends BaseStepComponent implements BaseStepInterface {
  private fullName: string;
  private phone: string;
  private cardNumber: string;
  private description: string;

  constructor () {
    super();
    this.fullName = '';
    this.phone = '';
    this.cardNumber = '';
    this.description = '';
  }

  created () {
    this.setInitData();
  }

  public validateStep () {
    const err:string[] = [];
    if (this.fullName.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.fraudFinderDetails.requiredFullName').toString());
    if (this.phone.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.fraudFinderDetails.requiredPhone').toString());
    if (this.cardNumber.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.fraudFinderDetails.requiredCardNumber').toString());
    if (this.description.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.fraudFinderDetails.requiredDescription').toString());

    return (err.length > 0) ? err : undefined;
  }

  public collectData () {
    const localReasonInfo = Object.assign({}, this.reasonInfo);

    localReasonInfo.victim = new LimitsVictimModel({
      fullName: this.fullName,
      phone: this.phone,
      cardNumber: this.cardNumber,
      comments: this.description
    });
    this.$emit('update:reason-info', localReasonInfo);
  }

  private setInitData () {
    if (this.reasonInfo.victim !== undefined && Object.keys(this.reasonInfo.victim).length > 0) {
      this.fullName = this.reasonInfo.victim.fullName;
      this.phone = this.reasonInfo.victim.phone;
      this.cardNumber = this.reasonInfo.victim.cardNumber;
      this.description = this.reasonInfo.victim.comments;
    }
  }
}
</script>

<style scoped>
.vue-tel-input {
  height: 40px;
  border: none;
  background: #f2f2f2;
}
.vue-tel-input >>> input {
  background: #f2f2f2;
}
.vue-tel-input:focus-within {
  box-shadow: none;
  border-bottom: 2px solid var(--q-color-primary)!important;
  background: #E6E6E6;
}
.vue-tel-input:focus-within >>> input {
  background: #E6E6E6;
}
.vue-tel-input >>> .vti__dropdown {
  display: none;
}
</style>
