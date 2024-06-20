<template>
  <div>
       <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.enterDataAboutCheater') }}</div>
       <div class="row">
         <div class="col-4 q-pr-xs">
           <q-input v-model="fullName" dense  filled :label="$t('components.client.wizard.wizard.cheaterIntroducedHimself') "/>
         </div>
         <div class="col-4 q-px-xs">
           <vue-tel-input :enabledFlags="false" :placeholder="$t('components.client.wizard.wizard.phone')" :onlyCountries="['UA']" v-model="phone"></vue-tel-input>
         </div>
         <div class="col-4 q-pl-xs">
           <q-input v-model="dateCall" dense filled readonly :label="$t('components.client.wizard.wizard.callDate')">
            <template v-slot:prepend>
              <q-icon name="event" class="cursor-pointer"/>
              <q-popup-proxy ref="datePopup" transition-show="scale" transition-hide="scale">
                <q-date v-model="dateCall" :mask="formatting.dateVue" today-btn>
                  <div class="row items-center justify-end">
                    <q-btn v-close-popup :label="$t('components.client.wizard.wizard.calendar.close')" color="primary" outline no-caps/>
                  </div>
                </q-date>
              </q-popup-proxy>
            </template>
          </q-input>
         </div>
       </div>
        <q-input
                v-model="dataFromCheater"
                filled
                type="textarea"
                class="q-my-md"
                :placeholder="$t('components.client.wizard.wizard.writeDataFromCheater')"
        />
        <div class="q-mt-sm row text-grey-7 flex-center">
          <q-icon class="col-auto q-mr-xs" name="o_info" />
          <div class="col text-caption">{{ $t('components.client.wizard.wizard.allFieldsRequired') }}</div>
        </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import constants from '../../../../../common/constants';

import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
// @ts-ignore
import { VueTelInput } from 'vue-tel-input';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { mixins } from 'vue-class-component';
import ClientDetailMixin from '../../../../../mixins/client/clientDetail.mixin';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';
import { LimitsCheaterModel } from '../../../../../models/card/limits/reasons/limitsCheater.model';

@Component({
  name: 'CheaterDetailsComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent, VueTelInput }
})
export default class CheaterDetailsComponent extends mixins(BaseStepComponent, ClientDetailMixin) implements BaseStepInterface {
  private formatting: any;
  private fullName: string;
  private phone: string;
  private dateCall: any;
  private dataFromCheater: string;

  constructor () {
    super();
    this.formatting = constants.formatting;
    this.fullName = '';
    this.phone = '';
    this.dateCall = '';
    this.dataFromCheater = '';
  }

  validateStep (): string|string[]|undefined {
    const err:string[] = [];
    if (this.fullName.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.cheaterDetails.requiredFullName').toString());
    if (this.phone.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.cheaterDetails.requiredPhone').toString());
    if (!this.dateCall) err.push(this.$t('components.client.wizard.wizard.errors.cheaterDetails.requiredDateCall').toString());
    if (this.dataFromCheater.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.cheaterDetails.requiredDataFromCheater').toString());

    return (err.length > 0) ? err : undefined;
  }

  collectData (): void {
    const localReasonInfo = Object.assign({}, this.reasonInfo);

    localReasonInfo.cheater = new LimitsCheaterModel({
      fullName: this.fullName,
      phone: this.phone,
      dateCall: this.dateCall,
      comments: this.dataFromCheater
    });
    this.$emit('update:reason-info', localReasonInfo);
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
