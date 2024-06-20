<template>
  <div>
    <span class="cursor-pointer link" @click="showStatusPopup">{{ getCardStatus }}</span>
    <q-dialog v-model="showDialog">
      <secure-3-d-status-history-component
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
import { CardSecure3DStatusHistoryEnum } from '../../../../../../enums/card/cardSecure3DStatusHistory.enum';
import { CardSecure3DStatusHistoryEnumHelper } from '../../../../../../helpers/enumHelpers/cardSecure3DStatusHistoryEnum.helper';
import Secure3DStatusHistoryComponent from './secure3DStatusHistory.component.vue';
import { CardSecure3DStatusHistoryRequestInterface } from '../../../../../../interfaces/requests/card/status';
import { CardSecure3DStatusHistoryResponseInterface } from '../../../../../../interfaces/responses/card/status';
import { cardApi } from '../../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { NavigateQueryHelper } from '../../../../../../helpers/navigateQuery.helper';

@Component({
  components: { Secure3DStatusHistoryComponent }
})
export default class Secure3DStatusComponent extends Vue {
  @Prop() cardId!: number;
  @Prop() status!: CardSecure3DStatusHistoryEnum;

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
    void this.doCardStatusHistoryRequest();
  }

  private async doCardStatusHistoryRequest () {
    this.isLoading = true;
    const result = await this.loadCardSecure3DStatusHistory(this.$store, { cardId: NavigateQueryHelper.cards.getCardId(this.$route) as number });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      this.statusHistory = result.result?.item;
    }
    this.isLoading = false;
  }

  private async loadCardSecure3DStatusHistory (store: Store<any>, params:CardSecure3DStatusHistoryRequestInterface) : Promise<void | ApiResultModel<CardSecure3DStatusHistoryResponseInterface>> {
    const promise = cardApi.getSecure3DStatusHistoryAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardSecure3DStatusHistoryResponseInterface>(store, promise);
  }

  get getCardStatus () {
    return CardSecure3DStatusHistoryEnumHelper.getStatusHistoryName(this.status);
  }
}
</script>

<style scoped>
  .link {
    color: #0078D4;
  }
</style>
