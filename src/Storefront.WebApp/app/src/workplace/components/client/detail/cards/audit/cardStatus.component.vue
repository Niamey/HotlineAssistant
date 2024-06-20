<template>
  <div>
    <span class="cursor-pointer link" @click="showStatusPopup">{{ getCardStatus }}</span>
    <q-dialog v-model="showDialog">
      <card-status-history-component
        :detail-data="statusHistory"
        :has-error="hasError"
        :is-loading="isLoading"
        class="bg-white"
      />
    </q-dialog>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import { CardStatusEnum } from '../../../../../enums/card/cardStatus.enum';
import { CardStatusEnumHelper } from '../../../../../helpers/enumHelpers/cardStatusEnum.helper';
import CardStatusHistoryComponent from './cardStatusHistory.component.vue';
import { CardStatusHistoryRequestInterface } from '../../../../../interfaces/requests/card/status';
import { CardStatusHistoryResponseInterface } from '../../../../../interfaces/responses/card/status';
import { cardApi } from '../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import RouteHelper from '../../../../../mixins/routing/router.mixin';
import constants from '../../../../../common/constants';
import { ClientCardTabsEnum } from '../../../../../enums/client/clientCardTabs.enum';

@Component({
  components: { CardStatusHistoryComponent }
})
export default class CardStatusComponent extends RouteHelper {
  @Prop() cardId!: number;
  @Prop() status!: CardStatusEnum;
  @Prop() currentCard!: any;

  private showDialog: boolean;
  private statusHistory: any;
  private isLoading: boolean;
  private hasError: boolean;

  constructor () {
    super();
    this.showDialog = false;
    this.statusHistory = null;
    this.isLoading = false;
    this.hasError = false;
  }

  private async showStatusPopup () {
    this.showDialog = true;
    await this.doCardStatusHistoryRequest();
    /*
    if (this.status === CardStatusEnum.Active || this.status === CardStatusEnum.Canceled) {
      void this.doCardStatusHistoryRequest();
    } else {
      await this.navigateToStopList();
    }
    */
  }

  private async doCardStatusHistoryRequest () {
    this.isLoading = true;
    const result = await this.loadCardStatusHistory(this.$store, { cardId: this.cardId });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      this.statusHistory = result.result?.item;
    }
    this.isLoading = false;
  }

  private async loadCardStatusHistory (store: Store<any>, params:CardStatusHistoryRequestInterface) : Promise<void | ApiResultModel<CardStatusHistoryResponseInterface>> {
    const promise = cardApi.getStatusHistoryAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardStatusHistoryResponseInterface>(store, promise);
  }

  async navigateToStopList () {
    await this.navigateBySolarId('clientDetail', this.currentCard.solarId as number,
      {
        [constants.navigateQuery.card.cardId]: this.currentCard.cardId.toString(),
        [constants.navigateQuery.account.accountId]: this.currentCard.accountId?.toString(),
        [constants.navigateQuery.agreement.agreementId]: this.currentCard.agreementId?.toString(),
        [constants.navigateQuery.card.tabName]: ClientCardTabsEnum.StopList
      });
  }

  get getCardStatus () {
    return CardStatusEnumHelper.getStatusName(this.status);
  }
}
</script>

<style scoped>
  .link {
    color: #0078D4;
  }
</style>
