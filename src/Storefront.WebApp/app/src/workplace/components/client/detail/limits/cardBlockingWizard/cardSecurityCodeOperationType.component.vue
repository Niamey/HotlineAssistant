<template>
  <div>
    <div style="max-width: 250px;">
      <div class="label q-mb-md">{{$t('components.client.wizard.wizard.selectOperationType')}}</div>
      <q-select emit-value map-options dense filled v-model="operationType" :options="operationTypeList" :label="$t('components.client.wizard.wizard.operationType')" />
    </div>
    <template v-if="isMobileAppPassword">
      <div class="q-mt-lg row text-grey-7 flex-top">
        <q-icon class="col-auto q-ma-xs" name="o_info" />
        <div class="col text-caption">{{ $t('components.client.wizard.wizard.securityCode.mobilePasswordAppInfo') }}</div>
      </div>
    </template>
    <template v-if="isCardVerification">
      <div class="q-mt-lg row text-grey-7 flex-top">
        <q-icon class="col-auto q-ma-xs" name="o_info" />
        <div class="col text-caption">{{ $t('components.client.wizard.wizard.securityCode.cardVerificationTokenInfo') }}</div>
      </div>
      <div class="q-mt-lg row text-grey-7 flex-top">
        <q-icon class="col-auto q-ma-xs" name="o_info" />
        <div class="col text-caption">{{ $t('components.client.wizard.wizard.securityCode.cardVerificationInfo') }}</div>
      </div>
    </template>
    <template v-if="isOtherCode">
      <div class="q-mt-lg row text-grey-7 flex-top">
        <q-icon class="col-auto q-ma-xs" name="o_info" />
        <div class="col text-caption">{{ $t('components.client.wizard.wizard.securityCode.otherCodeInfo') }}</div>
      </div>
    </template>
    <div class="q-mt-lg">{{ $t('components.client.wizard.wizard.customerAgreeBlockCard') }}</div>
      <div class="q-gutter-sm">
          <q-option-group
            class="q-ml-none"
            :inline="true"
            :options="clientAgreeBlockMobileAppOptions"
            type="radio"
            v-model="isClientAgreeBlockMobileApp"
          />
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { mixins } from 'vue-class-component';
import ClientDetailMixin from '../../../../../mixins/client/clientDetail.mixin';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';
import { SecurityCodeOperationTypeEnum } from '../../../../../enums/card/limits/securityCodeOperationType.enum';
import { CardBlockSecurityCodeOperationEnumHelper } from '../../../../../helpers/enumHelpers/cardBlockSecuriyCodeOperationEnum.helper';

@Component({
  name: 'CardSecurityCodeOperationTypeComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent }
})
export default class CardSecurityCodeOperationTypeComponent extends mixins(BaseStepComponent, ClientDetailMixin) implements BaseStepInterface {
  private operationTypeList:Array<any>;
  private operationType: SecurityCodeOperationTypeEnum | null;
  private clientAgreeBlockMobileAppOptions: Array<any>;
  private isClientAgreeBlockMobileApp: boolean | null;

  constructor () {
    super();
    this.isClientAgreeBlockMobileApp = null;
    this.operationType = null;
    this.operationTypeList = [];
    this.clientAgreeBlockMobileAppOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];
  }

  get isMobileAppPassword () {
    return this.operationType === SecurityCodeOperationTypeEnum.MobileAppPassword;
  }

  get isCardVerification () {
    return this.operationType === SecurityCodeOperationTypeEnum.CardVerification;
  }

  get isOtherCode () {
    return this.operationType === SecurityCodeOperationTypeEnum.Other;
  }

  async created () {
    if (!this.ClientDetail) {
      await this.loadDetail(this.dynamicData.cardDetail.solarId);
    }
    for (const item in SecurityCodeOperationTypeEnum) {
      this.operationTypeList.push({
        label: CardBlockSecurityCodeOperationEnumHelper.getTypeName(item as SecurityCodeOperationTypeEnum),
        value: item
      });
    }
  }

  validateStep (): string|string[]|undefined {
    const err:string[] = [];

    if (this.operationType === null) err.push(this.$t('components.client.wizard.wizard.errors.cardSecurityCodeOperationType.requiredOperationType').toString());

    if (this.isClientAgreeBlockMobileApp === null) err.push(this.$t('components.client.wizard.wizard.errors.cardSecurityCodeOperationType.requiredClientAgree').toString());

    return (err.length > 0) ? err : undefined;
  }

  collectData (): void {
    const localReasonInfo = Object.assign({}, this.reasonInfo);
    localReasonInfo.isClientAgreeBlockMobileApp = this.isClientAgreeBlockMobileApp;
    localReasonInfo.securityCodeOperationType = this.operationType;
    localReasonInfo.isNeedDeleteToken = this.operationType === SecurityCodeOperationTypeEnum.CardVerification;
    this.$emit('update:reason-info', localReasonInfo);
  }
}
</script>

<style scoped>
</style>
