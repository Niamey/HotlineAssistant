<template>
  <div>
    <div class="row items-center">
      <div class="col text-body1 text-weight-medium">
        {{ getComponentlabel }}
      </div>
      <div class="col-auto">
        <q-toggle v-if="getIsShowAllOperation"
          v-model="allClientOperations"
          left-label
          :label="getAllClientOperationsLabel"
          @input="allClientOperationsUpdate"
        />
      </div>
    </div>
    <div v-if="!allClientOperations" class="row items-center q-py-sm">
      <div class="col-5">
          <q-input v-model="dateRangeText" dense readonly @click="onBeforeShowDateRange">
            <template v-slot:prepend>
              <q-icon name="event" class="cursor-pointer" @click="onBeforeShowDateRange" />
              <q-popup-proxy ref="datePopup" transition-show="scale" transition-hide="scale">
                <q-date range v-model="dateRange" :mask="formatting.date" today-btn>
                  <div class="row items-center justify-end">
                    <q-btn v-close-popup @click="dateInput" :label="$t('components.client.wizard.wizard.calendar.close')" color="primary" outline no-caps/>
                  </div>
                </q-date>
              </q-popup-proxy>
            </template>
          </q-input>
      </div>
      <div class="col-7 text-grey-7 text-caption text-right">
         <div class="text-primary cursor-pointer q-mt-md" @click="navigateToStatement">{{$t('components.client.statement.statementDetail.setStatement')}}</div>
        {{ $tc('components.client.wizard.wizard.selectedColumns', selectedRows.length ) }}
      </div>
    </div>
    <div class="q-mt-sm row text-grey-7 flex-center" v-else-if="allClientOperations && showTransactionsNotPossibleReissue">
      <q-icon class="col-auto q-mr-xs" name="o_info" style="font-size:16px; margin-top:-1px;" />
      <div class="col text-caption">{{ $t('components.client.wizard.wizard.transactionsNotPossibleReissue') }}</div>
    </div>
    <template v-if="!allClientOperations">
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
        <template v-slot:bottom>
        <div class="row fit justify-end">
          <pager-component class="pager"
            :loading="isLoading"
            :page-index="pageIndex"
            :page-size="pageSize"
            :is-available-next-page="isNextPageAvailable"
            @onNavigate="onPagerNavigate" />
        </div>
      </template>
      </q-table>
    </template>

    <q-dialog v-model="isConfirmDate" persistent>
      <q-card>
        <q-card-section class="row items-center">
          <q-icon name="help_outline" color="primary" size="lg" class="col-auto" />
          <span class="col q-ml-md">{{ $t('components.client.wizard.wizard.confirmMessage') }}</span>
        </q-card-section>

        <q-card-actions align="right">
          <q-btn outline :label="$t('components.client.wizard.wizard.confirm.no')" color="primary" @click="onCancelConfirm"></q-btn>
          <q-btn :label="$t('components.client.wizard.wizard.confirm.yes')" color="primary" @click="onOkConfirm"></q-btn>
        </q-card-actions>
      </q-card>
    </q-dialog>

     <div v-if="showTransactionNotPossible" class="q-mt-sm row text-grey-7 flex-top">
        <q-icon class="col-auto q-ma-xs" name="o_info" />
        <div class="col text-caption">{{ $t('components.client.wizard.wizard.transactionsNotPossibleReissue') }}</div>
      </div>
  </div>
</template>

<script lang="ts">
import { Component, Watch } from 'vue-property-decorator';
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
import { TransactionListFinancialBlockingRequestInterface } from '../../../../../interfaces/requests/transaction';
import { TransactionFinancialModel } from '../../../../../models/transaction';

import { date as QuasarDate, QPopupProxy } from 'quasar';
import constants from '../../../../../common/constants';

