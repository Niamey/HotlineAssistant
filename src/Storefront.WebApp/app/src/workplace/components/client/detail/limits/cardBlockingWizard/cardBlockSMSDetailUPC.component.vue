<template>
  <div>
    <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.enterDataAboutSMSInUPC') }}</div>
    <div style="max-width: 250px">
    <q-input filled dense v-model="smsDateUPC" :mask="dateInputMask" :label="$t('components.shared.dateAndTime')">
      <template v-slot:prepend>
        <q-icon name="event" class="cursor-pointer">
          <q-popup-proxy transition-show="scale" transition-hide="scale">
            <q-date v-model="smsDateUPC" :mask="formatting.dateTime">
              <div class="row items-center justify-end">
                <q-btn v-close-popup :label="$t('components.client.wizard.wizard.calendar.close')" color="primary" flat />
              </div>
            </q-date>
          </q-popup-proxy>
        </q-icon>
      </template>

      <template v-slot:append>
        <q-icon name="access_time" class="cursor-pointer">
          <q-popup-proxy transition-show="scale" transition-hide="scale">
            <q-time v-model="smsDateUPC" :mask="formatting.dateTime" format24h>
              <div class="row items-center justify-end">
                <q-btn v-close-popup :label="$t('components.client.wizard.wizard.calendar.close')" color="primary" flat />
              </div>
            </q-time>
          </q-popup-proxy>
        </q-icon>
      </template>
    </q-input>
  </div>
    <q-toggle class="q-mt-sm"
      v-model="isSMSMissingUPC"
      :label="$t('components.client.wizard.wizard.SMSMissing')"
      @input="onSMSMissingUPCUpdate"
    />

    <template v-if="smsDateUPC">
      <div class="q-mt-lg row text-grey-7 flex-start">
        <q-icon class="col-auto q-mr-xs q-mt-xs" name="o_info" size="16px" />
        <div class="col text-caption">
          {{$t('components.client.wizard.wizard.bankRecommendsBlockingCard')}}
        </div>
      </div>
      <div class="q-mt-lg">{{ $t('components.client.wizard.wizard.customerAgreeBlockCard') }}</div>
      <div class="q-gutter-sm">
          <q-option-group
            class="q-ml-none"
            :inline="true"
            :options="clientAgreeBlockMobileAppOptions"
            type="radio"
            v-model="isClientAgreeBlockMobileApp"
          />
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import constants from '../../../../../common/constants';

import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { mixins } from 'vue-class-component';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';

@Component({
  name: 'CardBlockSMSDetailUPCComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent }
})
export default class CardBlockSMSDetailUPCComponent extends mixins(BaseStepComponent) implements BaseStepInterface {
  private formatting: any;
  private isSMSMissingUPC: boolean;
  private smsDateUPC: any;
  private dateInputMask = '##.##.####, ##:##';
  private isClientAgreeBlockMobileApp: boolean | null;
  private clientAgreeBlockMobileAppOptions: any;

  constructor () {
    super();
    this.formatting = constants.formatting;
    this.isSMSMissingUPC = false;
    this.smsDateUPC = '';
    this.isClientAgreeBlockMobileApp = null;
    this.clientAgreeBlockMobileAppOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];
  }

  onSMSMissingUPCUpdate (value: boolean) {
    if (value) this.smsDateUPC = '';
  }

  validateStep (): string|string[]|undefined {
    const err:string[] = [];
    if (!this.isSMSMissingUPC && !this.smsDateUPC) err.push(this.$t('components.client.wizard.wizard.errors.cardBlockSMSDetailUPC.requiredDate').toString());

    if (this.smsDateUPC && this.isClientAgreeBlockMobileApp === null) err.push(this.$t('components.client.wizard.wizard.errors.cardBlockSMSDetailUPC.requiredClientAgree').toString());
    return (err.length > 0) ? err : undefined;
  }

  collectData (): void {
    const localReasonInfo = Object.assign({}, this.reasonInfo);
    localReasonInfo.isSMSMissingUPC = this.isSMSMissingUPC;

    if (!this.isSMSMissingUPC) {
      localReasonInfo.smsDateUPC = this.smsDateUPC;
      localReasonInfo.isClientAgreeBlockMobileApp = this.isClientAgreeBlockMobileApp;
    }

    this.$emit('update:reason-info', localReasonInfo);
  }
}
</script>

<style scoped>

</style>
