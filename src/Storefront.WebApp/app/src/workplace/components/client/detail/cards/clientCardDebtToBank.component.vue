<template>
  <block-spinner-component v-if="isLoading" />
  <no-data-component v-else-if="!debtToBankDetail" />
  <error-data-component v-else-if="hasError" />
  <div v-else>
    <div v-if="debtToBankDetail.period === creditPeriodTypeEnum.NotUsed">
      <div class="text-body2 text-weight-medium">{{ $t('components.client.detail.cards.debtToBank.creditLimitNotUsed') }}</div>
    </div>
    <div v-else-if="debtToBankDetail.period === creditPeriodTypeEnum.InGracePeriodFirstMonth || debtToBankDetail.period === creditPeriodTypeEnum.InGracePeriodSecondMonth">
      <div class="text-body2 text-weight-medium">{{ $t('components.client.detail.cards.debtToBank.cardInGracePeriodUntil') }} <span class="txt-orange">{{ getGracePeriodDate }}</span>.</div>
      <div class="text-body2 text-weight-medium">{{ $t('components.client.detail.cards.debtToBank.interestNotAccrued') }}</div>
      <br />
      <div>
        <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.preferentialPayment') }}</div>
        <div class="row">
          <div class="col-auto q-mr-sm">{{ getDebt | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
          <q-chip class="chip" square :label="`${$t('components.client.detail.cards.debtToBank.until')} ${getGracePeriodDate}`" />
        </div>
        <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.mandatoryPayment') }}</div>
        <div class="row">
          <div class="col-auto q-mr-sm">{{ getMandatoryPayment | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
          <q-chip v-if="debtToBankDetail.period === creditPeriodTypeEnum.InGracePeriodSecondMonth && getMandatoryPayment === 0" class="chip green" square :label="$t('components.client.detail.cards.debtToBank.credited')" />
          <q-chip v-else class="chip" square :label="`${$t('components.client.detail.cards.debtToBank.until')} ${formattedDate(debtToBankDetail.mandatoryDate)}`" />
        </div>
      </div>
      <br />
      <div class="text-body2 text-weight-medium">{{ $t('components.client.detail.cards.debtToBank.afterGracePeriodWillBeСredited') }} <span class="txt-orange">{{ debtToBankDetail.debtPercent }}%</span> {{ $t('components.client.detail.cards.debtToBank.perMonth') }}</div>
      <div class="text-body2 text-weight-medium">{{ $t('components.client.detail.cards.debtToBank.forMaxAmountDebtEachDay') }}</div>
      <div class="text-body2 text-weight-medium">{{ $t('components.client.detail.cards.debtToBank.fromFirstDayCurrentDebtPeriod') }}.</div>
    </div>
    <div v-else-if="debtToBankDetail.period === creditPeriodTypeEnum.NotInGracePeriodWithDebt">
        <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.preferentialPayment') }}</div>
        <div class="row">
          <div class="col-auto q-mr-sm">{{ getDebt | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
        </div>
        <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.mandatoryPayment') }}</div>
        <div class="row">
          <div class="col-auto q-mr-sm">{{ getMandatoryPayment | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
          <q-chip v-if="getMandatoryPayment === 0" class="chip green" square :label="$t('components.client.detail.cards.debtToBank.credited')" />
          <q-chip v-else class="chip red" square :label="$t('components.client.detail.cards.debtToBank.notCredited')" />
        </div>
        <template v-if="!debtToBankDetail.isCreditVacancy">
          <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.penalty') }}</div>
          <div class="row">
            <div class="col-auto q-mr-sm">{{ getPenalty | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
          </div>
        </template>
        <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.accruedInterest').toString().replace('{percent}', `${debtToBankDetail.debtPercent}%`) }}</div>
        <div class="row">
          <div class="col-auto q-mr-sm">{{ getFee | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
          <q-chip class="chip" square :label="`${$t('components.client.detail.cards.debtToBank.until')} ${getFirstDayNextMonth}`" />
        </div>
        <div v-if="debtToBankDetail.isCreditVacancy" class="text-body2 text-weight-medium">{{ $t('components.client.detail.cards.debtToBank.creditVacationPeriodInfo') }}</div>
    </div>
    <div v-else-if="debtToBankDetail.period === creditPeriodTypeEnum.NotInGracePeriod">
        <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.forUsingGracePeriodPayment') }}</div>
        <div class="row">
          <div class="col-auto q-mr-sm">{{ getDebt | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
        </div>
        <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.mandatoryPayment') }}</div>
        <div class="row">
          <div class="col-auto q-mr-sm">{{ getMandatoryPayment | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
          <q-chip v-if="getMandatoryPayment === 0" class="chip green" square :label="$t('components.client.detail.cards.debtToBank.credited')" />
          <q-chip v-else class="chip red" square :label="$t('components.client.detail.cards.debtToBank.notCredited')" />
        </div>
        <div class="text-grey-7 q-mt-sm">{{ $t('components.client.detail.cards.debtToBank.debtCalcPercentPerMonth').toString().replace('{percent}', `${debtToBankDetail.debtPercent}%`) }}</div>
        <div class="row">
          <div class="col-auto q-mr-sm">{{ getFee | FormatMoneyFilter }}<small class="text-grey-7">{{ debtToBankDetail.currencyName }}</small></div>
          <q-chip class="chip" square :label="`${$t('components.client.detail.cards.debtToBank.willBeCharged')} ${getFirstDayNextMonth}`" />
        </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Prop, Component } from 'vue-property-decorator';

import { CardDebtToBankInterface } from '../../../../interfaces/card/debtToBank/cardDebtToBank.interface';

import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import { FormatMoneyFilter, FormatDateFilter } from '../../../../filters';
import { CreditPeriodTypeEnum } from '../../../../enums/card/debtToBank/creditPeriodType.enum';
// import constants from '../../../../common/constants';
import { date as QuasarDate } from 'quasar';

import CardDebtToBankDetailMixin from '../../../../mixins/card/detail/cardDebtToBankDetail.mixin';
import ApiResultModel from '../../../../models/api/apiResult.model';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent },
  filters: { FormatMoneyFilter, FormatDateFilter }
})
export default class ClientCardDebtToBankComponent extends CardDebtToBankDetailMixin {
  @Prop({ required: true }) cardId!: number;

  private debtToBankDetail: CardDebtToBankInterface;
  private isLoading: boolean;
  private hasError: boolean;
  private currentDate: Date;
  private months: Array<string>;
  private creditPeriodTypeEnum: any;

  constructor () {
    super();
    this.debtToBankDetail = {};
    this.isLoading = false;
    this.hasError = false;
    this.currentDate = new Date();
    this.creditPeriodTypeEnum = CreditPeriodTypeEnum;
    this.months = Array.from(Array(12).keys()).map(i => {
      return this.$t(`quasar.dateUtil.months[${i}]`).toString();
    });
  }

  get isDebtToBankDetail () {
    return this.debtToBankDetail.period !== undefined;
  }

  get getGracePeriodDate () {
    let date = new Date();
    switch (this.debtToBankDetail.period) {
      case CreditPeriodTypeEnum.InGracePeriodFirstMonth:
        date = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() + 2, 0);
        break;
      case CreditPeriodTypeEnum.InGracePeriodSecondMonth:
        date = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() + 1, 0);
        break;
    }
    return this.formattedDate(date);
  }

  get getDebt () {
    return Math.abs(this.debtToBankDetail?.debt || 0);
  }

  get getMandatoryPayment () {
    return Math.abs(this.debtToBankDetail?.mandatoryPayment || 0);
  }

  get getPenalty () {
    return Math.abs(this.debtToBankDetail?.penalty || 0);
  }

  get getFee () {
    return Math.abs(this.debtToBankDetail?.fee || 0);
  }

  get getFirstDayNextMonth () {
    return this.formattedDate(new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() + 1, 1));
  }

  formattedDate (date: Date) {
    return QuasarDate.formatDate(date, 'DD MMMM YYYY', {
      months: this.months
    });
  }

  async mounted () {
    this.isLoading = true;
    const result = await this.loadCardDebtToBankDetail(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.cardId });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      // @ts-ignore
      this.debtToBankDetail = result.result?.item;
    }
    this.isLoading = false;
  }
}
</script>

<style scoped>
.chip {
  background-color: #F0F0F0;
  color: #7D7D80;
  margin: 0;
  padding: 2px 8px;
  height: 1.5em;
}
.chip.green {
  background-color: #DFF6DD;
  color: #107C10;
}
.chip.red {
  background-color: #FDE7E9;
  color: #D83B01;
}
small {
  padding-left: 4px;
}
.txt-orange {
  color: #f47c20;
}
</style>
