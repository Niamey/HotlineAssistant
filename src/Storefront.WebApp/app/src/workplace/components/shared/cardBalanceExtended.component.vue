<template>
  <div>
    <div class="row items-center ">
      <q-toggle
            class="text-subtitle1 text-weight-medium"
            v-model="showBalance"
            @input="doBalanceRequest()"
            left-label
            :label="$t('components.client.detail.cards.clientCardDetail.balances')"
          />
    </div>
    <template v-if="showBalance">
      <q-spinner v-if="isLoading" :size="sizeSpinner" color="primary"/>
      <error-data-small-component v-else-if="hasError"/>
      <no-data-small-component v-else-if="!this.balanceExtended"/>
      <template v-else>
        <div class="row tbl text-body2">
          <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.available')}}</div>
          <div class="col-7 q-mb-sm"
            v-if="this.balanceExtended &&
            this.balanceExtended.available ||
            this.balanceExtended.available == 0"
          >
            <span v-if="this.balanceExtended.available <= 0" class="cursor-pointer link" @click="navigateToDebt">{{ getBalance }}</span>
            <span v-else>{{ getBalance }}</span>
            <small class="currency q-ml-xs">{{this.balanceExtended.currency}}</small>
          </div>
          <no-data-small-component v-else />
          <template v-if="getMinimumBalance">
            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.minimumBalance')}}</div>
            <div class="col-7 q-mb-sm">
              <span>{{ getMinimumBalance }}</span>
              <small class="currency q-ml-xs">{{this.balanceExtended.currency}}</small>
            </div>
          </template>
          <template v-if="this.balanceExtended && this.balanceExtended.hasCreditLimit">
            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.creditLimit')}}</div>
            <div class="col-7 q-mb-sm" >{{getCreditLimit}}<small class="currency q-ml-xs">{{this.balanceExtended.currency}}</small></div>
            <div class="col-5 q-mb-sm">{{ $t('components.client.detail.cards.clientCardDetail.usedCreditLimit')}}</div>
            <div v-if="this.balanceExtended.usedCreditLimit < 0" class="col-7 q-mb-sm cursor-pointer link" @click="navigateToDebt">{{getUsedCreditLimit}}<small class="currency q-ml-xs">{{this.balanceExtended.currency}}</small></div>
            <div v-else>{{getUsedCreditLimit}}<small class="currency q-ml-xs">{{this.balanceExtended.currency}}</small></div>
          </template>
        </div>
      </template>
    </template>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import { Store } from 'vuex';
import { FormatCardNumberFilter, FormatMoneyFilter } from '../../filters';
import { CardBalanceRequestInterface } from '../../interfaces/requests/card/balance';
import { CardBalanceExtendedResponseInterface } from '../../interfaces/responses/card/balance/extended';
import { cardApi } from '../../api/card.api';
import { ApiWrapperActionHelper } from '../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../models/api/apiResult.model';
import ErrorDataSmallComponent from './errorDataSmall.component.vue';
import NoDataSmallComponent from './noDataSmall.component.vue';

import { NavigateQueryHelper } from '../../helpers/navigateQuery.helper';
import RouteHelper from '../../mixins/routing/router.mixin';
import constants from '../../common/constants';
import { ClientCardTabsEnum } from '../../enums/client/clientCardTabs.enum';

@Component({
  components: { ErrorDataSmallComponent, NoDataSmallComponent },
  filters: { FormatCardNumberFilter, FormatMoneyFilter }
})
export default class CardBalanceExtendedComponent extends RouteHelper {
  @Prop() cardId!: number;
  @Prop({ default: 'sm' }) sizeSpinner?: string;

  private showBalance: boolean;
  private balanceExtended: any;
  private isLoading: boolean;
  private hasError: boolean;

  constructor () {
    super();
    this.showBalance = false;
    this.balanceExtended = null;
    this.isLoading = false;
    this.hasError = false;
  }

  get getBalance () {
    if (this.balanceExtended?.available === undefined) return undefined;
    const available = FormatMoneyFilter(this.balanceExtended.available);
    return `${available}`;
  }

  get getCreditLimit () {
    if (this.balanceExtended?.creditLimit) {
      const creditLimit = FormatMoneyFilter(this.balanceExtended.creditLimit);
      return `${creditLimit}`;
    } else {
      return '0.00';
    }
  }

  get getUsedCreditLimit () {
    if (this.balanceExtended?.usedCreditLimit) {
      const usedCreditLimit = FormatMoneyFilter(this.balanceExtended.usedCreditLimit);
      return `${usedCreditLimit}`;
    } else {
      return '0.00';
    }
  }

  get getMinimumBalance () {
    if (this.balanceExtended?.minimumBalance) {
      const minimumBalance = FormatMoneyFilter(this.balanceExtended.minimumBalance);
      return `${minimumBalance}`;
    } else {
      return null;
    }
  }

  private async navigateToDebt () {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id),
      {
        [constants.navigateQuery.card.cardId]: NavigateQueryHelper.cards.getCardId(this.$route)?.toString(),
        [constants.navigateQuery.account.accountId]: NavigateQueryHelper.accounts.getAccountId(this.$route)?.toString(),
        [constants.navigateQuery.agreement.agreementId]: NavigateQueryHelper.agreements.getAgreementId(this.$route)?.toString(),
        [constants.navigateQuery.card.tabName]: ClientCardTabsEnum.Debt
      });
  }

  async loadCardBalanceExtended (store: Store<any>, params:CardBalanceRequestInterface) : Promise<void | ApiResultModel<CardBalanceExtendedResponseInterface>> {
    const promise = cardApi.getBalanceExtendedAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardBalanceExtendedResponseInterface>(store, promise);
  }

  async doBalanceRequest () {
    if (this.showBalance) {
      this.balanceExtended = null;
      this.isLoading = true;
      const result = await this.loadCardBalanceExtended(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.cardId });
      if (result instanceof ApiResultModel) {
        this.hasError = !!result.hasError;
        this.balanceExtended = result.result?.item;
      }
      this.isLoading = false;
    }
  }
}
</script>

<style scoped>
  .currency,
  .tbl > div:nth-child(2n+1),
  .gray {
    color: #7D7D80;
  }

  .link {
    color: #0078D4;
  }
</style>
