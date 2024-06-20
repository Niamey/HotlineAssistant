<template>
  <block-spinner-component v-if="showLoading" :loaded="showLoading" />
  <no-data-component v-else-if="showNoData" />
  <error-data-component v-else-if="showError" />
  <div v-else>
    <div class="text-subtitle1 text-weight-medium">{{ $t('components.client.detail.agreements.clientAgreementDetail.locksLabel') }}</div>
    <div>
      <span v-if="!detailData.classifiers || detailData.classifiers.length == 0">{{ $t('components.client.detail.agreements.clientAgreementDetail.noLocks') }}</span>
      <template v-else>
        <q-chip v-for="(classifier, index) in detailData.classifiers" square dense :key="index" class="q-mr-sm text-caption chip-locks">
          {{ getClassifierName(classifier.classifierName) }}
        </q-chip>
      </template>
    </div>

    <div class="q-mt-md text-subtitle1 text-weight-medium">{{ $t('components.client.detail.agreements.clientAgreementDetail.label') }}</div>
    <div class="row tbl text-body2">
      <template v-if="!bvrViewType">
        <div class="col-2 q-mb-xs">{{ $t('components.client.detail.agreements.clientAgreementDetail.client') }}</div>
        <div class="col-10 q-mb-xs q-pl-sm">{{detailData.clientName}}</div>
      </template>

      <div class="col-2 q-mb-xs">{{ $t('components.client.detail.agreements.clientAgreementDetail.status.label') }}</div>
      <div class="col-10 q-mb-xs q-pl-sm">
        <agreement-status-component v-if="detailData.agreementId" :agreementId="detailData.agreementId" :status="detailData.status" />
      </div>

      <template v-if="!bvrViewType">
        <div class="col-2 q-mb-xs">{{ $t('components.client.detail.agreements.clientAgreementDetail.type') }}</div>
        <div class="col-10 q-mb-xs q-pl-sm">{{ getAgreementType(detailData.type) }}</div>
      </template>

      <div class="col-2 q-mb-xs">{{ $t('components.client.detail.agreements.clientAgreementDetail.project') }}</div>
      <div class="col-10 q-mb-xs q-pl-sm" >{{detailData.productName}}</div>

      <template v-if="!bvrViewType">
        <div class="col-2">{{ $t('components.client.detail.agreements.clientAgreementDetail.bonusProgram') }}</div>
        <div class="col-10 q-pl-sm">{{detailData.bonus}}</div>
      </template>
      <div class="col-2 q-mb-xs">{{ $t('components.client.detail.agreements.clientAgreementDetail.debt') }}</div>
      <div class="col-10 q-mb-xs q-pl-sm">
          <q-spinner v-if="isBalanceLoading" color="primary" />
          <error-data-small-component v-else-if="balanceHasError" />
          <no-data-small-component v-else-if="!balance" />
          <div v-else-if="balance" >
              <span v-if="this.balance.usedCreditLimit != 0" class="cursor-pointer link" @click="navigateToDebt">{{ getBalance }}<small class="text-grey-7">{{ balance.currency }}</small></span>
              <span v-else>{{ getBalance }}<small class="text-grey-7">{{ balance.currency }}</small></span>
          </div>
      </div>
    </div>

    <div class="q-mt-sm row items-center cursor-pointer" @click="navigateToStatement">
      <div class="col-auto q-pr-md">
        <q-img
          src="../../../../../assets/images/icons/pdf_file_icon.svg"
          style="height: 40px; width: 33px"
          img-class="pdf-icon"
        />
      </div>
      <div class="col text-subtitle1 text-blue-7">{{$t('components.client.statement.statementDetail.setStatement')}}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';