import PagerComponent from '../../../../../components/shared/pager.component.vue';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';
import { ClientTransactionsModel, LimitsDateRangeModel } from '../../../../../models/card/limits';
import RouteHelper from '../../../../../mixins/routing/router.mixin';
import { mixins } from 'vue-class-component';
import { ReasonTypeEnum } from '../../../../../enums/card/limits/reasonType.enum';
// import { EntryTypeEnum } from '../../../../../enums/transaction';

interface IClientOperationListComponentData {
  operationId: number,
  rowId?: string,
  operationDate?: string,
  merchantName?: string,
  operationDetails?: string,
  amount?: number,
  currency?: string,
  status?: string,
}

@Component({
  name: 'ChoiceClientTransactionsComponent',
  components: { NoDataComponent, BlockSpinnerComponent, ErrorDataComponent, PagerComponent },
  filters: { FormatMoneyFilter }
})
export default class ChoiceClientTransactionsComponent extends mixins(BaseStepComponent, RouteHelper) implements BaseStepInterface {
  private allClientOperations: boolean;
  private dateRangeText: any;
  private dateRange: any;

  private columns: Array<GridColumnBaseInterface> = [];
  private tableData?: Array<IClientOperationListComponentData> = [];
  private selectedRows: Array<any> = [];

  private isLoading: boolean;
  private hasError: boolean;
  private pageIndex: number;
  private pageSize: number;
  private isNextPageAvailable: boolean;

  private isConfirmDate: boolean;
  private formatting: any;

  constructor () {
    super();
    this.formatting = constants.formatting;

    this.allClientOperations = false;
    this.dateRangeText = '';
    this.dateRange = '';

    this.isLoading = false;
    this.hasError = false;
    this.pageIndex = 1;
    this.pageSize = constants.transactionFinancialBlocking.pageSize || 10;
    this.isNextPageAvailable = false;

    this.isConfirmDate = false;

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

    if (this.dateRange === '') {
      this.dateRange = this.getDefaultDateRange;
    }

    await this.doRequest();
  }

  $refs!: {
    datePopup: QPopupProxy;
  }

  get getComponentlabel () {
    return (this.dynamicData.reasonId === ReasonTypeEnum.ClientIsCheater) ? this.$t('components.client.wizard.wizard.chooseCheaterOperation') : this.$t('components.client.wizard.wizard.chooseNotClientOperation');
  }

  get getAllClientOperationsLabel () {
    return (this.dynamicData.reasonId === ReasonTypeEnum.ClientIsCheater) ? this.$t('components.client.wizard.wizard.noOperation') : this.$t('components.client.wizard.wizard.allClientOperations');
  }

  get getIsShowAllOperation () {
    return this.dynamicData.isShowAllOperation ?? true;
  }

  get showTransactionsNotPossibleReissue () {
    return this.dynamicData.reasonId === ReasonTypeEnum.Lost || this.dynamicData.reasonId === ReasonTypeEnum.Stolen;
  }

  get getDefaultDateRange () {
    const currentDate = new Date();
    return {
      from: QuasarDate.formatDate(QuasarDate.subtractFromDate(currentDate, { days: constants.transactionFinancialBlocking.defaultDays || 30 }), this.formatting.date),
      to: QuasarDate.formatDate(currentDate, this.formatting.date)
    };
  }

  private onBeforeShowDateRange (evt: any) {
    evt.stopPropagation();

    if (this.selectedRows?.length > 0) {
      this.isConfirmDate = true;
    } else {
      this.$refs.datePopup.show();
    }
  }

  private onOkConfirm () {
    this.isConfirmDate = false;
    this.$refs.datePopup.show();
  }

  private onCancelConfirm () {
    this.isConfirmDate = false;
  }

  async dateInput () {
    if (this.dateRange === null || this.dateRange === undefined) return;

    this.pageIndex = 1;
    this.selectedRows = [];
    await this.doRequest();
  }

  @Watch('dateRange', { immediate: true })
  private onDateRangeChange (newVal: any) {
    if (newVal === null || newVal === undefined) return '';

    if (newVal instanceof Object) {
      const from = QuasarDate.formatDate(newVal.from, this.formatting.dateVue),
        to = QuasarDate.formatDate(newVal.to, this.formatting.dateVue);

      this.dateRangeText = `${from} – ${to}`;
    } else {
      this.dateRangeText = QuasarDate.formatDate(newVal, this.formatting.dateVue);
    }
  }

