<template>
 <q-card class="q-mb-md">
    <div v-if="isLoading">
      <block-spinner-component :loaded="isLoading" />
    </div>
    <div v-else-if="hasError">
      <error-data-component style="min-height:122px;" />
    </div>
    <q-table v-else
             class="visual-table no-shadow"
             row-key="rowNum"
             :data="Travels"
             :columns="columns"
             :hide-header="listData.length === 0"
             :rows-per-page-options="[pageSize]"
    >
      <template v-slot:top>
        <div class="col-auto q-table__title text-h6">{{ $t('components.client.detail.travels.clientTravelList.title') }}</div>
        <q-space />
        <q-btn :label="$t('components.client.detail.travels.clientTravelList.createNewTravel')" class="float-right col-auto q-px-sm" dense color="primary" no-caps @click="newTravelWizard" />
      </template>
      <template v-slot:body="props">
        <q-tr class="cursor-pointer"
              :props="props"
              @click.exact="toggleSingleRow(props.row)"
              @mouseenter="showDeleteId = props.row.travelId"
              @mouseleave="showDeleteId = -1">
          <q-td v-for="col in props.cols" :key="col.name" :props="props">
            <span v-if="col.name === 'status'" :class="getStatusClass(props.row)">{{ col.value }}</span>
            <template v-else-if="col.name === 'country' && isRiskCountry(props.row)">
              <q-icon name="o_privacy_tip" class="text-red q-mb-xs q-mr-sm" style="font-size: 15px;" />{{ col.value }}
            </template>
            <template v-else-if="col.name === 'deleteIcon' && props.row.status != travelStatusEnum.Deleted && showDeleteId == props.row.travelId">
                <q-icon clickable @click.stop="prepareDeleteTravel(props.row)" v-ripple name="o_delete" class="delete-color q-mb-xs" style="font-size: 20px;" />
            </template>
            <template v-else>{{ col.value }}</template>
          </q-td>
        </q-tr>
      </template>
      <template v-slot:no-data>
        <div class="text-gray q-my-xl text-center" style="width: 100%;">{{$t('components.client.detail.travels.clientTravelList.noData')}}</div>
      </template>
      <template v-slot:bottom>
        <div class="row fit justify-end">
          <pager-component class="pager"
                                 v-bind:loading="TravelsLoading"
                                 v-bind:page-index="pageIndex"
                                 v-bind:page-size="pageSize"
                                 v-bind:total="TravelsCount"
                                 v-bind:is-available-next-page="AvailableNextPage"
                                 v-on:onNavigate="onPagerNavigate" />
        </div>
      </template>
    </q-table>
    <q-dialog v-model="confirmDeleteTravel" persistent>
      <q-card>
        <q-card-section class="row items-center">
          <q-avatar icon="o_delete" color="primary" text-color="white" />
          <span class="q-ml-sm">{{ $t('components.client.detail.travels.clientTravelList.confirmDeleteTravel') }}</span>
        </q-card-section>

        <q-card-actions align="right">
          <q-btn flat :label="$t('components.client.detail.travels.clientTravelList.cancel')" color="primary" v-close-popup />
          <q-btn flat :label="$t('components.client.detail.travels.clientTravelList.delete')" color="primary" @click="deleteTravel" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <client-travel-process-component :isShow="isShowDialog" @hide="hideDialog"/>
  </q-card>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';

import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';

import { GridColumnBaseInterface } from '../../../../interfaces/base';

import { TravelInterface } from '../../../../interfaces/travel';
import { CountryInterface } from '../../../../interfaces/country';
import constants from '../../../../common/constants';
import { TravelStatusEnum } from '../../../../enums/travel/travelStatus.enum';
import { TravelStatusEnumHelper } from '../../../../helpers/enumHelpers/travelStatusEnum.helper';
import { FormatDateFilter } from '../../../../filters';
import ClientTravelProcessComponent from './clientTravelProcess.component.vue';
import { travelApi } from '../../../../api/travel.api';
import { mixins } from 'vue-class-component';
import RouteHelper from '../../../../mixins/routing/router.mixin';
import TravelListMixin from '../../../../mixins/travel/list/travelList.mixin';
import PagerComponent from '../../../../components/shared/pager.component.vue';
import BasePagedListComponent from '../../../base/basePagedList.component.vue';

