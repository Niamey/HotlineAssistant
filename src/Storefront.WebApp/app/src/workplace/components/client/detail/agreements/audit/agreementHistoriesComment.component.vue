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
        <q-item-label class="text-subtitle1" style="font-weight: 400">{{checkData(history.comment)}}</q-item-label>
        <q-item-label class="text-subtitle1">{{ getStatus(history.status)}}
          <span v-if="history.isFutureStatus" class="text-subtitle2">({{getLowerFutureStatusTitle}})</span>
          <span v-else-if="index == getCurrentStatusIndex" class="text-subtitle2">({{getLowerStatusTitle}})</span>
          </q-item-label>
      </q-item-section>
    </q-item>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import { AgreementStatusHistoryInterface } from '../../../../../interfaces/agreement/status';
import AgreementStatusHistoryComponent from './agreementStatusHistory.component.vue';
import { AgreementStatusEnumHelper } from '../../../../../helpers/enumHelpers/agreementStatusEnum.helper';
import { AgreementStatusEnum } from '../../../../../enums/agreement/agreementStatus.enum';
import constants from '../../../../../common/constants';
import { FormatDateFilter } from '../../../../../filters/formatDate.filter';

@Component({
  components: { AgreementStatusHistoryComponent },
  filters: { FormatDateFilter, AgreementStatusEnumHelper }
})
export default class AgreementHistoriesCommentComponent extends Vue {
    @Prop({ required: true }) detailData!: AgreementStatusHistoryInterface;

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

  getStatus (status: AgreementStatusEnum) {
    return AgreementStatusEnumHelper.getStatusName(status);
  }

  get getCurrentStatusIndex () {
    return this.detailData.histories?.findIndex(x => x.isFutureStatus === false);
  }

  get getLowerFutureStatusTitle () {
    return this.$t('components.client.detail.agreements.audit.agreementStatusHistory.futureStatus').toString().toLowerCase();
  }

  get getLowerStatusTitle () {
    return this.$t('components.client.detail.agreements.audit.agreementStatusHistory.status').toString().toLowerCase();
  }
}
</script>

<style scoped>
  .title {
  font-size: 16px;
  color: #26272B;
}
.text-subtitle2{
  font-size: 12px;
  color: #7D7D80;
}
.text-subtitle1{
  font-size: 14px;
  font-weight: 500;
  color:#26272B;
}
</style>
