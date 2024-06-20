<template>
  <div>
    <q-icon size="xs" name="error_outline" color="primary">
      <q-popup-proxy :offset="[0, 5]" anchor="bottom middle" self="top middle">
        <q-table
          class="overflow-hidden visual-table no-shadow text-body2"
          row-key="feeId"
          :columns="columns"
          :data="fees"
          hide-bottom
          :rows-per-page-options="[0]"
          wrap-cells
        >
          <template v-slot:body="props">
            <q-tr>
              <q-td>{{ props.row.feeTypeName }}</q-td>
              <q-td class="text-right">{{ props.row.feeAmount | FormatMoneyFilter}} <span class="text-gray">{{ props.row.feeCurrency }}</span></q-td>
            </q-tr>
          </template>
        </q-table>
      </q-popup-proxy>
    </q-icon>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import { TransactionFeeInterface } from '../../../../interfaces/transaction';
import { FormatMoneyFilter } from '../../../../filters';
import { GridColumnBaseInterface } from '../../../../interfaces/base';

@Component({
  filters: { FormatMoneyFilter }
})
export default class ClientActivityItemFeesComponent extends Vue {
  @Prop({ required: true }) fees!: Array<TransactionFeeInterface>;

  private columns: Array<GridColumnBaseInterface> = [];

  constructor () {
    super();

    this.columns = [
      { name: 'feeName', label: this.$t('components.client.detail.clientActivityItemFees.feeTypeName').toString(), align: 'left' },
      { name: 'feeAmount', label: this.$t('components.client.detail.clientActivityItemFees.feeAmount').toString(), align: 'right', headerStyle: 'width:150px;' }
    ];
  }
}
</script>

<style scoped>
  .text-gray {
    font-size: 12px;
    color: #7D7D80;
  }

  .visual-table {
    width: 450px;
  }

  .visual-table >>> th,
  .visual-table >>> td.q-td {
    font-size: 14px !important;
    line-height: 20px !important;
  }

</style>
