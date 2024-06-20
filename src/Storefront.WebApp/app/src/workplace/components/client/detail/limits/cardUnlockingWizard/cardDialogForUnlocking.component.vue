<template>
  <div>
    <template v-if="this.dynamicData.currentView === 'unlocked'">
      <div class="row green-color flex-start q-mb-lg">
        <q-icon class="col-auto q-mr-xs" name="done" size="20px" />
        <div class="col">{{ $t('components.client.wizard.unlocking.cardUnlocked') }}</div>
      </div>
      <div class="q-mt-sm row text-grey-7 flex-start">
        <q-icon class="col-auto q-mr-sm q-pt-xs" name="o_info" size="20px" />
        <div class="col">{{ $t('components.client.wizard.unlocking.checkInformationAboutWithdrawals') }}</div>
      </div>
    </template>
    <div v-if="this.dynamicData.currentView === 'locked'" class="row red-color flex-start q-mb-lg">
      <q-icon class="col-auto q-mr-xs" name="close" size="20px" />
      <div class="col">{{ $t('components.client.wizard.unlocking.cardBlocked') }}</div>
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

import { CardUnlockingLockRequestInterface } from '../../../../../interfaces/requests/card/limits';
import { CardUnlockingLockStatusResponseInterface } from '../../../../../interfaces/responses/card/limits';

@Component({
  name: 'CardDialogForUnlockingComponent'
})
export default class CardDialogForUnlockingComponent extends mixins(CardDetailMixin, BaseStepComponent) implements BaseStepInterface {
  validateStep (): string|string[]|undefined {
    return undefined;
  }

  public async collectData () {
    if (this.dynamicData.currentView === 'unlocked') {
      const request:CardUnlockingLockRequestInterface = {
        cardId: this.CardDetail?.cardId,
        comment: this.dynamicData.cardCurrentStatus.reason
      };

      const result = await this.lockCard(this.$store, request);

      return result;
    }
  }

  private async lockCard (store: Store<any>, params: CardUnlockingLockRequestInterface) : Promise<void | ApiResultModel<CardUnlockingLockStatusResponseInterface>> {
    const promise = cardApi.unlockingLockAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardUnlockingLockStatusResponseInterface>(store, promise);
  }
}
</script>

<style scoped>
.red-color {
  color: #D83B01;
}
.green-color {
  color: #107C10;
}
</style>
