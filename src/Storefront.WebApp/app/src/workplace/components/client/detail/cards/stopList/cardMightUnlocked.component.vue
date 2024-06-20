<template>
  <div>
    <q-btn class="full-width block-button round-borders"
      align="left"
      unelevated
      text-color="primary"
      :label="$t('components.client.detail.cards.limits.cardMightUnlocked.unlockCard')"
      :loading="isLoading"
      no-caps
      @click="unblockCardCheckStatus"
      icon="o_lock_open"
    />
    <card-unlocking-process-component :isShow="isShowUnlockingDialog" :cardCurrentStatus="this.statusData" @hide="hideUnlockingDialog"/>
  </div>
</template>

<script lang="ts">

import { Component, Prop } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';
import CardUnlockingProcessComponent from '../../limits/cardUnlockingProcess.component.vue';

import { CardCurrentStatusModel } from '../../../../../models/card/status';

import { CardUnlockingRequestInterface } from '../../../../../interfaces/requests/card/limits';
import { CardUnlockingStatusResponseInterface } from '../../../../../interfaces/responses/card/limits';

import { cardApi } from '../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import CardDetailMixin from '../../../../../mixins/card/detail/cardDetail.mixin';

import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';

import { CardStatusEnum } from '../../../../../enums/card/cardStatus.enum';

@Component({
  components: { CardUnlockingProcessComponent, BlockSpinnerComponent, ErrorDataComponent, NoDataComponent }
})
export default class CardMightUnlockedComponent extends mixins(CardDetailMixin) {
  @Prop({ required: true }) statusData!: CardCurrentStatusModel;
  private isShowUnlockingDialog: boolean;
  private isLoading: boolean;

  constructor () {
    super();
    this.isLoading = false;
    this.isShowUnlockingDialog = false;
  }

  async unblockCardCheckStatus () {
    this.isLoading = true;

    if (this.canUnblockImmediately) {
      await this.unlockingCard();
    } else {
      this.isLoading = false;
      this.showCardUnlocking();
    }

    this.isLoading = false;
  }

  showCardUnlocking () {
    this.isShowUnlockingDialog = true;
  }

  hideUnlockingDialog () {
    this.isShowUnlockingDialog = false;
  }

  get canUnblockImmediately () {
    return this.statusData?.status === CardStatusEnum.Suspend;
  }

  private async unlockingCard () {
    const request:CardUnlockingRequestInterface = {
      cardId: this.CardDetail?.cardId,
      comment: undefined
    };

    const result = await this.unlockCard(this.$store, request);

    this.isLoading = false;
    // @ts-ignore
    if (!result.hasError) {
      this.$q.notify({
        type: 'positive',
        // @ts-ignore
        message: result?.result.message,
        position: 'top',
        timeout: 2500
      });
      await this.refreshCardDetail();
    }
  }

  private async unlockCard (store: Store<any>, params: CardUnlockingRequestInterface) : Promise<void | ApiResultModel<CardUnlockingStatusResponseInterface>> {
    const promise = cardApi.unlockingAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardUnlockingStatusResponseInterface>(store, promise);
  }
}
</script>

<style scoped>
.block-button {
  background: #F8F9F9;
  height: 44px;
}

.block-button >>> .q-btn__wrapper {
  padding-left: 10px;
  padding-right: 0;
}
</style>
