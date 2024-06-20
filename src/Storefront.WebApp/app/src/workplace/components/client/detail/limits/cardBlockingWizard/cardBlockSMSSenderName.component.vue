<template>
  <div>
    <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.specifySMSSenderName') }}</div>
    <div style="max-width: 250px">
      <q-select emit-value map-options dense filled v-model="senderName" :options="senderNameList" :label="$t('components.client.wizard.wizard.senderNameInSMS')" />
    </div>

    <template v-if="getIsVostok">
      <div class="text-body1 text-weight-medium q-mt-xl q-mb-md">{{ $t('components.client.wizard.wizard.provideInformationFromClient') }}</div>
      <div class="row">
        <div class="col q-pr-xs">
          <q-input filled dense v-model="merchantName" :label="$t('components.client.wizard.wizard.merchantName')"/>
        </div>
        <div class="col q-pl-xs">
           <q-input filled dense v-model="date" :mask="dateInputMask" :label="$t('components.shared.dateAndTime')">
            <template v-slot:prepend>
              <q-icon name="event" class="cursor-pointer">
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-date v-model="date" :mask="formatting.dateTime">
                    <div class="row items-center justify-end">
                      <q-btn v-close-popup :label="$t('components.client.wizard.wizard.calendar.close')" color="primary" flat />
                    </div>
                  </q-date>
                </q-popup-proxy>
              </q-icon>
            </template>

            <template v-slot:append>
              <q-icon name="access_time" class="cursor-pointer">
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-time v-model="date" :mask="formatting.dateTime" format24h>
                    <div class="row items-center justify-end">
                      <q-btn v-close-popup :label="$t('components.client.wizard.wizard.calendar.close')" color="primary" flat />
                    </div>
                  </q-time>
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
        </div>
      </div>
      <div class="q-mt-lg row text-grey-7 flex-start">
        <q-icon class="col-auto q-mr-xs q-mt-xs" name="o_info" size="16px" />
        <div class="col text-caption">
          {{$t('components.client.wizard.wizard.bankRecommendsBlockingCard')}}
        </div>
      </div>
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
    </template>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import constants from '../../../../../common/constants';

import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { mixins } from 'vue-class-component';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';
import { SenderNameTypeEnum } from '../../../../../enums/card/limits/senderNameType.enum';
import { CardBlockSenderNameEnumHelper } from '../../../../../helpers/enumHelpers/cardBlockSenderNameTypeEnum.helper';

@Component({
  name: 'CardBlockSMSSenderNameComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent }
})
export default class CardBlockSMSSenderNameComponent extends mixins(BaseStepComponent) implements BaseStepInterface {
  private formatting: any;
  private date: any;
  private dateInputMask = '##.##.####, ##:##';
  private senderName:SenderNameTypeEnum | null;
  private senderNameList:Array<any>;
  private isClientAgreeBlockMobileApp: boolean | null;
  private clientAgreeBlockMobileAppOptions: any;
  private merchantName: string;

  constructor () {
    super();
    this.formatting = constants.formatting;
    this.date = '';
    this.senderName = null;
    this.senderNameList = [];
    this.isClientAgreeBlockMobileApp = null;
    this.clientAgreeBlockMobileAppOptions = [
      { label: this.$t('components.shared.yes'), value: true },
      { label: this.$t('components.shared.no'), value: false }
    ];
    for (const item in SenderNameTypeEnum) {
      this.senderNameList.push({
        label: CardBlockSenderNameEnumHelper.getName(item as SenderNameTypeEnum),
        value: item
      });
    }
    this.merchantName = '';
  }

  get getIsVostok () {
    return this.senderName === SenderNameTypeEnum.BankVostok;
  }

  validateStep (): string|string[]|undefined {
    const err:string[] = [];
    if (!this.senderName) err.push(this.$t('components.client.wizard.wizard.errors.cardBlockSMSSenderName.requiredSenderName').toString());

    if (this.getIsVostok) {
      if (this.merchantName.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.cardBlockSMSSenderName.requiredMerchantName').toString());

      if (this.date.trim() === '') err.push(this.$t('components.client.wizard.wizard.errors.cardBlockSMSSenderName.requiredDate').toString());

      if (this.isClientAgreeBlockMobileApp === null) err.push(this.$t('components.client.wizard.wizard.errors.cardBlockSMSSenderName.requiredClientAgree').toString());
    }

    return (err.length > 0) ? err : undefined;
  }

  collectData (): void {
    const localReasonInfo = Object.assign({}, this.reasonInfo);
    localReasonInfo.senderName = this.senderName;

    if (this.getIsVostok) {
      localReasonInfo.merchantName = this.merchantName;
      localReasonInfo.merchantDate = this.date;
      localReasonInfo.isClientAgreeBlockMobileApp = this.isClientAgreeBlockMobileApp;
    }

    this.$emit('update:reason-info', localReasonInfo);
  }
}
</script>

<style scoped>

</style>
