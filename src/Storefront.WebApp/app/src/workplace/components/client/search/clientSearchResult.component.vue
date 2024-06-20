<template>
  <div class="col">
    <div style="height: 528px;" class="full-width row flex-center" v-if="this.showLoading">
      <block-spinner-component :loaded="this.showLoading" />
    </div>
    <div v-else-if="showError" style="height: 528px;" class="full-width row flex-center" >
      <error-data-component  />
    </div>
    <q-table v-else
             class="visual-table"
             flat
             row-key="fullName"
             :data="listData"
             :columns="columns"
             :hide-bottom="!this.showNoData"
             :hide-header="this.showNoData"
             :rows-per-page-options="[rowsInPage]"
             :selected.sync="selected">
      <template v-slot:body="props">
        <q-tr class="cursor-pointer"
          :props="props"
          @click.exact="toggleRow(props.row)">
          <q-td>
            <span class="text-subtitle1 table-textcolor cursor-pointer">{{ props.row.fullName }}</span>
          </q-td>
          <q-td>
              <span v-if="props.row.segmentationType" class="text-subtitle1">{{ getType(props.row.segmentationType.segmentType)}}</span>
          </q-td>
          <q-td>
              <span class="text-subtitle1">{{ props.row.dateOfBirth }}</span>
          </q-td>
          <q-td>
              <span class="text-subtitle1">{{ props.row.inn }}</span>
          </q-td>
          <!-- <q-td v-for="col in props.cols" :key="col.name" :props="props">
            {{ col.value }}
          </q-td> -->
        </q-tr>
      </template>
      <template v-slot:no-data>
        <div style="height: 528px;" class="full-width row flex-center" >
          <no-data-component />
        </div>
      </template>
    </q-table>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';
import CounterpartModel from '../../../models/client/counterpart.model';
import BlockSpinnerComponent from '../../../components/shared/blockSpinner.component.vue';
import ClientDetailMixin from '../../../mixins/client/clientDetail.mixin';
import NoDataComponent from '../../shared/noData.component.vue';
import ErrorDataComponent from '../../shared/errorData.component.vue';
import BaseListComponent from '../../base/baseList.component.vue';
import { SegmentTypeEnum } from '../../../enums/client/segmentType.enum';
import { SegmentTypeEnumHelper } from '../../../helpers/enumHelpers/segmentTypeEnum.helper';

export interface IColumn {
  name: string,
  align: string,
  label: string,
  field: string
}

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent }
})
export default class ClientSearchResultComponent extends mixins(BaseListComponent, ClientDetailMixin) {
  @Prop({ required: true }) declare listData: Array<CounterpartModel>;
  @Prop() pageSize?: number;
  private columns: Array<IColumn> = [];
  private selected: Array<CounterpartModel> = [];
  private rowsInPage: number;

  constructor () {
    super();
    this.columns = [
      {
        name: 'fullName',
        align: 'left',
        label: this.$t('components.client.clientSearchResult.columns.fullName').toString(),
        field: 'fullName'
      },
      {
        name: 'status',
        align: 'left',
        label: this.$t('components.client.clientSearchResult.columns.status').toString(),
        field: 'segmentationType'
      },
      {
        name: 'birthDate',
        align: 'left',
        label: this.$t('components.client.clientSearchResult.columns.birthDate').toString(),
        field: 'dateOfBirth'
      },
      {
        name: 'inn',
        align: 'left',
        label: this.$t('components.client.clientSearchResult.columns.inn').toString(),
        field: 'inn'
      }
    ];

    this.rowsInPage = this.pageSize || 10;
  }

  getType (type: SegmentTypeEnum) {
    return SegmentTypeEnumHelper.getTypeName(type);
  }

  async toggleRow (row: any) {
    if (row.solarId) {
      this.selected = [row];
      this.onSelectClient(row);
      await this.$router.push({ name: 'clientDetail', params: { id: row.solarId } });
    } else {
      this.$q.notify({
        type: 'warning',
        message: this.$t('components.client.clientSearchResult.noCounterpart').toString(),
        position: 'bottom-right'
      });
    }
  }
}
</script>

<style scoped>
  .visual-table >>> .q-table__bottom--nodata {
    border: none;
  }
</style>
