<template>
  <q-card style="width: 364px">
    <q-card-section class="row items-center q-pb-none">
      <div class="title text-weight-bold">{{ $t('components.client.detail.agreements.audit.agreementStatusHistory.title') }}</div>
      <q-space />
      <q-btn icon="close" flat round dense v-close-popup style="color: #A8A9AA" />
    </q-card-section>

    <q-card-section class="q-pr-xs">
      <block-spinner-component v-if="showLoading" :loaded="showLoading" />
      <no-data-component v-else-if="showNoData" />
      <error-data-component v-else-if="showError" />
      <div class="column q-gutter-y-sm" v-else-if="detailData">
        <div>
          <div class="text-subtitle2">{{ $t('components.client.detail.agreements.audit.agreementStatusHistory.status') }}</div>
          <div class="text-subtitle1">{{ getStatus }}</div>
        </div>
        <div v-if="detailData.dateChangeStatus">
          <div class="text-subtitle2">{{ $t('components.client.detail.agreements.audit.agreementStatusHistory.dateOfChange') }}</div>
          <div class="text-subtitle1">{{ detailData.dateChangeStatus | formatDate(formatting.dateTime) }}</div>
        </div>
        <div >
          <q-list>
              <q-item-section>
                <q-item-label class="title">{{ $t('components.client.detail.agreements.audit.agreementStatusHistory.commentsTitle') }} ({{detailData.histories.length}})</q-item-label>
              </q-item-section>
              <template v-if="detailData.histories.length < 2">
                <agreement-histories-comment-component :detailData="detailData"/>
              </template>
                <q-scroll-area v-else
                      :thumb-style="thumbStyle" :delay="500"
                      style="height: 200px;"
                    >
                      <agreement-histories-comment-component :detailData="detailData"/>
                  </q-scroll-area>
          </q-list>
          </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';

import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { AgreementStatusHistoryInterface } from '../../../../../interfaces/agreement/status';
import BaseDetailComponent from '../../../../base/baseDetail.component.vue';
import { AgreementStatusEnumHelper } from '../../../../../helpers/enumHelpers/agreementStatusEnum.helper';
import { AgreementStatusEnum } from '../../../../../enums/agreement/agreementStatus.enum';
import constants from '../../../../../common/constants';
import AgreementHistoriesCommentComponent from './agreementHistoriesComment.component.vue';

import { FormatDateFilter } from '../../../../../filters/formatDate.filter';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent, AgreementHistoriesCommentComponent },
  filters: { FormatDateFilter }
})
export default class AgreementStatusHistoryComponent extends BaseDetailComponent {
  @Prop({ required: true }) declare detailData: AgreementStatusHistoryInterface;
  private thumbStyle:any = {};
  private formatting: any;

  constructor () {
    super();
    this.formatting = constants.formatting;
    this.thumbStyle = {
      right: '5px',
      borderRadius: '5px',
      backgroundColor: '#7D7D80',
      width: '5px',
      opacity: 0.5
    };
  }

  get getStatus () {
    return AgreementStatusEnumHelper.getStatusName(<AgreementStatusEnum> this.detailData?.currentStatus);
  }
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