import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import ErrorDataSmallComponent from '../../../../components/shared/errorDataSmall.component.vue';
import NoDataSmallComponent from '../../../../components/shared/noDataSmall.component.vue';
import { AgreementInterface } from '../../../../interfaces/agreement';
import BaseDetailComponent from '../../../base/baseDetail.component.vue';
import AgreementDetailMixin from '../../../../mixins/agreement/detail/agreementDetail.mixin';
import AgreementBalancelMixin from '../../../../mixins/agreement/balance/agreementBalance.mixin';
import AgreementStatusComponent from './audit/agreementStatus.component.vue';
import { AgreementTypeEnumHelper } from '../../../../helpers/enumHelpers/agreementTypeEnum.helper';
import { AgreementViewTypeEnum } from '../../../../enums/agreement/agreementViewType.enum';
import { AgreementClassifierTypeCodeEnum } from '../../../../enums/agreement/agreementClassifierTypeCode.enum';
import { AgreementClassifierTypeCodeEnumHelper } from '../../../../helpers/enumHelpers/agreementClassifierTypeCodeEnum.helper';
import { FormatMoneyFilter } from '../../../../filters';

import constants from '../../../../common/constants';
import { ClientCardTabsEnum } from '../../../../enums/client/clientCardTabs.enum';

import { mixins } from 'vue-class-component';
import CardListMixin from '../../../../mixins/card/list/cardList.mixin';
import ApiResultModel from '../../../../models/api/apiResult.model';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, NoDataSmallComponent, ErrorDataComponent, ErrorDataSmallComponent, AgreementStatusComponent },
  filters: { FormatMoneyFilter }
})
export default class ClientAgreementDetailComponent extends mixins(BaseDetailComponent, AgreementDetailMixin, AgreementBalancelMixin, CardListMixin) {
  @Prop({ required: true }) declare detailData: AgreementInterface;

  private balance: any;
  private isBalanceLoading: boolean;
  private balanceHasError: boolean;

  constructor () {
    super();
    this.balance = null;
    this.isBalanceLoading = false;
    this.balanceHasError = false;
  }

  @Watch('detailData', { immediate: true })
  private async onDetailDataUpdate (detailData:any) {
    if (detailData) {
      await this.doBalanceRequest();
    }
  }

  private get bvrViewType () {
    return this.detailData?.viewType === AgreementViewTypeEnum.Bvr;
  }

  private get getBalance () {
    const usedCreditLimit = FormatMoneyFilter(this.balance.usedCreditLimit);
    return `${usedCreditLimit}`;
  }

  private getAgreementType (type: any) {
    return AgreementTypeEnumHelper.getTypeName(type);
  }

  private getClassifierName (name: AgreementClassifierTypeCodeEnum): string {
    return AgreementClassifierTypeCodeEnumHelper.getTypeCodeName(name);
  }

  async doBalanceRequest () {
    this.isBalanceLoading = true;
    const result = await this.loadAgreementBalance(this.$store, { solarId: parseInt(this.$route.params.id), agreementId: this.detailData?.agreementId });
    if (result instanceof ApiResultModel) {
      this.balanceHasError = !!result.hasError;
      this.balance = result.result?.item;
    }
    this.isBalanceLoading = false;
  }

  private async navigateToDebt () {
    const card = this.Cards?.find(x => x.agreementId === this.detailData?.agreementId);
    if (card) {
      await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id),
        {
          [constants.navigateQuery.card.cardId]: card.cardId.toString(),
          [constants.navigateQuery.agreement.agreementId]: this.detailData?.agreementId.toString(),
          [constants.navigateQuery.card.tabName]: ClientCardTabsEnum.Debt
        });
    }
  }

  private async navigateToStatement () {
    const params = {
      [constants.navigateQuery.statement.clientId]: this.$route.params.id,
      [constants.navigateQuery.agreement.agreementId]: this.detailData?.agreementId.toString()
    };
    await this.newTabBySolarId('clientStatement', parseInt(this.$route.params.id), params);
  }
}
</script>

<style scoped>
  .tbl {
    margin-top: 5px;
  }

  .tbl > div:nth-child(2n+1) {
    color: #7D7D80
  }

  small {
    padding-left: 4px;
  }

  .chip-locks {
    background-color: #FFEEE1;
    color: #E26F16;
  }

  .link {
    color: #0078D4;
  }
</style>
