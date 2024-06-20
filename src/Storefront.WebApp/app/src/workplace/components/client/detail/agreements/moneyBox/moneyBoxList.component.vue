<template>
  <div class="q-mb-md">
    <div v-if="this.MoneyBoxListLoading">
      <block-spinner-component :loaded="this.MoneyBoxListLoading" />
    </div>
    <div v-else-if="this.MoneyBoxListHasError">
      <error-data-component style="min-height:122px;" />
    </div>
    <q-table v-else
             class="visual-table no-shadow"
             row-key="number"
             :data="MoneyBoxList"
             :columns="columns"
             hide-pagination
    >
      <template v-slot:body="props">
        <q-tr class="cursor-pointer"
              :props="props">
          <q-td v-for="col in props.cols" :key="col.name" :props="props">
            <template v-if="col.name === 'balance'">
              {{ col.value.amount | FormatMoneyFilter}} <small class="currency">{{ col.value.currency }}</small>
            </template>
            <span v-else>{{ col.value }}</span>
          </q-td>
        </q-tr>
      </template>
      <template v-slot:no-data>
        <no-data-component style="height:122px;"/>
      </template>
    </q-table>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { GridColumnBaseInterface } from '../../../../../interfaces/base';
import { MoneyBoxStatusEnum } from '../../../../../enums/agreement/moneyBoxStatus.enum';
import { MoneyBoxStatusEnumHelper } from '../../../../../helpers/enumHelpers/moneyBoxStatusEnum.helper';
import MoneyBoxListMixin from '../../../../../mixins/agreement/moneybox/moneyBoxList.mixin';
import { mixins } from 'vue-class-component';
import { FormatMoneyFilter } from '../../../../../filters';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent },
  filters: { FormatMoneyFilter }
})
export default class MoneyBoxListComponent extends mixins(MoneyBoxListMixin) {
  @Prop({ required: true }) agreementId!: number;
  private columns: Array<GridColumnBaseInterface> = [];
  constructor () {
    super();
    this.columns = [
      {
        name: 'name',
        align: 'left',
        label: this.$t('components.client.detail.agreements.moneyBoxList.name').toString(),
        field: 'name'
      },
      {
        name: 'status',
        align: 'left',
        label: this.$t('components.client.detail.agreements.moneyBoxList.status').toString(),
        field: 'status',
        format: (status: MoneyBoxStatusEnum) => MoneyBoxStatusEnumHelper.getStatusName(status)
      },
      {
        name: 'balance',
        align: 'left',
        label: this.$t('components.client.detail.agreements.moneyBoxList.balance').toString(),
        field: 'amount'
      }
    ];
  }

  async mounted () {
    await this.getMoneyBoxesList(this.agreementId);
  }
}
</script>

<style scoped>
  .currency {
    color: #7D7D80;
  }
</style>
