<template>
  <div>
    <span class="cursor-pointer link" @click="showStatusPopup">{{ getSmsInfoStatus }}</span>
    <q-dialog v-model="showDialog">
      <sms-info-status-history-component
        :detail-data="statusHistory"
        :has-error="hasError"
        :is-loading="isLoading"
        class="bg-white"
      />
    </q-dialog>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardSmsInfoHistoryStatusEnum } from '../../../../../../enums/card/cardSmsInfoHistoryStatus.enum';
import { CardSmsInfoHistoryStatusEnumHelper } from '../../../../../../helpers/enumHelpers/cardSmsInfoHistoryStatusEnum.helper';
import SmsInfoStatusHistoryComponent from './smsInfoStatusHistory.component.vue';
import { CardSmsInfoHistoryStatusRequestInterface } from '../../../../../../interfaces/requests/card/services';
import { CardSmsInfoHistoryStatusResponseInterface } from '../../../../../../interfaces/responses/card/services';
import { cardApi } from '../../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { NavigateQueryHelper } from '../../../../../../helpers/navigateQuery.helper';

@Component({
  components: { SmsInfoStatusHistoryComponent }
})
export default class SmsInfoStatusComponent extends Vue {
  @Prop() cardId!: number;
  @Prop() status!: CardSmsInfoHistoryStatusEnum;

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

  private showStatusPopup () {
    this.showDialog = true;
    void this.doSmsInfoStatusHistoryRequest();
  }

  private async doSmsInfoStatusHistoryRequest () {
    this.isLoading = true;
    const result = await this.loadSmsInfoHistoryStatus(this.$store, { cardId: NavigateQueryHelper.cards.getCardId(this.$route) as number });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      this.statusHistory = result.result?.item;
    }
    this.isLoading = false;
  }

  private async loadSmsInfoHistoryStatus (store: Store<any>, params:CardSmsInfoHistoryStatusRequestInterface) : Promise<void | ApiResultModel<CardSmsInfoHistoryStatusResponseInterface>> {
    const promise = cardApi.getSmsInfoStatusHistoryAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardSmsInfoHistoryStatusResponseInterface>(store, promise);
  }

  get getSmsInfoStatus () {
    return CardSmsInfoHistoryStatusEnumHelper.getHistoryStatusName(this.status);
  }
}
</script>

<style scoped>
  .link {
    color: #0078D4;
  }
</style>
