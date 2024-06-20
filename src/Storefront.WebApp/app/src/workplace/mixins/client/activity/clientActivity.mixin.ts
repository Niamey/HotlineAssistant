import { Component, Vue } from 'vue-property-decorator';

import { transactionApi } from '../../../api/transaction.api';
import { ApiWrapperActionHelper } from '../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { TransactionListResponseInterface } from '../../../interfaces/responses/transaction';
import { TransactionListRequestInterface } from '../../../interfaces/requests/transaction';

import { ClientActivityFilterInterface } from '../../../interfaces/client/activity/clientActivityFilter.interface';
import { utils } from '../../../utils/utils';
import { date as QuasarDate } from 'quasar';

import { ClientActivityFilterTypeEnum } from '../../../enums/client/activity/ClientActivityFilterType.enum';
import constants from '../../../common/constants';

@Component
export class ClientActivityMixin extends Vue {
  protected filter?: ClientActivityFilterInterface | null;
  private filterCount: number;
  protected cardNum: string;

  protected pageIndex: number;

  private activityIsLoading: boolean;
  private activityHasError: boolean;
  private activityData: any = [];

  private showMoreIsLoading: boolean;
  private showMoreHasError: boolean;
  private showMoreData: any = [];
  private isShowMoreVisible: boolean;

  constructor () {
    super();

    this.filterCount = 0;
    this.filter = null;
    this.pageIndex = 1;

    this.cardNum = '';

    this.activityIsLoading = false;
    this.activityHasError = false;

    this.isShowMoreVisible = false;

    this.showMoreIsLoading = false;
    this.showMoreHasError = false;
  }

  async created () {
    await this.doTransactionListRequest();
  }

  get calcFilterCount () {
    this.filterCount = 0;
    if (this.filter !== undefined && this.filter !== null) {
      if (this.filter.filters !== undefined && this.filter.filters.length > 0) {
        this.filterCount = this.filter.filters.length;
      }
    }
    return this.filterCount;
  }

  get filteredActivities () {
    let filtered = this.activityData;
    if (this.cardNum !== undefined && this.cardNum !== null && this.cardNum !== '') {
      filtered = filtered.filter((i:any) => this.cardNum !== undefined && i.cardNum?.slice(-4) === this.cardNum.slice(-4));
    } /* else {
      if (this.filter !== undefined && this.filter !== null) {
        if (this.filter.filters !== undefined && this.filter.filters.length > 0) {
          for (const f of this.filter.filters) {
            switch (f.filterType) {
              case ClientActivityFilterTypeEnum.Card:
                filtered = filtered.filter((i:any) => i.cardNum.slice(-4) === f.filterValueFrom?.toString().slice(-4));
                break;
              case ClientActivityFilterTypeEnum.Operation:
                filtered = filtered.filter((i:any) => {
                  const fromStr = f.filterValueFrom?.toString() || '', toStr = f.filterValueTo?.toString() || '';
                  return ((fromStr !== '' && toStr !== '' && Math.abs(parseInt(i.transactionAmount)) >= parseInt(fromStr) && Math.abs(parseInt(i.transactionAmount)) <= parseInt(toStr)) ||
                    (fromStr !== '' && toStr === '' && Math.abs(parseInt(i.transactionAmount)) >= parseInt(fromStr)) ||
                    (fromStr === '' && toStr !== '' && Math.abs(parseInt(i.transactionAmount)) <= parseInt(toStr)));
                });
                break;
            }
          }
        }
      }
    } */

    return filtered;
  }

  get groupedByDate () {
    const result = this.filteredActivities.reduce((accumulator: any, arr: any) => {
      const date:string = QuasarDate.formatDate(new Date(arr.transactionDate), constants.formatting.date);
      accumulator[date] = accumulator[date] || [];
      accumulator[date].push(arr);
      return accumulator;
    }, {});

    // sort by date
    let sorted = result;
    if (result !== undefined && result !== null) {
      const sortedArr = Object.entries(result).sort((a: any, b: any) => {
        const dateA = new Date(a[0]).getTime(),
          dateB = new Date(b[0]).getTime();

        if (dateA === dateB) return 0;
        return dateA > dateB ? -1 : 1;
      });

      sorted = sortedArr.reduce((accumulator: any, item: any) => {
        accumulator[item[0]] = item[1];
        return accumulator;
      }, {});
    }

    return sorted;
  }

  private async doRequest () {
    const request:TransactionListRequestInterface = {
      solarId: parseInt(this.$route.params.id),
      pageIndex: this.pageIndex,
      pageSize: constants.transaction.pageSize
    };

    const dateRangeFilter = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.DateRange);
    if (dateRangeFilter !== undefined) {
      const dateRange = (dateRangeFilter !== undefined) ? dateRangeFilter[0] : undefined;

      request.dateFrom = dateRange?.filterValueFrom?.toString() ?? '';
      request.dateTo = dateRange?.filterValueTo?.toString() ?? '';
    }

    const amountFilter = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.Operation);
    if (amountFilter !== undefined) {
      const amountRange = (amountFilter !== undefined) ? amountFilter[0] : undefined;
      const fromStr = amountRange?.filterValueFrom?.toString() || '',
        toStr = amountRange?.filterValueTo?.toString() || '';

      if (fromStr !== '') {
        request.amountFrom = parseInt(fromStr);
      }
      if (toStr !== '') {
        request.amountTo = parseInt(toStr);
      }
    }

    const cardNumberFilter = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.Card);
    if (cardNumberFilter !== undefined) {
      const cardNumber = (cardNumberFilter !== undefined) ? cardNumberFilter[0] : undefined;
      request.cardNumber = cardNumber?.filterValueFrom?.toString() || '';
    }
    const result = await this.loadTransactionList(this.$store, request);

    // @ts-ignore
    this.isShowMoreVisible = result?.result?.isNextPageAvailable ?? false;
    return result;
  }

  protected async doTransactionListRequest () {
    this.activityIsLoading = true;
    const result = await this.doRequest();
    // @ts-ignore
    this.activityHasError = result.hasError;
    // @ts-ignore
    this.activityData = result.result?.rows;

    this.activityIsLoading = false;
  }

  private async loadTransactionList (store: Store<any>, params: TransactionListRequestInterface) : Promise<void | ApiResultModel<TransactionListResponseInterface>> {
    const promise = transactionApi.getTransactionListAsync(params);
    return await ApiWrapperActionHelper.runWithTry<TransactionListResponseInterface>(store, promise);
  }

  protected async doShowMoreRequest () {
    this.showMoreIsLoading = true;
    const result = await this.doRequest();

    await utils.delay(500);

    // @ts-ignore
    this.showMoreHasError = result.hasError;
    // @ts-ignore
    this.showMoreData = result.result?.rows;

    if (this.showMoreData?.length > 0) {
      const activitySet = new Set([...this.activityData]);
      for (const item of this.showMoreData) {
        if (!activitySet.has(item)) {
          activitySet.add(item);
        }
      }
      this.activityData = Array.from(activitySet);
    }

    this.showMoreIsLoading = false;
  }
}
