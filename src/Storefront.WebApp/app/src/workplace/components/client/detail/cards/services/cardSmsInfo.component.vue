<template>
  <block-spinner-component v-if="this.showLoading" class="full-width row flex-center" :loaded="this.showLoading" />
  <error-data-component v-else-if="showError" style="min-height: 145px;" class="full-width row flex-center" />
  <no-data-component v-else-if="showNoData" style="min-height: 145px;" class="full-width row flex-center" />
  <div v-else-if="showData && localData">
    <div class="row tbl text-subtitle1">
      <div class="col-3 q-mb-sm text-grey-7">{{$t('components.client.detail.cards.clientCardServices.status')}}:</div>
      <div class="col-9 q-mb-sm text-light-blue-14 cursor-pointer" >
      <sms-info-status-component :status="detailData.status">{{ getStatus }}</sms-info-status-component>
      </div>

      <div v-if="isActive" class="col-3 q-mb-sm text-grey-7">{{ $t('components.client.detail.cards.clientCardServices.phone') }}:</div>
      <div v-if="isActive" class="col-9 q-mb-sm row items-center">
        <div :class="{'col-auto':!localData.isFinancePhone}">{{localData.phone}}</div>
        <div v-if="!localData.isFinancePhone" class="col-auto q-ml-sm text-grey-7 text-caption">({{ $t('components.client.detail.cards.clientCardServices.notFinPhone') }})</div>
      </div>

      <div class="col-3 q-mb-sm text-grey-7">{{ $t('components.client.detail.cards.clientCardServices.tariff') }}:</div>
      <div class="col-9 q-mb-sm">{{localData.tariff}}</div>
    </div>
    <div class="col-auto">
      <q-spinner v-if="statusIsLoading" size="md" color="primary" />
      <error-data-small-component v-else-if="statusHasError" />
      <q-toggle v-else v-model="service" left-label :label="getLabel" @input="toggleSmsInfoRequest" />
    </div>
  </div>
</template>

<script lang="ts">

import { Component, Prop, Watch } from 'vue-property-decorator';
import BaseDetailComponent from '../../../../base/baseDetail.component.vue';
import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import ErrorDataSmallComponent from '../../../../shared/errorDataSmall.component.vue';
import SmsInfoStatusComponent from '../../../../client/detail/cards/services/audit/smsInfoStatus.component.vue';

import { cardApi } from '../../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { NavigateQueryHelper } from '../../../../../helpers/navigateQuery.helper';

import { ChangeSmsInfoStatusRequestInterface } from '../../../../../interfaces/requests/card/services';
import { ChangeSmsInfoStatusResponseInterface } from '../../../../../interfaces/responses/card/services';

import { CardSmsInfoStatusEnum } from '../../../../../enums/card/services/cardSmsInfoStatus.enum';

@Component({
  components: { BlockSpinnerComponent, ErrorDataComponent, NoDataComponent, SmsInfoStatusComponent, ErrorDataSmallComponent }

})
export default class CardSmsInfoComponent extends BaseDetailComponent {
  @Prop({ required: true }) declare detailData: any;
  private localData: any;
  private service: boolean;

  private statusData?: any;
  private statusIsLoading: boolean;
  private statusHasError: boolean;

  constructor () {
    super();
    this.localData = Object.assign({}, this.detailData);
    this.service = this.isActive;

    this.statusIsLoading = false;
    this.statusHasError = false;
  }

  @Watch('detailData', { immediate: true })
  private onUpdated () {
    this.localData = Object.assign({}, this.detailData);
    this.service = this.isActive;
  }

  get getStatus () {
    if (!this.localData) return '';
    return this.isActive
      ? this.$t('enums.cardSmsInfoStatusEnum.active')
      : this.$t('enums.cardSmsInfoStatusEnum.inactive');
  }

  get isActive () {
    return !!(this.localData?.status === CardSmsInfoStatusEnum.Active);
  }

  private async toggleSmsInfoRequest () {
    await this.doChangeSmsInfoStatusRequest();
    if (!this.statusHasError) {
      this.localData.status = this.localData.status === CardSmsInfoStatusEnum.Active
        ? CardSmsInfoStatusEnum.Inactive : CardSmsInfoStatusEnum.Active;
    }
  }

  private async doChangeSmsInfoStatusRequest () {
    this.statusIsLoading = true;
    const result = await this.changeSmsInfoStatus(this.$store, { solarId: parseInt(this.$route.params.id), cardId: NavigateQueryHelper.cards.getCardId(this.$route) as number });
    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      // const resultStatus = result.result?.item;
    }
    this.statusIsLoading = false;
  }

  private async changeSmsInfoStatus (store: Store<any>, params:ChangeSmsInfoStatusRequestInterface) : Promise<void | ApiResultModel<ChangeSmsInfoStatusResponseInterface>> {
    const promise = this.isActive
      ? cardApi.changeSmsInfoStatusTurnOffAsync(params)
      : cardApi.changeSmsInfoStatusTurnOnAsync(params);
    return await ApiWrapperActionHelper.runWithTry<ChangeSmsInfoStatusResponseInterface>(store, promise);
  }

  get getLabel () {
    return this.service
      ? this.$t('components.client.detail.cards.services.cardSmsInfo.toggleTurnOff')
      : this.$t('components.client.detail.cards.services.cardSmsInfo.toggleTurnOn');
  }
}
</script>

<style scoped>
</style>
