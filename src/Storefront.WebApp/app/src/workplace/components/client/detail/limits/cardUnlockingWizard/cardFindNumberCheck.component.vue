<template>
  <div>
    <div class='text-grey-7 text-caption'>{{ $t('components.client.wizard.unlocking.enterContactPhoneNumber') }}</div>
    <div class="row">
      <vue-tel-input class="col-auto rounded" v-bind="bindProps" v-model="dynamicData.contactPhone" @validate="contactPhoneValidate" ref="contactPhone"></vue-tel-input>
    </div>

    <div v-if="isFinancePhoneConfirmed" class="q-mt-md row green-color flex-start">
      <q-icon class="col-auto q-mr-xs" name="done" size="20px" />
      <div class="col">{{ $t('components.client.wizard.unlocking.financialPhoneСonfirmed') }}</div>
    </div>
    <div v-else-if="isContactPhoneValid" class="q-mt-md row red-color flex-start">
      <q-icon class="col-auto q-mr-xs" name="close" size="20px" />
      <div class="col">{{ $t('components.client.wizard.unlocking.financialPhoneNotСonfirmed') }}</div>
    </div>
  </div>

</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';
import { mixins } from 'vue-class-component';
import ClientDetailMixin from '../../../../../mixins/client/clientDetail.mixin';

// @ts-ignore
import { VueTelInput } from 'vue-tel-input';

@Component({
  name: 'CardFindNumberCheckComponent',
  components: { VueTelInput }
})
export default class CardFindNumberCheckComponent extends mixins(ClientDetailMixin, BaseStepComponent) implements BaseStepInterface {
  private bindProps:any;
  private isContactPhoneValid: boolean;
  private isFinancePhoneConfirmed = false;

  constructor () {
    super();
    this.isContactPhoneValid = false;
    this.isFinancePhoneConfirmed = false;
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
  }

  created () {
    this.dynamicData.financePhone = this.ClientDetail?.financialPhone;
  }

  contactPhoneValidate (result: any) {
    this.isContactPhoneValid = result.isValid;
    if (this.isContactPhoneValid) {
      this.isFinancePhoneConfirmed = this.dynamicData.contactPhone.replace(/\s/g, '') === `+${this.dynamicData.financePhone}`;
    } else {
      this.isFinancePhoneConfirmed = false;
    }
  }

  public validateStep () {
    return !this.isContactPhoneValid ? this.$t('components.client.wizard.unlocking.errors.cardFindNumberCheck.requiredPhone').toString() : undefined;
  }

  public collectData () {
    const localUnlockingInfo = Object.assign({}, this.unlockingInfo);

    localUnlockingInfo.contactPhone = this.dynamicData.contactPhone.replace(/\s/g, '');
    localUnlockingInfo.financePhone = `+${this.dynamicData.financePhone}`;
    localUnlockingInfo.isFinancePhoneConfirmed = this.isFinancePhoneConfirmed;
    this.$emit('update:unlocking-info', localUnlockingInfo);
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
.green-color {
  color: #107C10;
}
.red-color{
  color: #D83B01;
}
</style>
