<template>
  <div class="activity-item  full-width" :class="{ expanded: isExpanded }">
    <div class="header cursor-pointer" @click="toggleDetail">
      <div>
        <span class="cardnum q-mr-sm" v-if="itemData.cardNum">{{ itemData.cardNum | FormatCardNumberFilter }}</span>
        <q-badge aling="middle" :label="getStatusName" class="status-badge q-mr-sm" :class="getStatusBadgeClass" />
        <q-badge aling="middle" :label="$t('components.client.detail.clientActivity.reverse')" class="status-badge q-mr-sm" v-if="isReverse"/>
        <q-badge aling="middle" class="child-badge q-mr-sm" v-if="isChildPresent">
          {{ $tc('components.client.detail.clientActivity.childCount', itemData.childs.length )}}
        </q-badge>
      </div>
      <div class="operation">{{ itemData.transactionTypeName }}:
        <span class="text-weight-medium" :class="operationClass">{{ itemData.transactionAmount | FormatMoneyFilter}}</span>
        <span class="q-ml-xs text-gray">{{ itemData.transactionCurrency }}</span>
      </div>
      <div class="commission">{{ $t('components.client.detail.clientActivity.commission') }}:
        <span class="text-weight-medium">{{ getFeeAmount | FormatMoneyFilter }}</span>
        <span class="q-ml-xs text-gray">{{ itemData.feeCurrency }}</span>
      </div>
      <div v-if="itemData.transactionDetails">{{ itemData.transactionDetails }}</div>

      <!--
      <div class="available">{{ $t('components.client.detail.clientActivity.available') }}:
        <span class="text-weight-medium">{{ itemData.availableAmount | FormatMoneyFilter}}</span>&nbsp;
        <span class="text-gray">{{ itemData.availableCurrency }}</span>
      </div>
      -->

      <div class="control">
        <q-icon class="control__arrow" :name="getArrowImg" size="sm" style="color:#A8A9AA" />
        <span class="control__time text-gray">{{ itemData.transactionDate | FormatDateFilter(formatting.timeVue) }}</span>
      </div>
    </div>
    <q-slide-transition>
      <div class="detail q-pt-sm" v-show="isExpanded">
        <client-activity-item-detail-component :detail="itemData" />

        <q-list class="q-mt-sm list-items" v-if="isChildPresent">
          <q-item dense class="no-padding" v-for="(child, index) in itemData.childs" :key="index">
            <client-activity-sub-item-component :item-data="child" :detailFields="['dateTime', 'status', 'responseCode', 'fees', 'info']"/>
          </q-item>
        </q-list>

        <template v-if="isReverse && isExpanded">
          <div class="q-mt-sm text-weight-medium">{{ $t('components.client.detail.clientActivity.expenseTransaction') }}</div>
          <client-activity-before-reversal-component :txnId="itemData.txnId" />
        </template>

        <!-- <q-btn flat class="q-mt-sm" color="primary" :label="$t('components.client.detail.clientActivity.disputeOperation')" no-caps @click="dispute" /> -->
      </div>
    </q-slide-transition>

  </div>
</template>

<script lang="ts">
import { DirectionClassEnum, TxnStatusEnum, DirectionEnum, ClassEnum } from '../../../../enums/transaction';
import { Vue, Component, Prop } from 'vue-property-decorator';
import constants from '../../../../common/constants';
import { FormatMoneyFilter, FormatDateFilter, FormatCardNumberFilter } from '../../../../filters';

import ClientActivitySubItemComponent from './clientActivitySubItem.component.vue';
import ClientActivityItemDetailComponent from './clientActivityItemDetail.component.vue';
import { TransactionTxnStatusHelper } from '../../../../helpers/enumHelpers/transactionTxnStatusEnum.helper';

import ClientActivityBeforeReversalComponent from './clientActivityBeforeReversal.component.vue';

@Component({
  components: { ClientActivitySubItemComponent, ClientActivityItemDetailComponent, ClientActivityBeforeReversalComponent },
  filters: { FormatMoneyFilter, FormatDateFilter, FormatCardNumberFilter }
})
export default class ClientActivityItemComponent extends Vue {
  @Prop({ required: true }) itemData: any;
  private isExpanded: boolean;
  private formatting: any;

  constructor () {
    super();
    this.isExpanded = false;
    this.formatting = constants.formatting;
  }

  private get isChildPresent () {
    return !!(this.itemData.childs != null && this.itemData.childs.length > 0);
  }

  private get isReverse () {
    return !!(this.itemData.direction === DirectionEnum.Reverse);
  }

  private get getStatus () {
    if (!this.isChildPresent) {
      return this.itemData.txnStatus;
    } else {
      return [...this.itemData.childs].pop()?.txnStatus;
    }
  }

  private get getFeeAmount () {
    if (this.isChildPresent) {
      const finChilds = this.itemData.childs.find((x:any) => x.class === ClassEnum.Financial);
      if (finChilds !== undefined) {
        return Math.abs(finChilds.feeAmount);
      }
    }

    return Math.abs(this.itemData.feeAmount);
  }

  private get getStatusBadgeClass () {
    switch (this.getStatus) {
      case TxnStatusEnum.Finished: return 'status-badge-finished';
      case TxnStatusEnum.Rejected: return 'status-badge-rejected';
      default: return '';
    }
  }

  private get getStatusName () {
    return TransactionTxnStatusHelper.getStatusName(this.getStatus);
  }

  private toggleDetail () {
    this.isExpanded = !this.isExpanded;
  }

  get getArrowImg () {
    return this.isExpanded ? 'keyboard_arrow_up' : 'keyboard_arrow_down';
  }

  get operationClass () {
    switch (<DirectionClassEnum> this.itemData.directionClass) {
      case DirectionClassEnum.Debit: return 'debit';
      case DirectionClassEnum.Credit: return 'credit';
      default: return '';
    }
  }
}
</script>

<style scoped>
  .activity-item {
    padding: 10px 16px;
    position: relative;
    transition: background-color .2s;
  }
  .activity-item .header {
    padding-right: 50px;
  }

  .control {
    position: absolute;
    top: 8px;
    right: 16px;
  }

  .control__arrow {
    display: none;
  }
  .control__time {
    display: block;
  }

  .expanded .control__time,
  .activity-item:hover .control__time {
    display: none;
  }
  .expanded .control__arrow,
  .activity-item:hover .control__arrow {
    display: block;
  }

  .activity-item:hover {
    background-color: #F8F9F9;
    transition: background-color .2s;
  }

  .activity-item.expanded {
    border-left: 2px solid #F47C20;
    border-left-color: var(--q-color-primary);
    background-color: #F8F9F9;
    padding-left: 14px;
  }

  .cardnum {
    color: #0078D4;
  }

  .text-gray {
    font-size: 12px;
    color: #7D7D80;
  }

  .credit {
    color: #107C10;
  }
  .debit {
    color: #D83B01
  }

  .status-badge {
    color: #7D7D80;
    background: #F0F0F0;
  }
  .status-badge-finished {
    color: #107C10;
    background: #DFF6DD;
  }
  .status-badge-rejected {
    color: #D83B01;
    background: #FDE7E9;
  }

  .child-badge {
    color: #0078D4;
    background: #E6F2FB;
    padding-left: 24px;
    position: relative;
  }

  .child-badge::after {
    content: '';
    position: absolute;
    top: 2px;
    bottom: 0;
    left: 6px;
    width: 12px;
    height: 12px;
    background-size: 100%;
    background-image: url('~assets/images/icons/childs.svg');
    background-repeat: no-repeat;
  }
</style>
