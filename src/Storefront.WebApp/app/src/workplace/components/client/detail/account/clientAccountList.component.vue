<template>
  <q-card class="q-mb-md">
    <div v-if="this.showLoading">
      <block-spinner-component :loaded="this.showLoading" />
    </div>
    <div v-else-if="showError">
      <error-data-component style="min-height:122px;" />
    </div>
    <q-table v-else
             class="visual-table no-shadow"
             row-key="number"
             :data="localData"
             :columns="columns"
             :hide-header="this.showNoData"
             :rows-per-page-options="[pageSize]"
    >
      <template v-slot:top>
        <q-icon name="arrow_back" style="font-size: 24px; cursor: pointer;" class="q-mr-md" @click="clearFilter" v-if="showBack"/>
        <div class="col-2 q-table__title text-h6">{{ $t('components.client.detail.account.clientAccountList.title') }}</div>
      </template>
      <template v-slot:body="props">
        <q-tr class="cursor-pointer"
              :props="props"
              @click.exact="toggleSingleRow(props.row)">
          <q-td v-for="col in props.cols" :key="col.name" :props="props">
            {{ col.value }}
          </q-td>
        </q-tr>
      </template>
      <template v-slot:no-data>
        <no-data-component style="height:122px;"/>
      </template>
      <template v-slot:bottom>
        <div class="row fit justify-end">
          <paging-component
            v-show="showIfMultipleRows"
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
  </q-card>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';
import { AccountStatusEnum } from '../../../../enums/account/accountStatus.enum';
import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';
import BasePagedListComponent from '../../../base/basePagedList.component.vue';
import { AccountInterface } from '../../../../interfaces/account/account.interface';
import PagingComponent from '../../../shared/paging.component.vue';
import constants from '../../../../common/constants';
import { GridColumnBaseInterface } from '../../../../interfaces/base';
import { AccountStatusEnumHelper } from '../../../../helpers/enumHelpers/accountStatusEnum.helper';
import { AccountTypeEnum } from '../../../../enums/account/accountType.enum';
import { AccountTypeEnumHelper } from '../../../../helpers/enumHelpers/accountTypeEnum.helper';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent, PagingComponent }
})
export default class ClientAccountListComponent extends BasePagedListComponent<AccountInterface> {
  @Prop({ required: true }) declare listData: Array<AccountInterface>;
  @Prop({ required: true, default: constants.paging.pageSize }) pageSize!: number;

  // private localData: Array<AccountInterface>;
  private columns: Array<GridColumnBaseInterface> = [];
  private selected: Array<AccountInterface> = [];

  constructor () {
    super();
    this.currentPageIndexKey = constants.navigateQuery.account.pageIndex;
    this.columns = [
      {
        name: 'number',
        align: 'left',
        label: this.$t('components.client.detail.account.clientAccountList.columns.accountNumber').toString(),
        field: 'number'
      },
      {
        name: 'currency',
        align: 'left',
        label: this.$t('components.client.detail.account.clientAccountList.columns.currency').toString(),
        field: 'currency'
      },
      {
        name: 'type',
        align: 'left',
        label: this.$t('components.client.detail.account.clientAccountList.columns.accountType').toString(),
        field: 'type',
        format: (type: AccountTypeEnum) => AccountTypeEnumHelper.getTypeName(type)
      },
      {
        name: 'status',
        align: 'left',
        label: this.$t('components.client.detail.account.clientAccountList.columns.status').toString(),
        field: 'status',
        format: (status: AccountStatusEnum) => AccountStatusEnumHelper.getStatusName(status)
      }
    ];
  }

  @Watch('listData', { immediate: true })
  private onUpdated () {
    this.localData = [...this.listData];
  }

  get showBack () {
    return (NavigateQueryHelper.agreements.hasAgreementId(this.$route) || NavigateQueryHelper.accounts.hasAccountId(this.$route));
  }

  async clearFilter () {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id), {});
  }

  // With no key pressed - single selection
  async toggleSingleRow (row: AccountInterface) {
    this.selected = [];
    this.selected.push(row);

    await this.navigateBySolarId('clientDetail', row.solarId as number,
      {
        [constants.navigateQuery.account.accountId]: row.accountId.toString(),
        [constants.navigateQuery.agreement.agreementId]: row.agreementId?.toString()
      });
  }

  private get showIfMultipleRows () {
    return this.listData.length > 1;
  }
}
</script>

<style scoped>
</style>
