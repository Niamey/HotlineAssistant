<template>
  <block-spinner-component v-if="isLoading" />
  <no-data-component v-else-if="!fullBalance" />
  <error-data-component v-else-if="hasError" />
  <div v-else>
    <div class="row ">
      <div class="col q-mx-lg">
        <div class="row tbl text-body2">
        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.available') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.available | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.own') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.own | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.blocked') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.blocked | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.loan') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.loan | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.overlimit') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.overlimit | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.overdue') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.overdue | FormatMoneyFilter }} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.creditLimit') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.creditLimit | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.finBlocking') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.finBlocking | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>
        </div>
      </div>
      <div class="vl"></div>
      <div class="col q-mx-xl">
        <div class="row tbl text-body2">
        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.interests') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.interests | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.penalty') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.penalty | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.minimalPayment') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.minimalPayment | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.mandatoryPayment') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.mandatoryPayment | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.unusedCreditLimit') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.unusedCreditLimit | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.debt') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.debt | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.fullOwn') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.fullOwn | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>

        <div class="col-7 q-mb-sm">{{ $t('components.client.detail.cards.cardFullBalance.fee') }}</div>
        <div class="col-5 q-mb-sm text-right">{{fullBalance.fee | FormatMoneyFilter}} <small class="currency">{{this.fullBalance.currency}}</small></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';

import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import { FormatCardNumberFilter, FormatMoneyFilter } from '../../../../filters';

import CardFullBalanceMixin from '../../../../mixins/card/balance/full/cardFullBalance.mixin';
import ApiResultModel from '../../../../models/api/apiResult.model';
import { Mixins } from 'vue-mixin-decorator';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent },
  filters: { FormatCardNumberFilter, FormatMoneyFilter }
})
export default class CardFullBalanceComponent extends Mixins(CardFullBalanceMixin) {
  @Prop() cardId!: number;

  private fullBalance: any;
  private isLoading: boolean;
  private hasError: boolean;

  constructor () {
    super();
    this.fullBalance = null;
    this.isLoading = false;
    this.hasError = false;
  }

  async mounted () {
    this.isLoading = true;
    const result = await this.loadCardFullBalance(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.cardId });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      this.fullBalance = result.result?.item;
    }
    this.isLoading = false;
  }
}
</script>

<style scoped>
  .vl {
    border-left: 1px solid #7D7D80;
    height: 90%;
    opacity: 0.2;
    position: absolute;
    left: 50%;
  }
  .currency {
    color: #7D7D80;
  }
  .text-title {
    font-size:18px;
  }

  .tbl {
    margin-top: 5px;
  }

  .tbl > div:nth-child(2n+1) {
    color: #7D7D80
  }
</style>
