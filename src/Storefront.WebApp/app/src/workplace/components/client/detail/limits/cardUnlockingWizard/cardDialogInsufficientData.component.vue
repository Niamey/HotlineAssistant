<template>
  <div>
    <div class="row red-color flex-start q-mb-lg">
      <q-icon class="col-auto q-mr-xs" name="close" size="20px" />
      <div class="col">{{ this.unlockingInfo.success === false ? `${$t('components.client.wizard.unlocking.operationsNotConfirmed')} ${$t('components.client.wizard.unlocking.notEnoughInformationToUnlock')}` : $t('components.client.wizard.unlocking.notEnoughInformationToUnlock') }}</div>
    </div>

    <div class="row tbl text-body2">
      <div class="col-5 q-mb-sm">{{ $t('components.client.wizard.unlocking.financialPhone')}}</div>
      <div class="col-7 q-mb-sm">{{ this.unlockingInfo.financePhone }}</div>

      <div class="col-5 q-mb-sm">{{ $t('components.client.wizard.unlocking.contactPhone')}}</div>
      <div class="col-7 q-mb-sm">{{ this.unlockingInfo.contactPhone }}</div>

      <div class="col-5 q-mb-sm">{{ $t('components.client.wizard.unlocking.cardMask')}}</div>
      <div class="col-7 q-mb-sm">{{ this.getCardMaskedNumber }}</div>

      <div class="col-5 q-mb-sm">{{ $t('components.client.wizard.unlocking.dialogId')}}</div>
      <div class="col-7 q-mb-sm"></div>
    </div>

    <template v-if="this.unlockingInfo.difficultToAnswer === true || this.unlockingInfo.success === false">
      <div class="text-grey-7 text-caption q-mt-lg">{{ $t('components.client.wizard.unlocking.specifyReasonNonConfirmation')}}</div>
      <q-input v-model="reason"
                  filled
                  type="textarea"
                  class="q-my-sm"
      />
    </template>
    <div v-else class="detail-wrapper rounded q-pb-md q-pt-md">
        <div class="text-weight-medium q-mx-md">{{ $t('components.client.wizard.unlocking.clientConfirmsThatNotPerformOperation')}}</div>
         <q-list dense>
            <q-item>
              <q-item-section side>
                <q-icon color="primary" name="lens" size="8px"/>
              </q-item-section>
              <q-item-section>
                {{ $t('components.client.wizard.unlocking.methodsCardReissueProvided')}}
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section side>
                <q-icon color="primary" name="lens" size="8px"/>
              </q-item-section>
              <q-item-section>
                {{ $t('components.client.wizard.unlocking.recommendedContactPolice')}}
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section side>
                <q-icon color="primary" name="lens" size="8px"/>
              </q-item-section>
              <q-item-section>
                {{ $t('components.client.wizard.unlocking.informedPossibleCall')}}
              </q-item-section>
            </q-item>
         </q-list>
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';
import { mixins } from 'vue-class-component';
import CardDetailMixin from '../../../../../mixins/card/detail/cardDetail.mixin';
import { FormatCardNumberFilter } from '../../../../../filters';

import { Store } from 'vuex';
import { cardApi } from '../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';

import { CardUnlockingFailedRequestInterface } from '../../../../../interfaces/requests/card/limits';
import { CardUnlockingFailedStatusResponseInterface } from '../../../../../interfaces/responses/card/limits';

@Component({
  name: 'CardDialogInsufficientDataComponent',
  filters: { FormatCardNumberFilter }
})
export default class CardDialogInsufficientDataComponent extends mixins(CardDetailMixin, BaseStepComponent) implements BaseStepInterface {
  private reason: string;

  constructor () {
    super();
    this.reason = '';
  }

  get getCommentText () {
    return `${this.$t('components.client.wizard.unlocking.clientConfirmsThatNotPerformOperation')}
    ${this.$t('components.client.wizard.unlocking.methodsCardReissueProvided')}
    ${this.$t('components.client.wizard.unlocking.recommendedContactPolice')}
    ${this.$t('components.client.wizard.unlocking.informedPossibleCall')}`;
  }

  get getCardMaskedNumber () {
    return FormatCardNumberFilter(this.CardDetail?.number);
  }

  validateStep (): string|string[]|undefined {
    return (this.unlockingInfo.difficultToAnswer && this.reason.length < 5) ? this.$t('components.client.wizard.unlocking.errors.cardDialogInsufficientData.requiredReason').toString() : undefined;
  }

  async collectData () {
    const localUnlockingInfo = Object.assign({}, this.unlockingInfo);

    localUnlockingInfo.cardMaskedNumber = this.getCardMaskedNumber;

    if (this.unlockingInfo.difficultToAnswer === true || this.unlockingInfo.success === false) {
      localUnlockingInfo.comment = this.reason;
    } else {
      localUnlockingInfo.comment = this.getCommentText;
    }

    this.$emit('update:unlocking-info', localUnlockingInfo);

    this.dynamicData.cardUnlockingData.solarId = this.CardDetail?.solarId;
    this.dynamicData.cardUnlockingData.cardId = this.CardDetail?.cardId;

    const result = await this.unlockingNotSuccessAsync(this.$store, this.dynamicData.cardUnlockingData);

    return result;
  }

  private async unlockingNotSuccessAsync (store: Store<any>, params: CardUnlockingFailedRequestInterface) : Promise<void | ApiResultModel<CardUnlockingFailedStatusResponseInterface>> {
    const promise = cardApi.unlockingFailedAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardUnlockingFailedStatusResponseInterface>(store, promise);
  }
}
</script>

<style scoped>
.tbl > div:nth-child(2n+1) {
    color: #7D7D80
  }
.red-color {
  color: #D83B01;
}
.detail-wrapper {
  background-color: #F8F9F9;
}
</style>
