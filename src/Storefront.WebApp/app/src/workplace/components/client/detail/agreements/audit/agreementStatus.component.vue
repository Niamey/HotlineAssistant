<template>
  <div>
    <span class="cursor-pointer link" @click="showStatusPopup">{{ getAgreementStatus }}</span>
    <q-dialog v-model="showDialog">
      <agreement-status-history-component
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
import { AgreementStatusEnum } from '../../../../../enums/agreement/agreementStatus.enum';
import { AgreementStatusEnumHelper } from '../../../../../helpers/enumHelpers/agreementStatusEnum.helper';
import AgreementStatusHistoryComponent from './agreementStatusHistory.component.vue';
import { AgreementStatusHistoryRequestInterface } from '../../../../../interfaces/requests/agreement/status';
import { AgreementStatusHistoryResponseInterface } from '../../../../../interfaces/responses/agreement/status';
import { agreementApi } from '../../../../../api/agreement.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';

@Component({
  components: { AgreementStatusHistoryComponent }
})
export default class AgreementStatusComponent extends Vue {
  @Prop() agreementId!: number;
  @Prop() status!: AgreementStatusEnum;

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
    void this.doAgreementStatusHistoryRequest();
  }

  private async doAgreementStatusHistoryRequest () {
    this.isLoading = true;
    const result = await this.loadAgreementStatusHistory(this.$store, { agreementId: this.agreementId });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      this.statusHistory = result.result?.item;
    }
    this.isLoading = false;
  }

  private async loadAgreementStatusHistory (store: Store<any>, params:AgreementStatusHistoryRequestInterface) : Promise<void | ApiResultModel<AgreementStatusHistoryResponseInterface>> {
    const promise = agreementApi.getStatusHistoryAsync(params);
    return await ApiWrapperActionHelper.runWithTry<AgreementStatusHistoryResponseInterface>(store, promise);
  }

  get getAgreementStatus () {
    return AgreementStatusEnumHelper.getStatusName(this.status);
  }
}
</script>

<style scoped>
  .link {
    color: #0078D4;
  }
</style>
