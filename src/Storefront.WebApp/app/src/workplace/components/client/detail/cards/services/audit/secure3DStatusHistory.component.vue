<template>
  <q-card style="width: 364px">
    <q-card-section class="row items-center q-pb-none">
      <div class="title text-weight-bold">{{ $t('components.client.detail.cards.audit.secure3DStatusHistory.title') }}</div>
      <q-space />
      <q-btn icon="close" flat round dense v-close-popup style="color: #A8A9AA" />
    </q-card-section>

    <q-card-section>
      <block-spinner-component v-if="showLoading"
                            v-bind:loaded="showLoading"
                            v-bind:size="'5em'"
                            v-bind:label="$t('components.shared.blockSpinner.label')"/>
      <no-data-component v-else-if="showNoData" />
      <error-data-component v-else-if="showError" />
      <div class="column q-gutter-y-sm" v-else-if="detailData">
        <div>
          <div class="text-subtitle2">{{ $t('components.client.detail.cards.audit.secure3DStatusHistory.date') }}</div>
          <div class="text-subtitle1">{{ detailData.dateChangeStatus | FormatDateFilter }}</div>
        </div>
        <div v-if="detailData.comment">
          <div class="text-subtitle2">{{ $t('components.client.detail.cards.audit.secure3DStatusHistory.comment') }}</div>
          <div class="text-subtitle1">{{ detailData.comment }}</div>
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';

import BlockSpinnerComponent from '../../../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../../shared/errorData.component.vue';
import { CardSecure3DStatusHistoryInterface } from '../../../../../../interfaces/card/status';
import BaseDetailComponent from '../../../../../base/baseDetail.component.vue';

import { FormatDateFilter } from '../../../../../../filters/formatDate.filter';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent },
  filters: { FormatDateFilter }
})
export default class Secure3DStatusHistoryComponent extends BaseDetailComponent {
  @Prop({ required: true }) declare detailData: CardSecure3DStatusHistoryInterface;
}
</script>

<style scoped>
.title {
  font-size: 18px;
  color: #26272B;
}
.text-subtitle2{
  color: #7D7D80;
}
.text-subtitle1{
  color:#26272B;
}
</style>
