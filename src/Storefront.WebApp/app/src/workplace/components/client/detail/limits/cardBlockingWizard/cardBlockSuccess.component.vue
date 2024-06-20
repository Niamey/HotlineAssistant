<template>
  <block-spinner-component class="full-width row flex-center" v-if="dynamicData.isLoading" />
  <error-data-component  class="full-width row flex-center" v-else-if="dynamicData.hasError" />
  <div v-else-if="isResult">
    <template v-if="!dynamicData.showDetails">
      <q-list dense class="result" v-if="showOperationStatuses">
        <q-item dense v-for="(item,index) in dynamicData.result.operationStatuses" :key="index" :class="item.success ? 'green-color' : ''">
          <q-item-section avatar>
            <q-icon :name="item.success ? 'done' : 'remove_done'" />
          </q-item-section>
          <q-item-section>
            {{ item.message }}
          </q-item-section>
        </q-item>
      </q-list>
      <div v-if="showInfoBlock" class="q-mt-md row text-grey-7 items-start">
        <q-icon class="col-auto q-mr-sm q-ml-md info-block-icon" name="o_info" style="font-size: 18px;" />
        <div class="col text-caption">{{ showInfoText }}</div>
      </div>
      <div v-if="showClientAsksAboutUnlocking" class="q-mt-md q-pa-md rounded-borders client-asks">
        <div class="text-weight-medium">{{ $t('components.client.wizard.wizard.clientAsksAboutUnlocking.label') }}:</div>
        <div class="q-pl-md q-mt-md client-asks-item">{{ $t('components.client.wizard.wizard.clientAsksAboutUnlocking.line1') }}</div>
        <div class="q-pl-md q-mt-md client-asks-item">{{ $t('components.client.wizard.wizard.clientAsksAboutUnlocking.line2') }}</div>
      </div>
      <div v-if="showTransactionNotPossible2" class="q-mt-sm row text-grey-7 flex-top">
        <q-icon class="col-auto q-ma-xs" name="o_info" />
        <div class="col text-caption">{{ $t('components.client.wizard.wizard.transactionsNotPossibleCanBeUnlocked') }}</div>
      </div>
      <div v-if="askSendScreenShot" class="q-mt-lg row text-grey-7 flex-start">
        <q-icon class="col-auto q-ma-xs" name="o_info" />
        <div class="text-caption">
          {{$t('components.client.wizard.wizard.askSendScreenShot')}} <span class="text-primary">bank@bvr.com.ua</span>
        </div>
      </div>
      <div v-if="showMobileAppPinResetInfo" class="q-mt-lg row text-grey-7 flex-start">
        <q-icon class="col-auto q-ma-xs" name="o_info" />
        <div class="text-caption">{{$t('components.client.wizard.wizard.mobileAppPinResetInfo')}}</div>
      </div>
      <div v-if="showEmployeesNeverAsk" class="info-wrapper q-pa-md q-mt-md">
        <div class="text-weight-medium">{{ $t('components.client.wizard.wizard.employeesNeverAsk.labelTop') }}:</div>
        <div class="q-pl-md q-mt-md client-asks-item" v-for="item in [1,2,3,4]" :key="item">{{ $t(`components.client.wizard.wizard.employeesNeverAsk.data${item}`) }}</div>
        <div class="text-weight-medium q-mt-md">{{ $t('components.client.wizard.wizard.employeesNeverAsk.labelBottom') }}</div>
      </div>

    </template>
    <div v-else v-html="dynamicData.result.resultForOperator"></div>
  </div>
  <div v-else>
    <div v-if="showNotBVROperation" class="q-mt-sm row text-grey-7 flex-top">
      <q-icon class="col-auto q-ma-xs" name="o_info" />
      <div class="col text-caption">{{ $t('components.client.wizard.wizard.notBVROperation') }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { ReasonTypeEnum } from '../../../../../enums/card/limits/reasonType.enum';

import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { SecurityCodeOperationTypeEnum } from '../../../../../enums/card/limits/securityCodeOperationType.enum';

@Component({
  name: 'CardBlockSuccessComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent }
})
export default class CardBlockSuccessComponent extends BaseStepComponent {
  private showInfoText: string;

  constructor () {
    super();
    this.showInfoText = '';
  }

  get isShowItem () {
    return false;
    /*
    return !!(this.dynamicData?.reasonId === ReasonTypeEnum.FoundbByAnotherPerson ||
      this.dynamicData?.reasonId === ReasonTypeEnum.Lost ||
      this.dynamicData?.reasonId === ReasonTypeEnum.Stolen ||
      this.dynamicData?.reasonId === ReasonTypeEnum.ForgottenInATM);
    */
  }

  get isResult () {
    return this.dynamicData.result?.status;
  }

  get showInfoBlock () {
    switch (this.dynamicData.reasonId) {
      case ReasonTypeEnum.FoundbByAnotherPerson:
        this.showInfoText = this.$t('components.client.wizard.wizard.transactionsNotPossible').toString();
        return true;

      case ReasonTypeEnum.WithdrawnFromATM:
        this.showInfoText = this.$t('components.client.wizard.wizard.transactionsNotPossible').toString();
        if (this.dynamicData.isClientCall === true) {
          this.showInfoText += `. ${this.$t('components.client.wizard.wizard.cardGotToTheThirdPerson').toString()}`;
        }
        return true;

      case ReasonTypeEnum.ClientIsCheater:
        this.showInfoText = this.$t('components.client.wizard.wizard.infoWillBeTransferred').toString();
        return true;
    }

    return false;
  }

  get showOperationStatuses () {
    if (this.dynamicData.reasonId === ReasonTypeEnum.ClientIsCheater && this.reasonInfo.person.isRefusedToProvideInfo) {
      return false;
    }
    return true;
  }

  get showClientAsksAboutUnlocking () {
    return this.dynamicData.reasonId === ReasonTypeEnum.WithdrawnFromATM && this.dynamicData.isClientCall === true;
  }

  get showTransactionNotPossible2 () {
    return this.dynamicData.reasonId === ReasonTypeEnum.BlockingOperations;
  }

  get showNotBVROperation () {
    return this.dynamicData.reasonId === ReasonTypeEnum.ReceivedSMSCode;
  }

  get askSendScreenShot () {
    return this.dynamicData.reasonId === ReasonTypeEnum.ReceivedSMSCode &&
    this.reasonInfo.isSMSMissingGate &&
    this.reasonInfo.isSMSMissingUPC &&
    !this.reasonInfo.isClientAgreeBlockMobileApp;
  }

  get showEmployeesNeverAsk () {
    return this.dynamicData.reasonId === ReasonTypeEnum.ReceivedSMSCode &&
    !this.reasonInfo.isClientToldSMCodeFromSMS &&
    (
      (!this.reasonInfo.isSMSMissingUPC && this.reasonInfo.isClientAgreeBlockMobileApp) ||
      (this.reasonInfo.isSMSMissingUPC && !this.reasonInfo.isSMSMissingGate)
    );
  }

  get showMobileAppPinResetInfo () {
    return this.dynamicData.reasonId === ReasonTypeEnum.ReceivedSMSCode &&
    !this.reasonInfo.isClientToldSMCodeFromSMS &&
    (this.reasonInfo.securityCodeOperationType as SecurityCodeOperationTypeEnum) === SecurityCodeOperationTypeEnum.MobileAppPassword;
  }
}
</script>

<style scoped>
.info-wrapper {
  background: #F8F9F9;
  border-radius: 4px;
}
.green-color {
  color: #107C10;
}

.info-block-icon {
  margin-top: 1px;
}

.client-asks {
  background-color: #F8F9F9;
}

.client-asks-item {
  position: relative;
}

.client-asks-item::before {
  content: '';
  position: absolute;
  width: 6px;
  height: 6px;
  left: 0px;
  top: 7px;
  border-radius: 3px;
  background-color: var(--q-color-primary);
}

.result >>> .q-item__section--avatar {
  min-width: auto !important;
}

</style>
