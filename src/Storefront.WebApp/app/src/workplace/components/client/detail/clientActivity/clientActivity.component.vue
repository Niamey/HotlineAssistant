<template>
  <div class="client-activity full-height bg-white">
    <div class="row justify-end items-center q-pr-md activity-header">
      <client-card-search-component class="col-auto search-card" @change="onSearchCard" />
      <div class="col-auto q-ml-sm">
        <span class="text-primary cursor-pointer">{{ $t('components.client.detail.clientActivity.filterLabel') }}
          <q-badge v-if="calcFilterCount > 0" align="middle" class="filter-badge q-ml-xs">{{ calcFilterCount }}</q-badge>
          <filter-activity-component :init-filter="filter" @apply-filter="onApplyFilter"/>
        </span>
      </div>
    </div>
    <block-spinner-component v-if="activityIsLoading" />
    <no-data-component v-else-if="!activityHasError && (!activityData || activityData.length == 0)" />
    <error-data-component v-else-if="activityHasError" />
    <div v-else class="scroll-container" @wheel="scrollContainer">
      <q-scroll-area  class="fit activity-scroll" :thumb-style="thumbStyle" :delay="500" ref="activityScrollArea" >
        <div v-for="(items, dateStr) in groupedByDate" :key="dateStr">
          <div class="activity-date font-gray-label q-mt-md">{{ getDateLabel(dateStr) }}</div>
          <q-list class="q-mt-sm list-items">
            <q-item class="no-padding" v-for="(item, index) in items" :key="index">
              <client-activity-item-component :item-data="item" />
            </q-item>
          </q-list>
        </div>

        <div v-if="isShowMoreVisible" class="q-pt-md q-pb-lg row justify-center items-center">
          <q-spinner v-if="showMoreIsLoading" color="primary" size="md" />
          <no-data-small-component v-else-if="!showMoreData && !showMoreHasError" />
          <error-data-small-component v-else-if="showMoreHasError" />
          <div v-else class="row justify-center items-center cursor-pointer" @click="showMore">
            <q-icon color="primary" name="refresh" size="sm" />
            <span class="q-ml-md text-primary ">{{ $t('components.client.detail.clientActivity.showMore') }}</span>
          </div>
        </div>
        <div v-else class="q-pt-md q-pb-lg font-gray-label flex flex-center">{{ $t('components.client.detail.clientActivity.noMoreData') }}</div>

        <q-btn fab icon="keyboard_arrow_up" color="primary" class="fixed-bottom-right fab-button" padding="xs" @click="scrollActivityToTop" :class="{show: isFabVisible}" />
        <q-scroll-observer @scroll="scrollHandler" />
      </q-scroll-area>
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import ClientCardSearchComponent from '../cards/clientCardSearch.component.vue';
import FilterActivityComponent from './filterActivity.component.vue';
import { ClientActivityFilterInterface } from '../../../../interfaces/client/activity/clientActivityFilter.interface';
import ClientActivityItemComponent from './clientActivityItem.component.vue';
import { QScrollArea, date as QuasarDate } from 'quasar';

import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';

import { mixins } from 'vue-class-component';
import { ClientActivityMixin } from '../../../../mixins/client/activity';

import NoDataSmallComponent from '../../../shared/noDataSmall.component.vue';
import ErrorDataSmallComponent from '../../../shared/errorDataSmall.component.vue';
import { ClientActivityFilterTypeEnum } from '../../../../enums/client/activity/ClientActivityFilterType.enum';

@Component({
  components: {
    ClientCardSearchComponent,
    FilterActivityComponent,
    ClientActivityItemComponent,
    BlockSpinnerComponent,
    NoDataComponent,
    ErrorDataComponent,
    NoDataSmallComponent,
    ErrorDataSmallComponent
  }
})
export default class ClientActivityComponent extends mixins(ClientActivityMixin) {
  private thumbStyle:any = {};
  private isFabVisible: boolean;

  $refs!: {
    activityScrollArea: QScrollArea;
  }

  constructor () {
    super();
    this.isFabVisible = false;

    this.thumbStyle = {
      right: '5px',
      borderRadius: '5px',
      backgroundColor: '#7D7D80',
      width: '5px',
      opacity: 1
    };
  }

  private getDateLabel (dateStr: string) {
    const months = Array.from(Array(12).keys()).map(i => {
      return this.$t(`quasar.dateUtil.months[${i}]`).toString();
    });

    return QuasarDate.formatDate(new Date(dateStr), 'DD MMMM', {
      months: months
    });
  }

  private onSearchCard (cardnum?: string) {
    this.cardNum = cardnum || '';
  }

  private async showMore () {
    this.pageIndex++;
    await this.doShowMoreRequest();
  }

  private async onApplyFilter (filter: ClientActivityFilterInterface) {
    const beenDate = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.DateRange),
      beenAmount = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.Operation),
      beenCardNumber = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.Card);

    this.filter = filter;
    const dateRangeFilter = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.DateRange),
      amountRangeFilter = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.Operation),
      cardNumberFilter = this.filter?.filters.filter(i => i.filterType === ClientActivityFilterTypeEnum.Card);

    if ((dateRangeFilter !== undefined && dateRangeFilter.length > 0) || ((dateRangeFilter === undefined || dateRangeFilter.length === 0) && beenDate !== undefined && beenDate.length > 0) ||
      (amountRangeFilter !== undefined && amountRangeFilter.length > 0) || ((amountRangeFilter === undefined || amountRangeFilter.length === 0) && beenAmount !== undefined && beenAmount.length > 0) ||
      (cardNumberFilter !== undefined && cardNumberFilter.length > 0) || ((cardNumberFilter === undefined || cardNumberFilter.length === 0) && beenCardNumber !== undefined && beenCardNumber.length > 0)) {
      this.pageIndex = 1;
      await this.doTransactionListRequest();
    }
  }

  private scrollHandler (ev:any) {
    this.isFabVisible = ev.position > 50 && ev.direction === 'up' && (ev.inflexionPosition - ev.position >= 100);
  }

  private scrollActivityToTop () {
    this.$refs.activityScrollArea.setScrollPosition(0, 200);
  }

  private scrollContainer (evt: any) {
    const position = this.$refs.activityScrollArea.getScrollPosition();
    this.$refs.activityScrollArea.setScrollPosition((position + parseInt(evt.deltaY)), 0);
  }
}
</script>

<style scoped>
  .client-activity {
    min-width: 270px;
  }

  .filter-badge {
    font-size: 10px;
  }

  .activity-date {
    text-align: center;
  }

  .font-gray-label {
    font-size: 12px;
    color: #7D7D80;
  }
  .list-items .q-item {
    margin-top: 2px;
  }

  .activity-header {
    height: 50px;

  }
  .activity-scroll {

  }

  .scroll-container {
    height: calc(100% - 50px) !important;
  }

  .fab-button {
    bottom: 16px;
    right: 16px;
    visibility: hidden;
    opacity: 0;
    transition: all .3s;
  }
  .fab-button.show {
    visibility: visible;
    opacity: 1;
  }

  .search-card {
    font-size: 14px !important;
  }

  .spacer {
    height: 50px;
  }
</style>
