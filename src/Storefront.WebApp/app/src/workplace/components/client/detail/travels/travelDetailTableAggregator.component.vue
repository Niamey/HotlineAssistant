<template>
  <q-card class="q-mb-md">
        <client-travel-info-component v-if="detailShow"
                                           :detailData="this.TravelDetail"
                                           :hasError="this.TravelDetailHasError"
                                           :isLoading="this.TravelDetailLoading" />
        <client-travel-list-component v-if="!detailShow"
                                           :listData="this.Travels" :page-size="pageSize" :hasError="this.TravelsHasError" :isLoading="this.TravelsLoading" />
  </q-card>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';

import TravelListMixin from '../../../../mixins/travel/list/travelList.mixin';
import TravelDetailMixin from '../../../../mixins/travel/detail/travelDetail.mixin';

import ClientTravelListComponent from './clientTravelList.component.vue';
import ClientTravelInfoComponent from './clientTravelInfo.component.vue';
import constants from '../../../../common/constants';

import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';

@Component({
  components: { ClientTravelListComponent, ClientTravelInfoComponent }
})
export default class TravelDetailTableAggregatorComponent extends mixins(TravelListMixin, TravelDetailMixin) {
  private pageSize: number = constants.paging.pageSize;

  created () {
    // eslint-disable-next-line @typescript-eslint/unbound-method
    this.$bus.on('travel-aggregator-detail-update', this.onTravelDetailUpdate);

    // eslint-disable-next-line @typescript-eslint/unbound-method
    this.$bus.on('travel-aggregator-list-update', () => {
      void (async () => {
        await this.onTravelListUpdate();
      })();
    });
  }

  beforeDestroy () {
    this.$bus.off('travel-aggregator-detail-update');
    this.$bus.off('travel-aggregator-list-update');
  }

  get detailShow () {
    return NavigateQueryHelper.travels.hasTravelId(this.$route);
  }
}
</script>

<style scoped>

</style>
