<template>
  <div>
        <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.enterDataPersonWhoCall') }}</div>
        <div class="row">
          <div class="col-8 q-pr-xs">
              <div class='text-grey-7 text-caption'>{{ $t('components.client.wizard.wizard.fullName') }}</div>
              <q-input v-model="fullName" dense filled/>
          </div>
          <div class="col-4 q-pl-xs">
              <div class='text-grey-7 text-caption'>{{ $t('components.client.wizard.wizard.phone') }}</div>
              <vue-tel-input :enabledFlags="false" placeholder="" :onlyCountries="['UA']" v-model="phone"></vue-tel-input>
          </div>
        </div>

        <q-input v-model="description"
          filled
          type="textarea"
          class="q-my-md"
          :placeholder="getDescriptionPlaceholder"
        />

        <q-toggle
          v-model="isRefusedToProvideInfo"
          :label="$t('components.client.wizard.wizard.refusedToProvideInfo')"
          style="margin-left:-10px;"
        />

        <div class="q-mt-sm row text-grey-7 flex-center" v-if="dynamicData.reasonId !== getReasonTypeEnum.ClientIsCheater">
          <q-icon class="col-auto q-mr-xs" name="o_info" style="font-size:16px; margin-top:-1px;" />
          <div class="col text-caption">{{ $t('components.client.wizard.wizard.allFieldsRequired') }}</div>
        </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
// @ts-ignore
import { VueTelInput } from 'vue-tel-input';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';
import { LimitsPersonWhoCallsModel } from '../../../../../models/card/limits';
import { ReasonTypeEnum } from '../../../../../enums/card/limits/reasonType.enum';

@Component({
  name: 'PersonWhoCallsDetailsComponent',
  components: { VueTelInput }
})
export default class PersonWhoCallsDetailsComponent extends BaseStepComponent implements BaseStepInterface {
  private fullName: string;
  private phone: string;
  private description: string;
  private isRefusedToProvideInfo: boolean;

  constructor () {
    super();
    this.fullName = '';
    this.phone = '';
    this.description = '';
    this.isRefusedToProvideInfo = false;
  }

  created () {
    this.setInitData();
  }

  private get getDescriptionPlaceholder () {
    return (this.dynamicData.reasonId === ReasonTypeEnum.ClientIsCheater) ? this.$t('components.client.wizard.wizard.cheaterInformatiomDescription') : this.$t('components.client.wizard.wizard.personWhoCallsDescription');
  }

  private get getReasonTypeEnum () {
    return ReasonTypeEnum;
  }

  public validateStep () {
    const err:string[] = [];
    if (this.fullName.trim() === '') {
      err.push(this.$t('components.client.wizard.wizard.errors.personalWhoCallsDetails.requiredFullName').toString());
    }
    if (this.phone.trim() === '') {
      err.push(this.$t('components.client.wizard.wizard.errors.personalWhoCallsDetails.requiredPhone').toString());
    }

    if (this.dynamicData.reasonId !== ReasonTypeEnum.ClientIsCheater || (this.dynamicData.reasonId === ReasonTypeEnum.ClientIsCheater && !this.isRefusedToProvideInfo)) {
      if (this.description.trim() === '') {
        err.push(this.$t('components.client.wizard.wizard.errors.personalWhoCallsDetails.requiredDescription').toString());
      }
    }

    return (err.length > 0) ? err : undefined;
  }

  public collectData () {
    const localReasonInfo = Object.assign({}, this.reasonInfo);

    localReasonInfo.person = new LimitsPersonWhoCallsModel({
      fullName: this.fullName,
      phone: this.phone,
      comments: this.description
    });

    if (this.dynamicData.reasonId === ReasonTypeEnum.ClientIsCheater) {
      localReasonInfo.person.isRefusedToProvideInfo = this.isRefusedToProvideInfo;
    }

    this.$emit('update:reason-info', localReasonInfo);
  }

  private setInitData () {
    if (this.reasonInfo.person !== undefined && Object.keys(this.reasonInfo.person).length > 0) {
      this.fullName = this.reasonInfo.person.fullName;
      this.phone = this.reasonInfo.person.phone;
      this.description = this.reasonInfo.person.comments;

      if (this.dynamicData.reasonId !== ReasonTypeEnum.ClientIsCheater) {
        this.isRefusedToProvideInfo = this.reasonInfo.person.isRefusedToProvideInfo;
      }
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
  border-bottom: 2px solid var(--q-color-primary)!important;
  background: #E6E6E6;
}
.vue-tel-input:focus-within >>> input {
  background: #E6E6E6;
}
.vue-tel-input >>> .vti__dropdown {
  display: none;
}
</style>