  protected async doRequest () {
    this.tableData = [];
    this.isLoading = true;

    const request:TransactionListFinancialBlockingRequestInterface = {
      solarId: parseInt(this.$route.params.id),
      pageIndex: this.pageIndex,
      pageSize: constants.transactionFinancialBlocking.pageSize
    };

    if (typeof this.dateRange === 'string') {
      request.dateFrom = this.dateRange;
      request.dateTo = this.dateRange;
    } else {
      request.dateFrom = this.dateRange.from;
      request.dateTo = this.dateRange.to;
    }

    const result = await this.loadTransactionListFinancial(this.$store, request);
    // @ts-ignore
    this.hasError = result.hasError;

    if (!this.hasError) {
      // @ts-ignore
      const rows = result?.result?.rows;
      if (rows) {
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

      // @ts-ignore
      this.isNextPageAvailable = result?.result?.isNextPageAvailable ?? false;
    }

    this.isLoading = false;
  }

  private async loadTransactionListFinancial (store: Store<any>, params: TransactionListFinancialBlockingRequestInterface) : Promise<void | ApiResultModel<TransactionListFinancialResponseInterface>> {
    const promise = transactionApi.getTransactionListFinancialForBlockingAsync(params);
    return await ApiWrapperActionHelper.runWithTry<TransactionListFinancialResponseInterface>(store, promise);
  }

  private async onPagerNavigate (page: number) {
    this.pageIndex = page;
    await this.doRequest();
  }

  public validateStep () {
    return (this.allClientOperations === true || this.selectedRows.length > 0) ? undefined
      : this.$t('components.client.wizard.wizard.errors.choiceClientTransactions.requiredAny').toString();
  }

  public collectData () {
    const localReasonInfo = Object.assign({}, this.reasonInfo);

    const mapped = this.allClientOperations ? [] : this.selectedRows.map(i => {
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
    const dateRange = this.allClientOperations ? undefined
      : (typeof this.dateRange === 'string' ? this.dateRange : new LimitsDateRangeModel(this.dateRange));

    localReasonInfo.clientTransactions = new ClientTransactionsModel(dateRange, this.allClientOperations, mapped);
    localReasonInfo.allClientOperations = this.allClientOperations;
    this.$emit('update:reason-info', localReasonInfo);
  }

  private setInitData () {
    if (this.reasonInfo.clientTransactions !== undefined && Object.keys(this.reasonInfo.clientTransactions).length > 0) {
      this.dateRange = this.reasonInfo.clientTransactions.dateRange ?? this.getDefaultDateRange;
      //  this.onDateRangeChange(this.dateRange);

      this.allClientOperations = this.reasonInfo.clientTransactions.allTransaction;

      this.selectedRows = this.reasonInfo.clientTransactions.transactions.map((i:any) => {
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

  private async navigateToStatement () {
    const params = {
      [constants.navigateQuery.statement.clientId]: this.$route.params.id,
      [constants.navigateQuery.agreement.agreementId]: this.dynamicData?.cardDetail.agreementId.toString()
    };
    await this.newTabBySolarId('clientStatement', parseInt(this.$route.params.id), params);
  }

  get showTransactionNotPossible () {
    return this.dynamicData.reasonId === ReasonTypeEnum.ForgottenInATM && this.allClientOperations;
  }

  private allClientOperationsUpdate () {
    const localReasonInfo = Object.assign({}, this.reasonInfo);
    localReasonInfo.allClientOperations = this.allClientOperations;
    this.$emit('update:reason-info', localReasonInfo);
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
    /* padding-right: 0; */
  }
  .visual-table >>> td:nth-child(2){
    padding-left: 0;
  }
  .visual-table >>> td {
    white-space: normal !important;
  }
</style>
