<template>
  <block-spinner-component v-if="this.showLoading" class="full-width row flex-center" :loaded="this.showLoading" />
  <error-data-component v-else-if="showError" style="min-height: 145px;" class="full-width row flex-center" />
  <no-data-component v-else-if="showNoData" style="min-height: 145px;" class="full-width row flex-center" />
  <div v-else-if="showData && localData">
    <div class="row tbl text-subtitle1">
      <div class="col-3 q-mb-sm text-grey-7">{{$t('components.client.detail.cards.clientCardServices.status')}}:</div>
      <div class="col-9 q-mb-sm text-light-blue-14 cursor-pointer">
        <secure-3-d-status-component :status="detailData.status">{{ getStatus }}</secure-3-d-status-component>
      </div>

      <div v-if="isActive" class="col-3 q-mb-sm text-grey-7">{{$t('components.client.detail.cards.clientCardServices.phone')}}:</div>
      <div v-if="isActive" class="col-9 q-mb-sm row items-center">
        <div :class="{'col-auto':!localData.isFinancePhone}">{{localData.phone}}</div>
        <div v-if="!localData.isFinancePhone" class="col-auto q-ml-sm text-grey-7 text-caption">({{$t('components.client.detail.cards.clientCardServices.notFinPhone')}})</div>
      </div>

      <div class="col-3 q-mb-sm text-grey-7">{{$t('components.client.detail.cards.clientCardServices.tariff')}}:</div>
      <div class="col-9 q-mb-sm">{{localData.tariff}}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';
import BaseDetailComponent from '../../../../base/baseDetail.component.vue';
import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { Card3DSecureStatusEnum } from '../../../../../enums/card/services/card3DSecureStatus.enum';
import Secure3DStatusComponent from '../../../../client/detail/cards/services/audit/secure3DStatus.component.vue';

@Component({
  components: { BlockSpinnerComponent, ErrorDataComponent, NoDataComponent, Secure3DStatusComponent }
})
export default class Card3DSecureComponent extends BaseDetailComponent {
  @Prop({ required: true }) declare detailData: any;
  private localData: any;

  constructor () {
    super();
    this.localData = Object.assign({}, this.detailData);
  }

  @Watch('detailData', { immediate: true })
  private onUpdated () {
    this.localData = Object.assign({}, this.detailData);
  }

  get getStatus () {
    if (!this.localData) return '';
    return this.isActive
      ? this.$t('enums.card3DSecureStatusEnum.active')
      : this.$t('enums.card3DSecureStatusEnum.inactive');
  }

  get isActive () {
    return !!(this.localData?.status === Card3DSecureStatusEnum.Active);
  }
}
</script>

<style scoped>
</style>
