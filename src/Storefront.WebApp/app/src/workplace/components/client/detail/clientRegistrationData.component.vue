<template>
  <q-card class="q-mb-md">
    <block-spinner-component v-if="showLoading" :loaded="showLoading" />
    <more-information-component v-else
                                v-bind:is-show="showData"
                                v-bind:labelExpand="$t('components.client.detail.clientRegistrationData.labelExpand')"
                                v-bind:labelCollapse="$t('components.client.detail.clientRegistrationData.labelCollapse')">
      <template #header>
        <div class="row">
          <div class="col text-h6 text-weight-medium">
          {{ $t('components.client.detail.clientRegistrationData.label')}}</div>
          <q-chip v-if="detailData.segmentationType && detailData.segmentationType.segmentType === 'Vip'" class="segmentTypeVip col-auto q-px-sm" square text-color="primary" >
            <q-icon name="star_rate" size="18px"></q-icon>
                  {{ getType(detailData.segmentationType.segmentType)}}
            </q-chip>
            <q-chip v-else-if="detailData.segmentationType" class="segmentType" square >{{ getType(detailData.segmentationType.segmentType)}}</q-chip>
        </div>
        <no-data-component v-if="showNoData" />
        <error-data-component v-else-if="showError" />
        <div class="row text-subtitle1" v-else-if="showData">
          <div class="col-auto q-mr-md"> {{ detailData.fullName }}</div>
          <div class="col-auto q-mr-md">
            <div class="row items-center">
              <q-icon v-if="detailData.financialPhone" name="phone" class="grey-color q-mr-sm vertical-middle" size="15px" />
              {{ detailData.financialPhone }}
            </div>
          </div>
          <div class="col"><span class="grey-color">{{ $t('components.client.detail.clientRegistrationData.inn') }}</span>
            {{ detailData.inn }}</div>
        </div>
      </template>
      <div class="q-gutter-y-sm" v-if="showData">
        <div class="text-weight-medium" style="font-size:18px">{{ $t('components.client.detail.clientRegistrationData.passportData')}}</div>
        <div class="row q-gutter-y-sm" v-if="detailData.mainDocument">
          <div class="col-4 col-xl-3 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.documentType')}}</div>
            <div>{{ detailData.mainDocument.documentType.name }}</div>
          </div>
          <div class="col-4 col-xl-3 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.documentSeriesAndNumber')}}</div>
            <div>{{ detailData.mainDocument.series }} {{ detailData.mainDocument.number }}</div>
          </div>
          <div class="col-4 col-xl-3 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.documentIssuedDate')}}</div>
            <div>{{ detailData.mainDocument.issueDate }}</div>
          </div>
          <div class="col-4 col-xl-3 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.citizenship')}}</div>
            <div>{{ detailData.residentCountry }}</div>
          </div>
          <div class="col-8 col-xl-6 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.documentIssuer')}}</div>
            <div>{{ detailData.mainDocument.issueBy }}</div>
          </div>
          <div class="col-4 col-xl-3 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.documentExpirationDate')}}</div>
            <div>{{ detailData.mainDocument.expirationDate }}</div>
          </div>
          <div class="col-4 col-xl-3 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.documentPhotoDate')}}</div>
            <div v-if="detailData.mainDocument">{{ detailData.mainDocument.photoDate }}</div>
          </div>
        </div>
        <q-separator color="#A8A9AA" />
        <div class="row q-gutter-y-sm">
          <div class="col-8 col-xl-6 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.birthPlace')}}</div>
            <div>{{ detailData.birthPlace }}</div>
          </div>
          <div class="col-4 col-xl-3 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.birthDate')}}</div>
            <div>{{ detailData.dateOfBirth }}</div>
          </div>
        </div>
        <q-separator color="#A8A9AA" />
        <div class="row q-gutter-y-sm">
          <div class="col-4 col-xl-3 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.residency')}}</div>
            <div>{{ detailData.residentCountry }}</div>
          </div>
          <div class="col-8 col-xl-9 cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.addressOfResidence')}}</div>
            <div>{{ detailData.residentialAddress }}</div>
          </div>
        </div>
        <div class="cell">
          <div>{{ $t('components.client.detail.clientRegistrationData.registrationAddress')}}</div>
          <div>{{ detailData.registrationAddress }}</div>
        </div>
        <q-separator color="#A8A9AA" />
        <div class="row q-gutter-y-sm">
          <div class="col-4 col-xl cell">
            <div class="row items-center">
              {{ $t('components.client.detail.clientRegistrationData.email')}}
            </div>
            <div>{{ detailData.email }}</div>
          </div>
          <div class="col-4 col-xl cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.job')}}</div>
            <div>{{ detailData.workPlace }}</div>
          </div>
          <div class="col-4 col-xl cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.position')}}</div>
            <div>{{ detailData.workPosition}}</div>
          </div>
          <div class="col-4 col-xl cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.password')}}</div>
            <div>{{ detailData.codeWord }}</div>
          </div>
          <div class="col-4 col-xl cell">
            <div>{{ $t('components.client.detail.clientRegistrationData.gender.label')}}</div>
            <div>{{ getGender(detailData.gender)}}</div>
          </div>
        </div>
      </div>
    </more-information-component>
  </q-card>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import MoreInformationComponent from '../../../components/shared/moreInformation.component.vue';
import BlockSpinnerComponent from '../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../shared/noData.component.vue';
import ErrorDataComponent from '../../shared/errorData.component.vue';
import { CounterpartInterface } from '../../../interfaces/client';
import BaseDetailComponent from '../../base/baseDetail.component.vue';
import { SegmentTypeEnum } from '../../../enums/client/segmentType.enum';
import { SegmentTypeEnumHelper } from '../../../helpers/enumHelpers/segmentTypeEnum.helper';
import { ClientGenderEnum } from '../../../enums/client/clientGender.enum';
import { ClientGenderEnumHelper } from '../../../helpers/enumHelpers/clientGenderEnum.helper';

@Component({
  components: { MoreInformationComponent, BlockSpinnerComponent, NoDataComponent, ErrorDataComponent }
})
export default class ClientRegistrationDataComponent extends BaseDetailComponent {
  @Prop({ required: true }) declare detailData: CounterpartInterface;

  getType (type: SegmentTypeEnum) {
    return SegmentTypeEnumHelper.getTypeName(type);
  }

  getGender (gender: ClientGenderEnum) {
    return ClientGenderEnumHelper.getGender(gender);
  }

  // TODO check this variation later
  // protected get showData (): boolean {
  //   return super.showData && this.detailData.counterpartId != null;
  // }
  protected get showData (): boolean {
    return !this.showLoading && !this.showError && this.detailData !== undefined && this.detailData.counterpartId != null;
  }
}
</script>

<style scoped>
  .segmentType {
    color: #7D7D80;
    background-color: #E9E9EA;
    height: 24px;
  }
  .segmentTypeVip {
    text-transform: uppercase;
    background-color: #FDE5D2;
    height: 24px;
    font-size: 20px;
  }
  .grey-color {
    color: #A8A9AA;
  }
  .cell div:first-child {
    color: #7D7D80;
    font-size: 14px;
  }
  .active {
    color: #107C10;
  }
</style>