@Component({
  components: { PagerComponent, BlockSpinnerComponent, ErrorDataComponent, ClientTravelProcessComponent },
  filters: { FormatDateFilter }
})
export default class ClientTravelListComponent extends mixins(RouteHelper, TravelListMixin, BasePagedListComponent) {
  @Prop({ required: true }) declare isLoading: boolean;
  @Prop({ required: true }) declare hasError: boolean;
  @Prop({ required: true }) declare listData: Array<TravelInterface>;
  @Prop({ required: true, default: constants.paging.pageSize }) pageSize!: number;
  private columns: Array<GridColumnBaseInterface> = [];
  private selected: Array<TravelInterface> = [];
  private isShowDialog: boolean;
  private showDeleteId: number;
  private travelToDelete: TravelInterface | null;
  private confirmDeleteTravel: boolean;
  private pageIndex: number;

  constructor () {
    super();
    this.currentPageIndexKey = constants.navigateQuery.travel.pageIndex;
    this.pageIndex = this.getPageIndex(this.currentPageIndexKey);
    this.travelToDelete = null;
    this.confirmDeleteTravel = false;
    this.showDeleteId = -1;
    this.isShowDialog = false;
    this.columns = [
      {
        name: 'rowNum',
        align: 'left',
        label: this.$t('components.client.detail.travels.clientTravelList.columns.travelId').toString(),
        field: 'rowNum',
        format: (row:number) => row.toString().padStart(5, '0')
      },
      {
        name: 'status',
        align: 'left',
        label: this.$t('components.client.detail.travels.clientTravelList.columns.status').toString(),
        field: 'status',
        format: (status: TravelStatusEnum) => TravelStatusEnumHelper.getStatusName(status)
      },
      {
        name: 'period',
        align: 'left',
        label: this.$t('components.client.detail.travels.clientTravelList.columns.period').toString(),
        field: 'startTravel',
        format: (val: string, row: TravelInterface) => {
          if (val === row.endTravel) {
            return `${FormatDateFilter(val, constants.formatting.dateVue)}`;
          } else {
            return `${FormatDateFilter(val, constants.formatting.dateVue)} - ${FormatDateFilter(row.endTravel, constants.formatting.dateVue)}`;
          }
        }
      },
      {
        name: 'country',
        align: 'left',
        label: this.$t('components.client.detail.travels.clientTravelList.columns.country').toString(),
        field: 'countries',
        format: (countries: Array<CountryInterface>) => {
          let result = '';
          countries.forEach((element, index) => {
            if (countries.length - 1 === index) {
              result += `${element.countryName}`;
            } else {
              result += `${element.countryName}, `;
            }
          });
          return result;
        }
      },
      { name: 'deleteIcon', label: '', headerStyle: 'width:60px' }
    ];
  }

  newTravelWizard () {
    this.isShowDialog = !this.isShowDialog;
  }

  hideDialog () {
    this.isShowDialog = false;
  }

  async toggleSingleRow (row: TravelInterface) {
    this.selected = [];
    this.selected.push(row);

    await this.navigateBySolarId('clientDetail', row.solarId as number,
      {
        [constants.navigateQuery.travel.travelId]: row.travelId.toString(),
        [constants.navigateQuery.travel.pageIndex]: this.pageIndex.toString()
      });
  }

  private get travelStatusEnum () {
    return TravelStatusEnum;
  }

  private get showIfMultipleRows () {
    return this.listData.length > 1;
  }

  private getStatusClass (row: TravelInterface) {
    switch (row.status) {
      case TravelStatusEnum.Active:
        return 'green-color';
      case TravelStatusEnum.Deleted:
        return 'red-color';
      case TravelStatusEnum.Error:
        return 'red-color text-weight-bold';
      default:
        return '';
    }
  }

  private isRiskCountry (row: TravelInterface) {
    const riskyCountries = row.countries?.filter(e => e.isRisky);
    if (!riskyCountries) return false;
    return riskyCountries.length !== 0;
  }

  private prepareDeleteTravel (row: TravelInterface) {
    this.travelToDelete = row;
    this.confirmDeleteTravel = true;
  }

  private async deleteTravel () {
    if (this.travelToDelete) {
      const result = await travelApi.deleteAsync(
        {
          solarId: this.travelToDelete.solarId,
          travelId: this.travelToDelete.travelId
        }
      );

      if (result.success) {
        this.travelToDelete.status = TravelStatusEnum.Deleted;
        await this.updateTravelList(this.travelToDelete);
      }

      this.travelToDelete = null;

      this.$q.notify({
        type: result.success ? 'positive' : 'negative',
        message: result.message,
        position: 'top'
      });
    }
  }

  async onPagerNavigate (data: number) {
    this.pageIndex = data;
    await this.getTravelsByPage(data, parseInt(this.$route.params.id));
    this.onChangePage(this.Travels, this.pageIndex);
  }
}
</script>

<style scoped>
.text-gray {
  color: #7D7D80;
  font-size: 18px;
}
.red-color {
  color: #D83B01;
}
.green-color {
  color: #107C10;
}
.delete-color {
  color: #A8A9AA;
}
</style>
