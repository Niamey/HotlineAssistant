<template>
  <div>
    <div v-if="this.unlockingInfo.success === true" class="row green-color flex-start q-mb-lg">
      <q-icon class="col-auto q-mr-xs" name="done" size="20px" />
      <div class="col">{{ $t('components.client.wizard.unlocking.operationsConfirmed') }}</div>
    </div>
    <div v-if="this.unlockingInfo.dataConfirmed === true && this.unlockingInfo.isFinancePhoneConfirmed === true" class="row flex-start q-mb-lg">
      <q-icon class="col-auto q-mr-xs" name="done" size="20px" />
      <div class="col">{{ $t('components.client.wizard.unlocking.dataConfirmed') }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';
import { cardApi } from '../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';

import { mixins } from 'vue-class-component';
import CardDetailMixin from '../../../../../mixins/card/detail/cardDetail.mixin';

import { CardUnlockingRequestInterface } from '../../../../../interfaces/requests/card/limits';
import { CardUnlockingStatusResponseInterface } from '../../../../../interfaces/responses/card/limits';

@Component({
  name: 'CardUnlockingComponent'
})
export default class CardUnlockingComponent extends mixins(CardDetailMixin, BaseStepComponent) implements BaseStepInterface {
  validateStep (): string|string[]|undefined {
    return undefined;
  }

  async collectData () {
    const request:CardUnlockingRequestInterface = {
      cardId: this.CardDetail?.cardId,
      comment: undefined
    };

    const result = await this.unlockCard(this.$store, request);

    return result;
  }

  private async unlockCard (store: Store<any>, params: CardUnlockingRequestInterface) : Promise<void | ApiResultModel<CardUnlockingStatusResponseInterface>> {
    const promise = cardApi.unlockingAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardUnlockingStatusResponseInterface>(store, promise);
  }
}
</script>

<style scoped>
.green-color {
  color: #107C10;
}
</style>
