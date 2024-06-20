<template>
  <div>
    <div v-if="isShowEye" class="row items-center">
      <span class="blured">00 000 000</span>
      <q-icon class="gray on-right cursor-pointer" :size="sizeIcon" name="visibility" @click="doBalanceRequest"/>
    </div>
    <template v-else>
      <q-spinner v-if="isLoading" :size="sizeSpinner" color="primary" />
      <error-data-small-component v-else-if="hasError" />
      <no-data-small-component v-else-if="!getBalance" />
      <template v-else>
        <span v-if="this.balance.available <= 0" class="cursor-pointer link" @click="navigateToDebt">{{ getBalance }}</span>
        <span v-else>{{ getBalance }}</span>
        <small class="gray q-ml-xs">{{ this.balance.currency }}</small>
      </template>
    </template>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import { FormatCardNumberFilter, FormatMoneyFilter } from '../../filters';
import { mixins } from 'vue-class-component';
import CardBalanceMixin from '../../mixins/card/balance/cardBalance.mixin';
import ErrorDataSmallComponent from './errorDataSmall.component.vue';
import NoDataSmallComponent from './noDataSmall.component.vue';

import { NavigateQueryHelper } from '../../helpers/navigateQuery.helper';
import RouteHelper from '../../mixins/routing/router.mixin';
import constants from '../../common/constants';
import { ClientCardTabsEnum } from '../../enums/client/clientCardTabs.enum';
import ApiResultModel from '../../models/api/apiResult.model';

@Component({
  components: { ErrorDataSmallComponent, NoDataSmallComponent },
  filters: { FormatCardNumberFilter, FormatMoneyFilter }
})
export default class CardBalanceComponent extends mixins(CardBalanceMixin, RouteHelper) {
  @Prop() cardId!: number;
  @Prop({ default: '1.5em' }) sizeIcon?: string;
  @Prop({ default: 'sm' }) sizeSpinner?: string;

  private isShowEye: boolean;
  private balance: any;
  private isLoading: boolean;
  private hasError: boolean;

  constructor () {
    super();
    this.isShowEye = true;
    this.balance = null;
    this.isLoading = false;
    this.hasError = false;
  }

  get getBalance () {
    if (this.balance?.available === undefined) return undefined;

    const available = FormatMoneyFilter(this.balance.available);
    return `${available}`;
  }

  async doBalanceRequest () {
    this.isShowEye = false;
    this.isLoading = true;
    const result = await this.loadCardBalance(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.cardId });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      this.balance = result.result?.item;
    }
    this.isLoading = false;
  }

  private async navigateToDebt () {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id),
      {
        [constants.navigateQuery.card.cardId]: this.cardId.toString(),
        [constants.navigateQuery.account.accountId]: NavigateQueryHelper.accounts.getAccountId(this.$route)?.toString(),
        [constants.navigateQuery.agreement.agreementId]: NavigateQueryHelper.agreements.getAgreementId(this.$route)?.toString(),
        [constants.navigateQuery.card.tabName]: ClientCardTabsEnum.Debt
      });
  }
}
</script>

<style scoped>
  .gray {
    color: #7D7D80
  }
  .blured {
    filter: blur(3px);
  }
  .link {
    color: #0078D4;
  }
</style>
