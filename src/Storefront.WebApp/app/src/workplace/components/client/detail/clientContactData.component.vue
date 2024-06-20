<template>
  <q-card class="client-data no-shadow">
    <div class="row text-body1 q-pa-sm no-wrap justify-center" v-if="showData && isShow">
      <div class="col-auto q-mr-md">
        <div class="row items-center">
          <q-icon v-if="detailData.fullName" name="person" class="q-mr-sm vertical-middle" size="15px" />
          {{ detailData.fullName }}
        </div>
      </div>
      <div class="col-auto q-mr-md">
        <div class="row items-center">
          <q-icon v-if="detailData.financialPhone" name="phone" class="q-mr-sm vertical-middle" size="15px" />
          {{ detailData.financialPhone }}
        </div>
      </div>
      <div class="col-auto"><span class="">{{ $t('components.client.detail.clientRegistrationData.inn') }}</span>
        {{ detailData.inn }}
      </div>
    </div>
  </q-card>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import { CounterpartInterface } from '../../../interfaces/client';
import BaseDetailComponent from '../../base/baseDetail.component.vue';

@Component
export default class ClientContactDataComponent extends BaseDetailComponent {
  @Prop({ required: true }) declare detailData: CounterpartInterface;
  @Prop({ required: true }) isShow!: boolean;
}
</script>

<style scoped>
.client-data {
  background-color:rgba(255, 255, 255, 0.1);
}
</style>
