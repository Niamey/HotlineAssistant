<template>
  <div>
    <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.enterDataAboutSMSInGate') }}</div>
    <div style="max-width: 250px">
    <q-input filled dense v-model="smsDateGate" :mask="dateInputMask" :label="$t('components.shared.dateAndTime')">
      <template v-slot:prepend>
        <q-icon name="event" class="cursor-pointer">
          <q-popup-proxy transition-show="scale" transition-hide="scale">
            <q-date v-model="smsDateGate" :mask="formatting.dateTime">
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
            <q-time v-model="smsDateGate" :mask="formatting.dateTime" format24h>
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
      v-model="isSMSMissingGate"
      :label="$t('components.client.wizard.wizard.SMSMissing')"
      @input="onSMSMissingGateUpdate"
    />
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
  name: 'CardBlockSMSDetailGateComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent }
})
export default class CardBlockSMSDetailGateComponent extends mixins(BaseStepComponent) implements BaseStepInterface {
  private formatting: any;
  private isSMSMissingGate: boolean;
  private smsDateGate: any;
  private dateInputMask = '##.##.####, ##:##';

  constructor () {
    super();
    this.formatting = constants.formatting;
    this.isSMSMissingGate = false;
    this.smsDateGate = '';
  }

  onSMSMissingGateUpdate (value: boolean) {
    if (value) this.smsDateGate = '';
  }

  validateStep (): string|string[]|undefined {
    if (!this.isSMSMissingGate && !this.smsDateGate) return this.$t('components.client.wizard.wizard.errors.cardBlockSMSDetailGate.requiredDate').toString();
    return undefined;
  }

  collectData (): void {
    const localReasonInfo = Object.assign({}, this.reasonInfo);
    localReasonInfo.isSMSMissingGate = this.isSMSMissingGate;
    if (!this.isSMSMissingGate) {
      localReasonInfo.smsDateGate = this.smsDateGate;
    }

    this.$emit('update:reason-info', localReasonInfo);
  }
}
</script>

<style scoped>

</style>
