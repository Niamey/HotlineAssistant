<template>
  <div>
    <div class="row">
      <div class="col-6 q-pr-xs">
        <div class="label q-mb-md">{{ $t('components.client.wizard.wizard.enterPhoneClient') }}</div>
        <vue-tel-input class="rounded" v-bind="bindProps" v-model="dynamicData.contactPhone"></vue-tel-input>
        <q-toggle class="q-mt-sm"
          v-model="useFinPhone"
          :label="$t('components.client.wizard.wizard.useFinPhone')"
          @input="setFinPhone()"
        />
      </div>
      <div class="col-6 q-pl-xs" v-if="showSelectOperationType">
        <div class="label q-mb-md">{{$t('components.client.wizard.wizard.selectOperationType')}}</div>
        <q-select emit-value map-options dense filled v-model="transactionType" :options="transactionTypeList" :label="$t('components.client.wizard.wizard.operationType')" />
      </div>
    </div>
    <div class="q-mt-md" v-if="showBlock">
      <div>{{$t('components.client.wizard.wizard.operationFromUserDevice')}}</div>
      <div class="q-gutter-sm">
          <q-option-group
            class="q-ml-none"
            :inline="true"
            :options="dataOptions"
            type="radio"
            v-model="transactionFromUserDevice"
          />
      </div>
      <div v-if="transactionFromUserDevice !== null" class="q-mt-lg row text-grey-7 flex-start">
              <q-icon class="col-auto q-mr-xs q-mt-xs" name="o_info" size="16px" />
              <div class="col text-caption">
                <div>{{ transactionFromUserDevice ? $t('components.client.wizard.wizard.changePinAndCallLater') : $t('components.client.wizard.wizard.callMobileOperatorAndBlockSim') }}</div>
                <div v-if="!transactionFromUserDevice" class="q-mt-lg">
                  {{$t('components.client.wizard.wizard.mobileAppAccessBlocked')}}
                </div>
              </div>
      </div>
    </div>
    <div class="q-mt-sm row text-grey-7 flex-center" v-if="showTransactionsNotPossibleReissue">
      <q-icon class="col-auto q-mr-xs" name="o_info" style="font-size:16px; margin-top:-1px;" />
      <div class="col text-caption">{{ $t('components.client.wizard.wizard.transactionsNotPossibleReissue') }}</div>
    </div>
    <div v-if="clarifyDetails" class="q-mt-lg row text-grey-7 flex-start">
        <q-icon class="col-auto q-mr-xs q-mt-xs" name="o_info" size="16px" />
        <div class="col text-caption">
          {{$t('components.client.wizard.wizard.transactionsNotPossibleСlarifyDetails')}}
        </div>
    </div>
    <div v-if="cancelRegistration" class="q-mt-lg row text-grey-7 flex-start">
        <q-icon class="col-auto q-mr-xs q-mt-xs" name="o_info" size="16px" />
        <div class="col text-caption">
          {{$t('components.client.wizard.wizard.cancelRegistrationInMobileApp')}}
        </div>
    </div>

    <div v-if="askSendScreenShot" class="q-mt-lg row text-grey-7 flex-start">
      <div class="text-caption">
        {{$t('components.client.wizard.wizard.askSendScreenShot')}} <span class="text-primary">bank@bvr.com.ua</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
// import { ReasonTypeEnum } from '../../../../../enums/card/limits/reasonType.enum';
// @ts-ignore
import { VueTelInput } from 'vue-tel-input';
import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { mixins } from 'vue-class-component';
import ClientDetailMixin from '../../../../../mixins/client/clientDetail.mixin';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';
import { TransactionTypeEnum } from '../../../../../enums/card/limits/transactionType.enum';
import { CardBlockTransactionEnumHelper } from '../../../../../helpers/enumHelpers/cardBlockTransactionTypeEnum.helper';
import { ReasonTypeEnum } from '../../../../../enums/card/limits';

@Component({
  name: 'CardTransactionsTypeComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent, VueTelInput }
})
export default class CardTransactionsTypeComponent extends mixins(BaseStepComponent, ClientDetailMixin) implements BaseStepInterface {
  private bindProps:any;
  private transactionTypeList:Array<any>;
  private transactionFromUserDevice: boolean | null;
  private transactionType: TransactionTypeEnum | null;
  private dataOptions: Array<any>;
  private useFinPhone: boolean;

