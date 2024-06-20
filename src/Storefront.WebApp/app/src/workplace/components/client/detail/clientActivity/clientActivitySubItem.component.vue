<template>
  <div class="activity-subitem full-width" :class="{ expanded: isExpanded }">
    <div class="header cursor-pointer" @click="toggleDetail">
      <div class="operation">{{ itemData.transactionTypeName }}:
        <span class="text-weight-medium" :class="operationClass">{{ itemData.transactionAmount | FormatMoneyFilter}}</span>
        <span class="q-ml-xs text-gray">{{ itemData.transactionCurrency }}</span>
      </div>
      <div v-if="itemData.transactionDetails">{{ itemData.transactionDetails }}</div>

      <div class="control">
        <q-icon class="control__arrow" :name="getArrowImg" size="sm" style="color:#A8A9AA" />
      </div>
    </div>
    <q-slide-transition>
      <div class="detail q-pt-sm" v-show="isExpanded">
        <client-activity-item-detail-component :detail="itemData" :fields="detailFields"/>
      </div>
    </q-slide-transition>

  </div>
</template>

<script lang="ts">
import { DirectionClassEnum } from '../../../../enums/transaction';
import { Vue, Component, Prop } from 'vue-property-decorator';
import constants from '../../../../common/constants';
import { FormatMoneyFilter, FormatDateFilter, FormatCardNumberFilter } from '../../../../filters';
import ClientActivityItemDetailComponent from './clientActivityItemDetail.component.vue';

@Component({
  components: { ClientActivityItemDetailComponent },
  filters: { FormatMoneyFilter, FormatDateFilter, FormatCardNumberFilter }
})
export default class ClientActivitySubItemComponent extends Vue {
  @Prop({ required: true }) itemData: any;
  @Prop() detailFields?: Array<string>;

  private isExpanded: boolean;
  private formatting: any;

  constructor () {
    super();
    this.isExpanded = false;
    this.formatting = constants.formatting;
  }

  private toggleDetail () {
    this.isExpanded = !this.isExpanded;
  }

  get getArrowImg () {
    return this.isExpanded ? 'keyboard_arrow_down' : 'keyboard_arrow_right';
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
  .activity-subitem {
    padding: 8px 16px;
    position: relative;
    transition: background-color .2s;
  }
  .activity-subitem .header {
    padding-left: 18px;
  }

  .control {
    position: absolute;
    top: 6px;
    left: 2px;
  }

  .activity-subitem:hover {
    background-color: #F8F9F9;
    transition: background-color .2s;
  }

  .activity-subitem.expanded {
    background-color: #F8F9F9;
    /*padding-left: 14px;*/
  }

  .cardnum {
    color: #0078D4;
  }

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

  .credit {
    color: #107C10;
  }
  .debit {
    color: #D83B01
  }

</style>
