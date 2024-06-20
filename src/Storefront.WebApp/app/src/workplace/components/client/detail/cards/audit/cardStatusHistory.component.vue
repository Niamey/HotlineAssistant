<template>
  <q-card style="width: 364px">
    <q-card-section class="row items-center q-pb-none">
      <div class="title text-weight-bold">{{ $t('components.client.detail.cards.audit.cardStatusHistory.title') }}</div>
      <q-space />
      <q-btn icon="close" flat round dense v-close-popup style="color: #A8A9AA" />
    </q-card-section>

    <q-card-section class="q-pr-none">
      <block-spinner-component v-if="showLoading" :loaded="showLoading" />
      <no-data-component v-else-if="!iSdetailData" />
      <error-data-component v-else-if="showError" />
      <div class="column q-gutter-y-sm" v-else-if="iSdetailData">
        <div v-if="detailData.dateChangeStatus">
          <div class="text-subtitle2">{{ $t('components.client.detail.cards.audit.cardStatusHistory.emissionDate') }}</div>
          <div class="text-subtitle1">{{ detailData.dateChangeStatus | formatDate(formatting.dateTime) }}</div>
        </div>
        <div>
          <div class="text-subtitle2" >{{ $t('components.client.detail.cards.audit.cardStatusHistory.status') }}</div>
          <div class="text-subtitle1">{{ getStatus }}</div>
        </div>
        <div >
          <q-list>
              <q-item-section>
                <q-item-label class="title">{{ $t('components.client.detail.cards.audit.cardStatusHistory.commentsTitle') }} ({{detailData.histories.length}})</q-item-label>
              </q-item-section>
              <template v-if="detailData.histories.length < 2">
                <card-histories-comment-component :detailData="detailData"/>
              </template>
                <q-scroll-area v-else
                      :thumb-style="thumbStyle" :delay="500"
                      style="height: 200px; max-width: 350px;"
                    >
                    <card-histories-comment-component :detailData="detailData"/>
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
import { CardStatusHistoryInterface } from '../../../../../interfaces/card/status';
import BaseDetailComponent from '../../../../base/baseDetail.component.vue';
import { CardStatusEnumHelper } from '../../../../../helpers/enumHelpers/cardStatusEnum.helper';
import { CardStatusEnum } from '../../../../../enums/card/cardStatus.enum';

import { FormatDateFilter } from '../../../../../filters/formatDate.filter';
import constants from '../../../../../common/constants';
import CardHistoriesCommentComponent from './cardHistoriesComment.component.vue';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent, CardHistoriesCommentComponent },
  filters: { FormatDateFilter }
})
export default class CardStatusHistoryComponent extends BaseDetailComponent {
  @Prop({ required: true }) declare detailData: CardStatusHistoryInterface;
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
    return CardStatusEnumHelper.getStatusName(<CardStatusEnum> this.detailData?.currentStatus);
  }

  get iSdetailData () {
    return this.detailData?.histories;
  }
}
</script>

<style scoped>
.title {
  font-size: 18px;
  color: #26272B;
}
.text-caption{
  color: #7D7D80;
  font-weight: 400;
}
.text-subtitle2{
  color: #7D7D80;
}
.text-subtitle1{
  color:#26272B;
}
</style>
