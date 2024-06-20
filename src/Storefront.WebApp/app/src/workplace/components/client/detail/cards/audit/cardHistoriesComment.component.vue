<template>
  <div>
    <q-item v-for="(history, index) in detailData.histories" :key="index" class="q-pl-none">
                          <q-item-section top side class="q-pr-sm">
                            <q-icon name="account_circle" color="primary" size="sm"></q-icon>
                          </q-item-section>

                          <q-item-section >
                            <div class="row">
                              <q-item-label class="col-5 text-subtitle1">{{checkData(history.userLogin)}}</q-item-label>
                              <q-item-label class="col-7 text-right text-subtitle2">{{history.modifyDate | formatDate(formatting.dateTime)}}</q-item-label>
                              <q-item-label class="text-subtitle2">{{checkData(history.systemName)}}</q-item-label>
                            </div>
                            <q-item-label class="text-subtitle1">{{checkData(history.comment)}}</q-item-label>
                            <q-item-label class="text-subtitle1">{{getStatus(history.status)}}
                              <span v-if="history.isFutureStatus" class="text-subtitle2">({{getLowerFutureStatusTitle}})</span>
                              <span v-if="index == currentStatusIndex" class="text-subtitle2">({{getLowerStatusTitle}})</span>
                              </q-item-label>
                          </q-item-section>
                        </q-item>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { CardStatusHistoryInterface } from '../../../../../interfaces/card/status';
import { CardStatusEnumHelper } from '../../../../../helpers/enumHelpers/cardStatusEnum.helper';
import { CardStatusEnum } from '../../../../../enums/card/cardStatus.enum';

import { FormatDateFilter } from '../../../../../filters/formatDate.filter';
import constants from '../../../../../common/constants';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent },
  filters: { FormatDateFilter }
})
export default class CardHistoriesCommentComponent extends Vue {
  @Prop({ required: true }) detailData!: CardStatusHistoryInterface;
  private formatting: any;

  constructor () {
    super();
    this.formatting = constants.formatting;
  }

  checkData (val: any) {
    if (val == null || val === undefined) {
      return 'Немає даних';
    } else return val;
  }

  get currentStatusIndex () {
    return this.detailData.histories?.findIndex(x => x.isFutureStatus === false);
  }

  get getLowerFutureStatusTitle () {
    return this.$t('components.client.detail.cards.audit.cardStatusHistory.futureStatus').toString().toLowerCase();
  }

  get getLowerStatusTitle () {
    return this.$t('components.client.detail.cards.audit.cardStatusHistory.status').toString().toLowerCase();
  }

  getStatus (status: CardStatusEnum) {
    return CardStatusEnumHelper.getStatusName(status);
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
  font-size: 12px;
  color: #7D7D80;
}
.text-subtitle1{
  font-size: 14px;
  color:#26272B;
}
</style>