  constructor () {
    super();
    this.bindProps = {
      mode: 'international',
      defaultCountry: 'UA',
      disabledFetchingCountry: false,
      disabled: false,
      disabledFormatting: false,
      placeholder: '',
      required: true,
      enabledCountryCode: true,
      preferredCountries: ['UA', 'RU', 'BY'],
      autocomplete: 'off',
      validCharactersOnly: true
    };
    this.transactionType = null;
    this.transactionFromUserDevice = null;
    this.transactionTypeList = [];
    this.dataOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];
    this.useFinPhone = false;
  }

  async created () {
    if (!this.ClientDetail) {
      await this.loadDetail(this.dynamicData.cardDetail.solarId);
    }
    for (const item in TransactionTypeEnum) {
      this.transactionTypeList.push({
        label: CardBlockTransactionEnumHelper.getTypeName(item as TransactionTypeEnum),
        value: item
      });
    }
  }

  validateStep (): string|string[]|undefined {
    const err:string[] = [];

    if (this.dynamicData.contactPhone.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.cardTransactionsType.requiredPhone').toString());

    if (this.showSelectOperationType) {
      if (this.transactionType === null) err.push(this.$t('components.client.wizard.wizard.errors.cardTransactionsType.requiredTransactionsType').toString());

      if (this.transactionType === TransactionTypeEnum.MobileApp && this.transactionFromUserDevice === null) err.push(this.$t('components.client.wizard.wizard.errors.cardTransactionsType.requiredFromClientMobileApp').toString());
    }

    return (err.length > 0) ? err : undefined;
  }

  collectData (): void {
    const localReasonInfo = Object.assign({}, this.reasonInfo);
    localReasonInfo.contactPhone = this.dynamicData.contactPhone;
    if (this.showSelectOperationType) {
      localReasonInfo.transactionType = this.transactionType;
      localReasonInfo.isFromUserDevice = this.transactionFromUserDevice;
    }
    this.$emit('update:reason-info', localReasonInfo);
  }

  get showBlock () {
    return this.transactionType === TransactionTypeEnum.MobileApp;
  }

  get showSelectOperationType () {
    return this.dynamicData.reasonId !== ReasonTypeEnum.ForgottenInATM &&
     this.dynamicData.reasonId !== ReasonTypeEnum.Lost &&
     this.dynamicData.reasonId !== ReasonTypeEnum.Stolen &&
     this.dynamicData.reasonId !== ReasonTypeEnum.ReceivedSMSCode;
  }

  get showTransactionsNotPossibleReissue () {
    return (this.dynamicData.reasonId !== ReasonTypeEnum.ForgottenInATM) &&
    (this.dynamicData.reasonId !== ReasonTypeEnum.ReportedData) &&
    ((this.dynamicData.reasonId === ReasonTypeEnum.Lost) ||
    (this.dynamicData.reasonId === ReasonTypeEnum.Stolen));
  }

  get cancelRegistration () {
    if (this.dynamicData.reasonId === ReasonTypeEnum.ReportedData ||
    this.dynamicData.reasonId === ReasonTypeEnum.ReceivedSMSCode) {
      return this.reasonInfo.isCancelAccessCode;
    }
    return false;
  }

  get clarifyDetails () {
    return this.dynamicData.reasonId === ReasonTypeEnum.ReportedData ||
    this.dynamicData.reasonId === ReasonTypeEnum.ReceivedSMSCode;
  }

  get askSendScreenShot () {
    return this.dynamicData.reasonId === ReasonTypeEnum.ReceivedSMSCode &&
    (this.reasonInfo.isSMSMissingGate && this.reasonInfo.isSMSMissingUPC);
  }

  setFinPhone () {
    if (this.useFinPhone) {
      this.dynamicData.contactPhone = this.ClientDetail?.financialPhone?.toString();
    } else {
      this.dynamicData.contactPhone = '';
    }
  }
}
</script>

<style scoped>
.vue-tel-input {
  height: 40px;
  border: none;
  background: #f2f2f2;
}
.vue-tel-input >>> input {
  background: #f2f2f2;
}
.vue-tel-input:focus-within {
  box-shadow: none;
  border-bottom: 2px solid var(--q-color-primary) !important;
  background: #E6E6E6;
}
.vue-tel-input:focus-within >>> input {
  background: #E6E6E6;
}
.vue-tel-input >>> .vti__dropdown {
  display: none;
}
.label {
  font-weight: 500;
  font-size: 16px;
  line-height: 24px;
  color: #26272B;
}
.green-color {
  color: #107C10;
}
</style>
