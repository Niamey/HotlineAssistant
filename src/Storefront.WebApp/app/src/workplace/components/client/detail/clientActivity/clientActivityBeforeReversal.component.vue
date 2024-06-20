<template>
    <q-spinner v-if="originalIsLoading" color="primary" size="md" class="row full-width flex-center" />
    <no-data-small-component v-else-if="!originalData && !originalHasError" class="row full-width flex-center" />
    <error-data-small-component v-else-if="originalHasError" class="row full-width flex-center" />
    <div v-else>
      <q-list class="list-items" >
        <q-item dense class="no-padding">
          <client-activity-sub-item-component :item-data="originalData" />
        </q-item>
        <q-item dense class="no-padding" v-for="(child, index) in originalData.childs" :key="index">
          <client-activity-sub-item-component :item-data="child" :detailFields="['dateTime', 'status', 'responseCode', 'info']"/>
        </q-item>
      </q-list>
    </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import NoDataSmallComponent from '../../../shared/noDataSmall.component.vue';
import ErrorDataSmallComponent from '../../../shared/errorDataSmall.component.vue';
import ClientActivitySubItemComponent from './clientActivitySubItem.component.vue';

import { TransactionReversalRequestInterface } from '../../../../interfaces/requests/transaction';
import { TransactionReversalResponseInterface } from '../../../../interfaces/responses/transaction';
import { transactionApi } from '../../../../api/transaction.api';
import { ApiWrapperActionHelper } from '../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../models/api/apiResult.model';
import { Store } from 'vuex';

@Component({
  components: { NoDataSmallComponent, ErrorDataSmallComponent, ClientActivitySubItemComponent },
  filters: { }
})
export default class ClientActivityBeforeReversalComponent extends Vue {
  @Prop({ required: true }) txnId!: number;

  private originalIsLoading: boolean;
  private originalHasError: boolean;
  private originalData: any = {};

  constructor () {
    super();

    this.originalIsLoading = false;
    this.originalHasError = false;
  }

  async created () {
    await this.doRequest();
  }

  private async doRequest () {
    if (!this.txnId) return;

    this.originalIsLoading = true;
    const result = await this.loadTransactionOriginal(this.$store, { solarId: parseInt(this.$route.params.id), txnId: this.txnId });
    if (result instanceof ApiResultModel) {
      this.originalHasError = !!result.hasError;
      if (!this.originalHasError) {
        this.originalData = result.result?.item;
      }
    }
    this.originalIsLoading = false;
  }

  private async loadTransactionOriginal (store: Store<any>, params:TransactionReversalRequestInterface) : Promise<void | ApiResultModel<TransactionReversalResponseInterface>> {
    const promise = transactionApi.getTransactionReversalAsync(params);
    return await ApiWrapperActionHelper.runWithTry<TransactionReversalResponseInterface>(store, promise);
  }
}
</script>

<style scoped>
</style>
