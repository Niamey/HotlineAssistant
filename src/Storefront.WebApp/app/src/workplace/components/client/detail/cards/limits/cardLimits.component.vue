<template>
    <block-spinner-component class="full-width row flex-center" style="height: 450px;" v-if="isLoading" />
    <error-data-component  class="full-width row flex-center" style="height: 450px;" v-else-if="hasError" />
    <no-data-component class="full-width row flex-center" style="height: 450px;" v-else-if="!hasError && cardLimits.length == 0" />
  <div v-else>
    <div class="text-h6 text-primary q-my-lg">{{ $t('components.client.detail.cards.limits.cardLimits.limits') }}</div>
    <div class="row">
      <div class="col q-pr-lg">
        <div class="text-body1 text-weight-medium">{{ $t('components.client.detail.cards.limits.cardLimits.operations') }}</div>

        <div class="text-weight-medium q-mt-sm">{{ $t('components.client.detail.cards.limits.cardLimits.internalOperations') }}</div>
          <card-limit-detail-component :parentClasses="['blured']" :detailLimit="cardLimits.internalTransactions" />
        <div class="text-weight-medium q-mt-md">{{ $t('components.client.detail.cards.limits.cardLimits.transferOperations') }}</div>
          <card-limit-detail-component :parentClasses="['blured']" :detailLimit="cardLimits.transferTransactions" />
      </div>
      <div class="col q-pr-lg">
        <div class="text-weight-medium text-body1">{{ $t('components.client.detail.cards.limits.cardLimits.cardLimits') }}</div>
        <q-toggle
            v-model="cardLimits.limitOfCashWithdrawal"
            @input="setLimitStatus()"
            left-label
            :label="$t('components.client.detail.cards.limits.cardLimits.cashWithdrawalLimit')"
        />
        <div class="row q-mt-sm items-center">
          <div class="col text-weight-medium">{{ $t('components.client.detail.cards.limits.cardLimits.systemLimit') }}</div>
          <q-btn class="col-auto" flat color="primary" label="Змінити" @click="show" no-caps/>
        </div>
          <div class="text-grey-7 q-mt-sm q-pr-xl">{{ $t('components.client.detail.cards.limits.cardLimits.riskyLimit') }}</div>
          <!--<div class="row blured">
            <div class="col-auto q-mr-sm">{{this.cardLimits.systemLimit.limits[0].limit | FormatMoneyFilter}}<small class="text-grey-7">{{this.cardLimits.systemLimit.limits[0].currencyName}}</small></div>
          </div>-->

        <div class="text-weight-medium text-body1 q-mt-lg">{{ $t('components.client.detail.cards.limits.cardLimits.products') }}</div>
        <div class="text-weight-medium q-mt-sm" v-if="this.detailData.isVirtual">{{ $t('components.client.detail.cards.limits.cardLimits.virtual') }}</div>
          <card-limit-detail-component :detailLimit="cardLimits.products" />
      </div>
    </div>
    <card-change-system-limit-component :isShow="isShowLimitDialog" :systemLimit="cardLimits.systemLimit" :detailData="detailData" @hide="hideLimitDialog"/>
  </div>
</template>

<script lang="ts">
import { cardApi } from '../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Vue, Component, Prop } from 'vue-property-decorator';
import CardChangeSystemLimitComponent from './cardChangeSystemLimit.component.vue';
import CardBlockingProcessComponent from '../../limits/cardBlockingProcess.component.vue';
import { Store } from 'vuex';
import { CardLimitOfCashWithdrawalRequestInterface, CardLimitStatusRequestInterface } from '../../../../../interfaces/requests/card/limits';
import { CardLimitOfCashWithdrawalResponseInterface, CardLimitStatusResponseInterface } from '../../../../../interfaces/responses/card/limits';
import { FormatMoneyFilter, FormatStringUnitsFilter } from '../../../../../filters';
import { CardInterface } from '../../../../../interfaces/card';
import { CardLimitOfCashWithdrawalModel } from '../../../../../models/card/limits';

import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import CardLimitDetailComponent from './cardLimitDetail.component.vue';

@Component({
  components: { CardChangeSystemLimitComponent, CardBlockingProcessComponent, NoDataComponent, BlockSpinnerComponent, ErrorDataComponent, CardLimitDetailComponent },
  filters: { FormatMoneyFilter, FormatStringUnitsFilter }
})
export default class CardLimitsComponent extends Vue {
  @Prop({ required: true }) detailData!: CardInterface;
  private isShowLimitDialog: boolean;
  private isLoading: boolean;
  private hasError: boolean;
  private cardLimits: CardLimitOfCashWithdrawalModel;

  constructor () {
    super();
    this.isShowLimitDialog = false;
    this.isLoading = false;
    this.hasError = false;
    this.cardLimits = {};
  }

  show () {
    this.isShowLimitDialog = true;
  }

  hideLimitDialog () {
    this.isShowLimitDialog = false;
  }

  async created () {
    await this.doRequest();
  }

  private limitOfCashWithdrawalToggleChange () {
    this.cardLimits.limitOfCashWithdrawal = !this.cardLimits.limitOfCashWithdrawal;
  }

  protected async setLimitStatus () {
    const result = await this.setCardLimitStatusAsync(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.detailData.cardId, turnOn: this.cardLimits.limitOfCashWithdrawal });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      if (!this.hasError) {
        this.$q.notify({
          type: result.result?.item.success ? 'positive' : 'negative',
          message: result.result?.item.message,
          position: 'top'
        });
        if (!result.result?.item.success) {
          this.limitOfCashWithdrawalToggleChange();
        }
      } else {
        this.$q.notify({
          type: 'negative',
          message: this.$t('components.client.detail.cards.limits.cardLimits.unableToSetStatus').toString(),
          position: 'top'
        });
        this.limitOfCashWithdrawalToggleChange();
      }
    }
  }

  protected async doRequest () {
    this.isLoading = true;
    const result = await this.loadCardLimits(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.detailData.cardId });
    // @ts-ignore
    this.hasError = result.hasError;
    if (!this.hasError) {
      // @ts-ignore
      this.cardLimits = result?.result.item;
    }
    this.isLoading = false;
  }

  private async setCardLimitStatusAsync (store: Store<any>, params:CardLimitStatusRequestInterface) : Promise<void | ApiResultModel<CardLimitStatusResponseInterface>> {
    const promise = cardApi.setCardLimitOfCashWithdrawalAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardLimitStatusResponseInterface>(store, promise);
  }

  private async loadCardLimits (store: Store<any>, params: CardLimitOfCashWithdrawalRequestInterface) : Promise<void | ApiResultModel<CardLimitOfCashWithdrawalResponseInterface>> {
    const promise = cardApi.getCardLimitOfCashWithdrawalAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardLimitOfCashWithdrawalResponseInterface>(store, promise);
  }
}
</script>

<style scoped>
.chip {
  background-color: #DFF6DD;
  color: #107C10;
  margin: 0;
  padding: 2px 8px;
  height: 1.5em;
}
small {
  padding-left: 4px;
}
.blured {
  filter: blur(3px);
}
</style>
