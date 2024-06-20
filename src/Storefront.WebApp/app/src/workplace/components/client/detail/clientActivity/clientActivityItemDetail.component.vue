<template>
  <div class="detail__table row q-gutter-y-sm">
    <template v-if="showField('dateTime')">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.dateTime') }}</div>
      <div class="col-7 ellipsis">{{ detail.transactionDate | FormatDateFilter(formatting.dateTimeVue) }}</div>
    </template>
    <template v-if="showField('merchantName') && detail.getFullMerchantName">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.merchant') }}</div>
      <div class="col-7 ellipsis">{{ detail.getFullMerchantName }}</div>
    </template>
    <template v-if="showField('mcc') && detail.mcc">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.mcc') }}</div>
      <div class="col-7 ellipsis">{{ detail.mcc }}</div>
    </template>
    <template v-if="showField('terminalId') && detail.terminalId">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.terminalId') }}</div>
      <div class="col-7 ellipsis">{{ detail.terminalId }}</div>
    </template>
    <template v-if="showField('billingAmount')">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.accountAmount') }}</div>
      <div class="col-7 ellipsis">{{ Math.abs(detail.billingAmount) | FormatMoneyFilter}} <span class="text-gray">{{ detail.billingCurrency }}</span></div>
    </template>
    <template v-if="showField('fees')">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.commission') }}</div>
      <div class="col-7 row items-center">
        <div class="col-auto q-mr-sm">{{ Math.abs(detail.feeAmount) | FormatMoneyFilter}} <span class="text-gray">{{ detail.feeCurrency }}</span></div>
        <client-activity-item-fees-component v-if="detail.fees != null && detail.fees.length > 0" :fees="detail.fees"  class="col-auto fees" />
      </div>
    </template>
    <template v-if="showField('status')">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.status') }}</div>
      <div class="col-7 ellipsis">{{ detail.txnStatus }}</div>
    </template>
    <template v-if="showField('responseCode')">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.responseCode') }}</div>
      <div class="col-7 ellipsis">{{ detail.responseCode }}</div>
    </template>
    <template v-if="showField('authCode') && detail.authCode">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.authCode') }}</div>
      <div class="col-7 ellipsis">{{ detail.authCode }}</div>
    </template>
    <template v-if="showField('rrn') && detail.rrn">
      <div class="col-5 text-gray">RRN</div>
      <div class="col-7 ellipsis">{{ detail.rrn }}</div>
    </template>
    <template v-if="showField('stan') && false" ><!-- v-if="detail.stan" -->
      <div class="col-5 text-gray">STAN</div>
      <div class="col-7 ellipsis">{{ detail.stan }}</div>
    </template>
    <template v-if="showField('acqInstitutionCode') && detail.acqInstitutionCode">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.acqInstitutionCode') }}</div>
      <div class="col-7 ellipsis">{{ detail.acqInstitutionCode }}</div>
    </template>
    <template v-if="showField('terminalType') && detail.terminalType">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.terminalType') }}</div>
      <div class="col-7 ellipsis">{{ detail.terminalType }}</div>
    </template>
    <template v-if="showField('cardDataInputMode') && detail.cardDataInputMode">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.cardDataInputMode') }}</div>
      <div class="col-7 ellipsis">{{ detail.cardDataInputMode }}</div>
    </template>
    <template v-if="showField('pinVerification') && detail.pinVerification">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.pinVerification') }}</div>
      <div class="col-7 ellipsis">{{ detail.pinVerification }}</div>
    </template>
    <template v-if="showField('cvv2Verification') && detail.cvv2Verification">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.cvv2Verification') }}</div>
      <div class="col-7 ellipsis">{{ detail.cvv2Verification }}</div>
    </template>
    <template v-if="showField('cavvValidation') && detail.cavvValidation">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.cavvValidation') }}</div>
      <div class="col-7 ellipsis">{{ detail.cavvValidation }}</div>
    </template>
    <template v-if="showField('info')">
      <div class="col-5 text-gray">{{ $t('components.client.detail.clientActivity.info') }}</div>
      <div class="col-7 ellipsis">{{ detail.getTransactionInfo }}</div>
    </template>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import { TransactionInterface } from '../../../../interfaces/transaction';
import constants from '../../../../common/constants';
import { FormatMoneyFilter, FormatDateFilter } from '../../../../filters';
import ClientActivityItemFeesComponent from './clientActivityItemFees.component.vue';

@Component({
  components: { ClientActivityItemFeesComponent },
  filters: { FormatMoneyFilter, FormatDateFilter }
})
export default class ClientActivityItemDetailComponent extends Vue {
  @Prop({ required: true }) detail!: TransactionInterface;
  @Prop() fields?: Array<string>;

  private formatting:any;

  constructor () {
    super();
    this.formatting = constants.formatting;
  }

  private showField (fieldName: string) {
    if (!this.fields) return true;
    return !!(this.fields.includes(fieldName));
  }
}
</script>

<style scoped>
  .detail__table {
    font-size: 12px;
  }
  .detail__table > .col-7 {
    padding-left: 8px;
  }

  .ellipsis:hover {
    white-space: normal;
  }

  .text-gray {
    font-size: 12px;
    color: #7D7D80;
  }

  .fees {
    position: relative;
    top:-2px;
  }

</style>
