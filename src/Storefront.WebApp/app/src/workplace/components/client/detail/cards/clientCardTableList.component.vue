<template>
  <div class="no-borders no-shadow">
    <div class="full-width row flex-center" v-if="this.showLoading">
      <block-spinner-component :loaded="this.showLoading" />
    </div>
    <div v-else-if="showError" style="min-height: 122px;" class="full-width row flex-center" >
      <error-data-component  />
    </div>
    <q-table v-else
      class="visual-table no-shadow"
      row-key="cardId"
      :data="localData"
      :columns="columns"
      :hide-header="this.showNoData"
      :rows-per-page-options="[pageSize]"
    >
      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td v-for="col in props.cols" :key="col.name" :props="props">
            <card-status-component v-if="col.name === 'status'" :cardId="props.row.cardId" :status="props.row.status" :currentCard="props.row"/>
            <span v-else-if="col.name === 'number'" @click="toggleSingleRow(props.row)">{{ col.value }}</span>
            <span v-else-if="col.name === 'securePhone'">{{ col.value }}
                <q-tooltip v-if="props.row[col.name]"
                  content-class="bg-white shadow-2"
                  :delay="200"
                  anchor="top right"
                  self="bottom left"
                  :offset="[3, 0]"
                  transition-show="scale"
                  transition-hide="scale">
                    <div class="row tw justify-center items-center">
                      <div class="col-7 text-body2 text-black">{{props.row[col.name]}}</div>
                      <q-badge :class="[props.row.financePhone == props.row.securePhone ? 'badge-positive' : 'badge-negative']" class="q-ml-sm text-body2 text-weight-medium ">ФН</q-badge>
                      </div>
                </q-tooltip>
            </span>
            <span v-else>{{ col.value }}</span>
          </q-td>
        </q-tr>
      </template>

      <template v-slot:no-data>
        <div style="height: 122px;" class="full-width row flex-center" >
          <no-data-component />
        </div>
      </template>
      <template v-slot:bottom>
        <div class="row fit justify-end">
          <paging-component
            :loading="false"
            :initial-page="currentPageIndex"
            :page-size="pageSize"
            :items="listData"
            :max-pages="pageSize"
            @changePage="onChangePage"
          />
        </div>
      </template>
    </q-table>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';
import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import { CardInterface } from '../../../../interfaces/card';
import { GridColumnBaseInterface } from '../../../../interfaces/base';
// import { CardStatusEnum } from '../../../../enums/card/cardStatus.enum';
import BasePagedListComponent from '../../../base/basePagedList.component.vue';
import PagingComponent from '../../../shared/paging.component.vue';
import constants from '../../../../common/constants';
// import { CardStatusEnumHelper } from '../../../../helpers/enumHelpers/cardStatusEnum.helper';
import { FormatDateFilter, FormatCardNumberFilter } from '../../../../filters';
import CardStatusComponent from './audit/cardStatus.component.vue';
import { CardPushInfoStatusEnum } from '../../../../enums/card';
import { CardPushInfoStatusEnumHelper } from '../../../../helpers/enumHelpers/cardPushInfoStatusEnum.helper';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent, PagingComponent, CardStatusComponent },
  filters: { FormatDateFilter, FormatCardNumberFilter }
})
export default class ClientCardTableListComponent extends BasePagedListComponent<CardInterface> {
  @Prop({ required: true }) declare listData: Array<CardInterface>;
  @Prop({ required: true, default: constants.paging.pageSize }) pageSize!: number;

  private columns: Array<GridColumnBaseInterface> = [];
  private selected: Array<CardInterface> = [];

  constructor () {
    super();
    this.currentPageIndexKey = constants.navigateQuery.card.pageIndex;
    this.columns = [
      {
        name: 'number',
        align: 'left',
        label: this.$t('components.client.detail.cards.clientCardTableList.columns.number').toString(),
        field: (row: any) => FormatCardNumberFilter(row.number),
        classes: 'cursor-pointer table-textcolor'
      },
      {
        name: 'expired',
        align: 'left',
        label: this.$t('components.client.detail.cards.clientCardTableList.columns.expired').toString(),
        field: (row: any) => FormatDateFilter(row.expired, constants.formatting.expiryDate)
      },
      {
        name: 'embossedName',
        align: 'left',
        label: this.$t('components.client.detail.cards.clientCardTableList.columns.embossedName').toString(),
        field: 'embossedName'
      },
      {
        name: 'pushInfo',
        align: 'left',
        label: this.$t('components.client.detail.cards.clientCardTableList.columns.pushInfoPhone').toString(),
        field: 'pushInfo',
        format: (val: any) => this.getPushInfoStatus(val)
      },
      {
        name: 'securePhone',
        align: 'left',
        label: this.$t('components.client.detail.cards.clientCardTableList.columns.securePhone').toString(),
        field: 'securePhone',
        classes: 'cursor-pointer table-textcolor link-tooltip',
        format: (val: any) => this.getCardService(val)
      },
      {
        name: 'inStopList',
        align: 'left',
        label: this.$t('components.client.detail.cards.clientCardTableList.columns.inStopList').toString(),
        field: 'inStopList',
        // classes: 'table-textcolor',
        format: (val: boolean) => this.getLimit(val)
      },
      {
        name: 'status',
        align: 'left',
        label: this.$t('components.client.detail.cards.clientCardTableList.columns.status').toString(),
        field: 'status'
      }
    ];
  }

  @Watch('listData', { immediate: true })
  private onUpdated () {
    this.localData = [...this.listData];
  }

  getLimit (val:boolean) {
    return val ? this.$t('components.client.detail.cards.clientCardTableList.limit.yes').toString()
      : this.$t('components.client.detail.cards.clientCardTableList.limit.no').toString();
  }

  getCardService (value: string) {
    return value ? this.$t('components.client.detail.cards.clientCardTableList.services.yes').toString()
      : this.$t('components.client.detail.cards.clientCardTableList.services.no').toString();
  }

  getPushInfoStatus (value: CardPushInfoStatusEnum) {
    return CardPushInfoStatusEnumHelper.getPushInfoStatusName(value);
  }

  // With no key pressed - single selection
  async toggleSingleRow (row: CardInterface) {
    this.selected = [];
    this.selected.push(row);

    await this.navigateBySolarId('clientDetail', row.solarId as number,
      {
        [constants.navigateQuery.card.cardId]: row.cardId.toString(),
        [constants.navigateQuery.agreement.agreementId]: row.agreementId?.toString()
      });
  }
}
</script>

<style scoped>
  .table-textcolor {
    color: #0078D4;
  }
  .tw {
    width: 170px;
  }

  .link-tooltip:hover {
    text-decoration: underline;
    color: #26272B;
  }
    .q-badge {
    height: 16px;
    padding-top:3px;
  }
  @media (min-width: 1920px) {
    .tbl  {
      font-size:16px;
    }
    .q-badge {
      height: 20px;
      padding-top:4px;
    }
  }
  .badge-positive { background-color: #107C10; }
  .badge-negative { background-color: #D83B01; }
</style>
