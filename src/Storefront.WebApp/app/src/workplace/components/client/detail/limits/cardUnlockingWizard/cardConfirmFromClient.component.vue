<template>
  <block-spinner-component v-if="isLoading" :loaded="isLoading" />
  <error-data-component v-else-if="hasError" />
  <div v-else>
    <div class="detail-wrapper rounded q-pb-md q-pt-md q-mb-md">
        <div class="text-weight-medium q-mx-md">{{ $t('components.client.wizard.unlocking.checkFromClientToConfirm')}}</div>
         <q-list dense>
            <q-item>
              <q-item-section>
                {{ this.dynamicData.cardCurrentStatus.reason }}
              </q-item-section>
            </q-item>
         </q-list>
    </div>
    <div>{{ $t('components.client.wizard.unlocking.clientConfirmData') }}</div>
    <div class="q-gutter-sm">
        <q-option-group
          class="q-ml-none"
          :inline="true"
          :options="dataOptions"
          type="radio"
          v-model="dataConfirmed"
        />
    </div>
    <template v-if="dataConfirmed === false">
      <div>{{ $t('components.client.wizard.unlocking.clientDifficultToAnswer') }}</div>
      <div class="q-gutter-sm">
          <q-option-group
            class="q-ml-none"
            :inline="true"
            :options="dataOptions"
            type="radio"
            v-model="difficultToAnswer"
          />
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';

import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../../../components/shared/errorData.component.vue';

@Component({
  name: 'CardConfirmFromClientComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent }
})
export default class CardConfirmFromClientComponent extends BaseStepComponent implements BaseStepInterface {
  private dataConfirmed: boolean | null;
  private dataOptions: Array<any>;
  private difficultToAnswer: boolean | null;

  constructor () {
    super();
    this.dataConfirmed = null;
    this.difficultToAnswer = null;
    this.dataOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];
  }

  public validateStep () {
    const err:string[] = [];
    if (this.dataConfirmed === null) err.push(this.$t('components.client.wizard.unlocking.errors.cardConfirmFromClient.requiredDataConfirmed').toString());
    if (this.dataConfirmed === false && this.difficultToAnswer === null) err.push(this.$t('components.client.wizard.unlocking.errors.cardConfirmFromClient.requiredDifficultToAnswer').toString());
    return (err.length > 0) ? err : undefined;
  }

  public collectData () {
    const localUnlockingInfo = Object.assign({}, this.unlockingInfo);

    localUnlockingInfo.dataConfirmed = this.dataConfirmed;
    localUnlockingInfo.difficultToAnswer = this.dataConfirmed === false ? this.difficultToAnswer : null;
    this.$emit('update:unlocking-info', localUnlockingInfo);
  }
}
</script>

<style scoped>
.detail-wrapper {
  background-color: #F8F9F9;
}
</style>
