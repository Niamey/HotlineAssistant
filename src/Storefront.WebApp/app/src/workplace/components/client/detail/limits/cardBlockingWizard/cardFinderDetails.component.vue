<template>
  <div>
       <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.enterData') }}</div>
       <div class="row">
         <div class="col-7 q-pr-xs">
           <div class='text-grey-7 text-caption'>{{ $t('components.client.wizard.wizard.fullName') }}</div>
           <q-input v-model="fullName" dense  filled />
         </div>
         <div class="col-5 q-pl-xs">
           <div class='text-grey-7 text-caption'>{{ $t('components.client.wizard.wizard.phone') }}</div>
           <vue-tel-input :enabledFlags="false" placeholder="" :onlyCountries="['UA']" v-model="phone"></vue-tel-input>
         </div>
       </div>
       <q-toggle
            class="q-mt-md"
            v-model="isThingsFound"
            right-label
            :label="$t('components.client.wizard.wizard.thingsFound')"
        />
        <q-input :disable="!isThingsFound"
                v-model="thingsDescription"
                filled
                type="textarea"
                class="q-my-md"
                :placeholder="$t('components.client.wizard.wizard.writeThingsFound')"
        />
        <div v-if="isThingsFound" class="q-mt-sm row text-grey-7 flex-center">
          <q-icon class="col-auto q-mr-xs" name="o_info" />
          <div class="col text-caption">{{ $t('components.client.wizard.wizard.allFieldsRequired') }}</div>
        </div>
        <div class="q-mt-sm row text-grey-7 flex-center">
          <q-icon class="col-auto q-mr-xs" name="o_info" />
          <div class="col text-caption">{{ $t('components.client.wizard.wizard.transactionsNotPossible') }}. {{ isThingsFound ? $t('components.client.wizard.wizard.meetingAgreement') : $t('components.client.wizard.wizard.destroyCard') }}</div>
        </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
// @ts-ignore
import { VueTelInput } from 'vue-tel-input';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';

import { FoundbByAnotherPersonReasonModel } from '../../../../../models/card/limits';

@Component({
  name: 'CardFinderDetailsComponent',
  components: { VueTelInput }
})
export default class CardFinderDetailsComponent extends BaseStepComponent implements BaseStepInterface {
  private fullName: string;
  private phone: string;
  private isThingsFound: boolean;
  private thingsDescription: string;

  constructor () {
    super();
    this.fullName = '';
    this.phone = '';
    this.isThingsFound = false;
    this.thingsDescription = '';
  }

  created () {
    this.setInitData();
  }

  public validateStep () {
    const err:string[] = [];

    if (this.isThingsFound === true && this.fullName.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.сardFinderDetails.requiredFullName').toString());

    if (this.isThingsFound === true && this.phone.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.сardFinderDetails.requiredPhone').toString());

    if (this.isThingsFound === true && this.thingsDescription.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.сardFinderDetails.requiredThingsDesc').toString());

    return (err.length > 0) ? err : undefined;
  }

  public collectData () {
    // const localReasonInfo = Object.assign({}, this.reasonInfo);
    const localReasonInfo = new FoundbByAnotherPersonReasonModel({
      fullName: this.fullName,
      phone: this.phone,
      foundThings: this.isThingsFound,
      comments: this.thingsDescription
    });

    this.$emit('update:reason-info', localReasonInfo);
  }

  private setInitData () {
    if (this.reasonInfo !== undefined && Object.keys(this.reasonInfo).length > 0) {
      this.fullName = this.reasonInfo.fullName;
      this.phone = this.reasonInfo.phone;
      this.isThingsFound = this.reasonInfo.foundThings;
      this.thingsDescription = this.reasonInfo.comments;
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
  border-bottom: 2px solid var(--q-color-primary) !important;
  background: #E6E6E6;
}
.vue-tel-input:focus-within >>> input {
  background: #E6E6E6;
}
.vue-tel-input >>> .vti__dropdown {
  display: none;
}
</style>
