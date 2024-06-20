<template>
  <div>
    <div class="row items-center">
      <div class="col text-body1 text-weight-medium">
        {{ $t('components.client.wizard.wizard.chooseNotClientOperation') }}
      </div>
    </div>

    <template>
      <block-spinner-component class="full-width row flex-center" v-if="isLoading" />
      <error-data-component  class="full-width row flex-center" style="height: 150px;" v-else-if="hasError" />
      <q-table
        v-else
        class="visual-table no-shadow"
        :data="tableData"
        :columns="columns"
        row-key="rowId"
        selection="multiple"
        :selected.sync="selectedRows"
        hide-selected-banner
        hide-pagination
        :rows-per-page-options="[pageSize]"
      >
        <template v-slot:header="props">
          <q-tr :props="props">
            <q-th colspan="2" class="text-left" width="200px">{{props.cols[0].label}}</q-th>
            <q-th
                v-for="col in props.cols.filter((element, index) => { return index > 0;})"
                :key="col.name"
                :props="props"
            >
              {{ col.label }}
            </q-th>
          </q-tr>
        </template>
        <template v-slot:no-data>
          <div style="height: 150px;" class="full-width row flex-center">
            <no-data-component />
          </div>
        </template>
      </q-table>
    </template>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { GridColumnBaseInterface } from '../../../../../interfaces/base';
import { FormatMoneyFilter } from '../../../../../filters';

import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';

import { transactionApi } from '../../../../../api/transaction.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { TransactionListFinancialResponseInterface } from '../../../../../interfaces/responses/transaction';
import { TransactionListFinancialUnblockingRequestInterface } from '../../../../../interfaces/requests/transaction';
import { TransactionFinancialModel } from '../../../../../models/transaction';

import { date as QuasarDate } from 'quasar';
import constants from '../../../../../common/constants';

import PagerComponent from '../../../../../components/shared/pager.component.vue';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';
// import { EntryTypeEnum } from '../../../../../enums/transaction';

interface IClientOperationListComponentData {
  operationId: number,
  operationDate?: string,
  merchantName?: string,
  operationDetails?: string,
  amount?: string,
  status?: string,
}

@Component({
  name: 'ChoiceClientTransactionsComponent',
  components: { NoDataComponent, BlockSpinnerComponent, ErrorDataComponent, PagerComponent },
  filters: { FormatMoneyFilter }
})
export default class ChoiceClientTransactionsComponent extends BaseStepComponent implements BaseStepInterface {
  private columns: Array<GridColumnBaseInterface> = [];
  private tableData?: Array<IClientOperationListComponentData> = [];
  private selectedRows: Array<any> = [];

  private isLoading: boolean;
  private hasError: boolean;
  private pageSize: number;

  private formatting: any;

  constructor () {
    super();
    this.formatting = constants.formatting;

    this.isLoading = false;
    this.hasError = false;
    this.pageSize = constants.transactionFinancialUnblocking.pageSize || 3;

    this.columns = [
      {
        name: 'operationDate',
        label: this.$t('components.client.wizard.wizard.columns.dateTime').toString(),
        field: 'operationDate',
        align: 'left',
        format: (val:any) => QuasarDate.formatDate(val, this.formatting.dateTimeVue)
      },
      { name: 'merchantName', label: this.$t('components.client.wizard.wizard.columns.merchantName').toString(), field: 'merchantName', align: 'left', headerStyle: 'min-width: 230px;' },
      { name: 'operationDetails', label: this.$t('components.client.wizard.wizard.columns.operationDetails').toString(), field: 'operationDetails', align: 'left', headerStyle: 'min-width: 220px;' },
      {
        name: 'amount',
        label: this.$t('components.client.wizard.wizard.columns.amount').toString(),
        field: (row:any) => `${FormatMoneyFilter(row.amount)} ${row.currency}`,
        align: 'right'
      },
      { name: 'status', label: this.$t('components.client.wizard.wizard.columns.status').toString(), field: 'status', align: 'left' }
    ];
  }

  async created () {
    this.setInitData();
    await this.doRequest();
  }

  protected async doRequest () {
    this.tableData = [];
    this.isLoading = true;

    const request:TransactionListFinancialUnblockingRequestInterface = {
      solarId: parseInt(this.$route.params.id),
      pageSize: constants.transactionFinancialUnblocking.pageSize
    };

    const result = await this.loadTransactionListFinancial(this.$store, request);
    // @ts-ignore
    this.hasError = result.hasError;

    if (!this.hasError) {
      // @ts-ignore
      const rows = result?.result?.rows;
      if (rows?.length > 0) {
        this.tableData = rows.map((i: TransactionFinancialModel) => {
          return {
            operationId: i.txnId,
            rowId: i.rowId,
            operationDate: i.transactionDate,
            merchantName: i.getFullMerchantName,
            operationDetails: i.transactionTypeName,
            amount: i.transactionAmount,
            currency: i.transactionCurrency,
            status: i.txnStatus
          };
        });
      }
    }
    this.isLoading = false;
  }

  private async loadTransactionListFinancial (store: Store<any>, params: TransactionListFinancialUnblockingRequestInterface) : Promise<void | ApiResultModel<TransactionListFinancialResponseInterface>> {
    const promise = transactionApi.getTransactionListFinancialForUnBlockingAsync(params);
    return await ApiWrapperActionHelper.runWithTry<TransactionListFinancialResponseInterface>(store, promise);
  }

  public validateStep () {
    return undefined;
  }

  public collectData () {
    const localUnlockingInfo = Object.assign({}, this.unlockingInfo);

    localUnlockingInfo.confirmedTransactions = this.selectedRows;
    localUnlockingInfo.success = this.pageSize === this.selectedRows.length;

    this.$emit('update:unlocking-info', localUnlockingInfo);
  }

  private setInitData () {
    if (!this.unlockingInfo) {
      return;
    }
    if (this.unlockingInfo.confirmedTransactions !== undefined && Object.keys(this.unlockingInfo.confirmedTransactions).length > 0) {
      this.selectedRows = this.unlockingInfo.confirmedTransactions.map((i:any) => {
        return {
          operationId: i.operationId,
          rowId: i.rowId,
          operationDate: i.operationDate,
          merchantName: i.merchantName,
          operationDetails: i.operationDetails,
          amount: i.amount,
          currency: i.currency,
          status: i.status
        };
      });
    }
  }
}
</script>

<style scoped>
  .visual-table >>> .q-tr:first-of-type:after {
    display: none;
  }
  .visual-table >>> th:first-of-type{
    padding: 7px 10px 7px 0;
  }
  .visual-table >>> .q-table__top {
    padding-left: 0;
    padding-right: 0;
  }
  .visual-table >>> .q-checkbox__inner--truthy .q-checkbox__bg,
  .visual-table >>> .q-checkbox__inner--indet .q-checkbox__bg {
    background: var(--q-color-primary);
    border-color: var(--q-color-primary);
  }
  .visual-table.q-table__card {
    box-shadow: none;
  }
  .visual-table >>> .q-table--col-auto-width {
    padding-left: 0;
  }
  .visual-table >>> td:nth-child(2){
    padding-left: 0;
  }
  .visual-table >>> td {
    white-space: normal !important;
  }
</style>
