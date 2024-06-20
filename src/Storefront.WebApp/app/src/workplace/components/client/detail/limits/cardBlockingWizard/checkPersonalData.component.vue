<template>
  <div>
    <div class="text-body1 text-weight-medium q-mb-md">{{ $t('components.client.wizard.wizard.selectDataBecameKnown') }}:</div>

    <block-spinner-component class="full-width row flex-center" style="height: 450px;" v-if="isLoading" />
    <error-data-component  class="full-width row flex-center" style="height: 450px;" v-else-if="hasError" />
    <no-data-component class="full-width row flex-center" style="height: 450px;" v-else-if="!hasError && listPersonalData.length == 0" />
    <div class="row" v-else>
      <div v-for="item in listPersonalData" :key="item.type" class="col-6 q-pb-lg q-pr-lg">
        <div class="text-body1 text-weight-medium">{{ item.title }}</div>
        <div class="q-gutter-sm" v-for="subitem in item.values" :key="subitem.id">
          <q-checkbox v-model="subitem.value" :label="subitem.title" />
        </div>
      </div>
    </div>

    <div v-if="isCancelAccessCode" class="q-mt-sm row text-grey-7 flex-center">
      <q-icon class="col-auto q-mr-xs" name="o_info" />
      <div class="col text-caption">{{ $t('components.client.wizard.wizard.selectedValuesWillCancelRegistration') }}</div>
    </div>

    <div v-if="isNeedDeleteToken" class="q-mt-sm row text-grey-7 flex-center">
      <q-icon class="col-auto q-mr-xs" name="o_info" />
      <div class="col text-caption">{{ $t('components.client.wizard.wizard.findTokensWithNotActiveStatus') }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';

import { modulesApi } from '../../../../../api/modules.api';
import { ApiWrapperActionHelper } from '../../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { PersonalDataListResponseInterface } from '../../../../../interfaces/responses/modules';
import { PersonalDataListRequestInterface } from '../../../../../interfaces/requests/modules';
import { LocalizationEnum } from '../../../../../enums/localization/localization.enum';

import { utils } from '../../../../../utils/utils';
import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { BaseStepInterface } from './../../../../../interfaces/card/limits/baseStep.interface';
import { LimitsPersonalDataModel } from '../../../../../models/card/limits';
import { PersonDataTypeEnum } from '../../../../../enums/modules/personalDataType.enum';

@Component({
  name: 'CheckPersonalDataComponent',
  components: { NoDataComponent, BlockSpinnerComponent, ErrorDataComponent }
})
export default class CheckPersonalDataComponent extends BaseStepComponent implements BaseStepInterface {
  private listPersonalData: Array<any>;

  private isLoading: boolean;
  private hasError: boolean;

  constructor () {
    super();

    this.isLoading = false;
    this.hasError = false;

    this.listPersonalData = [];
  }

  async created () {
    await this.doRequest();
    this.setInitData();
  }

  protected async doRequest () {
    this.isLoading = true;
    const result = await this.loadPersonalDataList(this.$store, { localization: LocalizationEnum.Ukraine });

    await utils.delay(500);

    if (result instanceof ApiResultModel) {
      this.hasError = !!result.hasError;
      if (!this.hasError) {
        // @ts-ignore
        this.listPersonalData = result?.result?.rows;
      }
    }
    this.isLoading = false;
  }

  private async loadPersonalDataList (store: Store<any>, params: PersonalDataListRequestInterface) : Promise<void | ApiResultModel<PersonalDataListResponseInterface>> {
    const promise = modulesApi.getPersonalDataServiceAsync(params);
    return await ApiWrapperActionHelper.runWithTry<PersonalDataListResponseInterface>(store, promise);
  }

  private get isCancelAccessCode () {
    const anyChecked = this.listPersonalData.filter(b => {
      return b.values.find((i:any) => i.value === true) !== undefined;
    });

    if (anyChecked.length > 1) return true;

    if (anyChecked.length === 1) {
      return anyChecked[0].values.filter((x: { value: boolean; }) => x.value === true).length > 1;
    }
    return false;
  }

  private get isNeedDeleteToken () {
    const other = this.listPersonalData.find(x => x.type === PersonDataTypeEnum.Other);

    if (other) {
      return other.values.find((x: { id: number; }) => x.id === 16)?.value;
    }

    return false;
  }

  public validateStep () {
    const anyChecked = this.listPersonalData.find(b => {
      return b.values.find((i:any) => i.value === true) !== undefined;
    }) !== undefined;

    return (!anyChecked) ? this.$t('components.client.wizard.wizard.errors.checkPersonalData.requiredAny').toString() : undefined;
  }

  public collectData () {
    const localReasonInfo = Object.assign({}, this.reasonInfo);

    const filtered = [];
    for (const block of this.listPersonalData) {
      for (const i of block.values) {
        if (i.value === true) {
          filtered.push({
            personDataType: block.type,
            personDataValue: i.id,
            personDataTitle: i.title
          });
        }
      }
    }

    localReasonInfo.clientTransactions = {};
    localReasonInfo.properties = filtered.map(i => new LimitsPersonalDataModel(i));
    localReasonInfo.isNeedDeleteToken = this.isNeedDeleteToken;
    localReasonInfo.isCancelAccessCode = this.isCancelAccessCode;
    this.$emit('update:reason-info', localReasonInfo);
  }

  private setInitData () {
    if (this.reasonInfo.properties !== undefined && this.reasonInfo.properties.length > 0) {
      for (const block of this.listPersonalData) {
        for (const i of block.values) {
          if (this.reasonInfo.properties.find((t:any) => t.personDataType === block.type && t.personDataValue === i.id)) {
            i.value = true;
          }
        }
      }
    }
  }
}
</script>

<style scoped>
</style>
