<template>
  <div>
    <div class="full-width row flex-center" v-if="this.showLoading">
        <block-spinner-component class="full-width row flex-center" :loaded="this.showLoading" />
    </div>
    <div class="full-width row flex-center shadow-2" style="height: 528px;" v-else-if="showError">
      <error-data-component  />
    </div>
    <q-table v-else
      class="visual-table no-shadow"
      :data="localData"
      :columns="columns"
      :hide-header="this.showNoData"
      :rows-per-page-options="[pageSize]"
      row-key="agreementId"
    >
      <template v-slot:top>
        <div class="col-2 q-table__title text-h6">
          {{ $t('components.client.detail.agreements.clientAgreementList.title') }}
        </div>
        <q-space />
        <div class="col=auto">
          <!--<q-toggle
            v-model="isDebt"
            @input="showDebtRequest"
            left-label
            :label="$t('components.client.detail.agreements.clientAgreementList.toggle.showDebt')"
          />-->
          <q-toggle
            v-model="isInactive"
            @input="onChangeActive"
            left-label
            :label="$t('components.client.detail.agreements.clientAgreementList.toggle.showInactive')"
          />
        </div>
      </template>

      <template v-slot:body="props">
        <q-tr :props="props" v-if="props.row.status !== 2 || isVisibled">
          <q-td>
            <span class="table-textcolor cursor-pointer" @click="toggleSingleRow(props.row)">
              {{ props.row.agreementNumber }}
            </span>
          </q-td>
          <q-td>
              {{ props.row.productName }}
          </q-td>
          <q-td>
            <agreement-status-component :agreementId="props.row.agreementId" :status="props.row.status" />
          </q-td>
          <!--<q-td class="text-right">
            <span class="text-right"
                  :class="{ blured: isBlured }">
              {{ props.row.debt }}
              <span class="q-ml-sm text-caption grey"
                    v-if="props.row.debt !== null">
                {{ props.row.currency }}
              </span>
            </span>
          </q-td>-->
          <q-td></q-td>
        </q-tr>
      </template>

      <template v-slot:bottom>
            <!--<div class="text-subtitle1 col-8 q-pl-sm">
              <span class="q-mr-sm text-weight-medium">
                {{ $t('components.client.detail.agreements.clientAgreementList.totalDebt') }}:
              </span>
              <span :class="{ blured: isBlured }">
                1123123123.23
                <span class="text-caption grey">UAH</span>
              </span>
            </div>-->
            <div class="col-12 justify-end">
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
      <template v-slot:no-data>
        <div style="height: 528px;" class="full-width row flex-center">
          <no-data-component />
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
import BasePagedListComponent from '../../../base/basePagedList.component.vue';
import { AgreementInterface } from '../../../../interfaces/agreement';
import { GridColumnBaseInterface } from '../../../../interfaces/base';
import PagingComponent from '../../../shared/paging.component.vue';
import constants from '../../../../common/constants';
import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';
import AgreementStatusComponent from './audit/agreementStatus.component.vue';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent, PagingComponent, AgreementStatusComponent }
})

export default class ClientAgreementListComponent extends BasePagedListComponent<AgreementInterface> {
  @Prop({ required: true }) declare listData: Array<AgreementInterface>;
  @Prop({ required: true, default: constants.paging.pageSize }) pageSize!: number;

  // private localData: Array<AgreementInterface>;
  private columns: Array<GridColumnBaseInterface> = [];
  private visibleColumns: Array<string> = [];
  private selected: Array<AgreementInterface> = [];

  private isDebt: boolean;
  private isInactive: boolean;
  private isBlured: boolean;
  private isVisibled: boolean;

  constructor () {
    super();
    this.currentPageIndexKey = constants.navigateQuery.agreement.pageIndex;

    this.isBlured = true;
    this.isInactive = NavigateQueryHelper.agreements.getIsInactive(this.$route);
    this.isDebt = NavigateQueryHelper.agreements.getIsDebt(this.$route);
    this.isVisibled = false;

    this.columns = [
      { name: 'agreementNumber', label: this.$t('components.client.detail.agreements.clientAgreementList.columns.number').toString(), field: 'agreementNumber', align: 'left', headerStyle: 'width:150px;' },
      { name: 'productName', label: this.$t('components.client.detail.agreements.clientAgreementList.columns.project').toString(), field: 'productName', align: 'left', headerStyle: 'width:280px;' },
      { name: 'status', label: this.$t('components.client.detail.agreements.clientAgreementList.columns.status').toString(), field: 'status', align: 'left', headerStyle: 'width:100px;' },
      // { name: 'debt', label: this.$t('components.client.detail.agreements.clientAgreementList.columns.debt').toString(), field: 'debt', align: 'right', headerStyle: 'width:100px;' },
      { name: 'spacer', field: '' }
    ];
  }

  @Watch('listData', { immediate: true })
  private onUpdated () {
    this.localData = this.listData;
    this.localData = this.localData.map(i => {
      i.debt = 11111;
      return i;
    });
  }

  async toggleSingleRow (row: AgreementInterface) {
    this.selected = [];
    this.selected.push(row);

    await this.navigateBySolarId('clientDetail', row.solarId as number,
      {
        [constants.navigateQuery.agreement.agreementId]: row.agreementId.toString()
      });
  }

  // eslint-disable-next-line
  /* showDebtRequest (value: boolean) {
    this.$route.query['agreement.IsDebt'] = value.toString();

    if (this.isDebt) {
      // делаем запрос для получения задолженностей
      setTimeout(() => {
        this.localData = this.localData.map(i => {
          i.debt = i.agreementId === 100601 ? 11111.22 : i.debt;
          return i;
        });
        this.isBlured = false;
      }, 2000);
    } else {
      // прячем задолженности
      this.localData = this.localData.map(i => {
        if (i.debt !== null) {
          i.debt = 999999999.99;
        }
        return i;
      });
      this.isBlured = true;
    }
  } */

  onChangeActive () {
    this.navigateByQueryParam(constants.navigateQuery.agreement.isInactive, this.isInactive, false);
  }
}
</script>

<style scoped>
  .table-textcolor {
    color: #0078D4;
  }

  .blured {
    filter: blur(3px);
  }

  .visual-table >>> .q-table__bottom {
    background-color: #F4F4F4;
  }
  .visual-table >>> .q-table__bottom--nodata {
    background-color: #fff;
  }

  .grey {
    color: #7D7D80;
  }
</style>
