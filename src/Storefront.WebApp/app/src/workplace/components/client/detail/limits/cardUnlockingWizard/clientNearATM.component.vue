<template>
  <div>
    <div>{{ $t('components.client.wizard.unlocking.clientNearATMorСashbox') }}</div>
    <div class="q-gutter-sm">
        <q-option-group
          class="q-ml-none"
          :inline="true"
          :options="dataOptions"
          type="radio"
          v-model="isNearATM"
          @input="valueChange"
        />
    </div>
    <div v-if="isNearATM !== null" class="q-mt-sm row text-grey-7 flex-start">
      <q-icon class="col-auto q-mr-sm q-pt-xs" name="o_info" size="20px" />
      <div class="col">{{ isNearATM ? $t('components.client.wizard.unlocking.stayNearATMorCashbox') : $t('components.client.wizard.unlocking.contactLaterNearATMorCashbox') }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';
import { cardApi } from '../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';

import { mixins } from 'vue-class-component';
import CardDetailMixin from '../../../../../mixins/card/detail/cardDetail.mixin';

import { CardUnlockingRequestInterface } from '../../../../../interfaces/requests/card/limits';
import { CardUnlockingStatusResponseInterface } from '../../../../../interfaces/responses/card/limits';

@Component({
  name: 'ClientNearATMComponent'
})
export default class ClientNearATMComponent extends mixins(CardDetailMixin, BaseStepComponent) implements BaseStepInterface {
  private isNearATM: boolean | null;
  private dataOptions: Array<any>;

  constructor () {
    super();
    this.isNearATM = null;
    this.dataOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];
  }

  public validateStep () {
    return this.isNearATM === null ? this.$t('components.client.wizard.unlocking.errors.clientNearATM.requiredNearATM').toString() : undefined;
  }

  public async collectData () {
    const localUnlockingInfo = Object.assign({}, this.unlockingInfo);
    localUnlockingInfo.isNearATM = this.isNearATM;
    this.$emit('update:unlocking-info', localUnlockingInfo);

    if (this.isNearATM) {
      const request:CardUnlockingRequestInterface = {
        cardId: this.CardDetail?.cardId,
        comment: undefined
      };

      const result = await this.unlockCard(this.$store, request);

      return result;
    }
  }

  private async unlockCard (store: Store<any>, params: CardUnlockingRequestInterface) : Promise<void | ApiResultModel<CardUnlockingStatusResponseInterface>> {
    const promise = cardApi.unlockingAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardUnlockingStatusResponseInterface>(store, promise);
  }

  valueChange () {
    if (this.isNearATM) {
      this.$emit('updateStep', { isFinish: false, nextButtonLabel: this.$t('components.client.wizard.unlocking.unlockCard') });
    } else {
      this.$emit('updateStep', { isFinish: true, nextButtonLabel: this.$t('components.client.wizard.unlocking.finish') });
    }
  }
}
</script>

<style scoped>

</style>
