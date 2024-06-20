<template>
<div class="row">
  <div class="full-width row flex-center q-pt-xs" v-if="this.showLoading">
          <service-list-skeleton-loader-component/>
  </div>
  <div v-else-if="showError" class="full-width row flex-center q-pt-xl" >
        <error-data-component  />
  </div>
   <q-list v-else
            class="row inline q-mt-xl"
            :data="listData"
            :hide-bottom="!this.showNoData"
            :hide-header="this.showNoData">
      <q-item style="max-width: 10.75em" v-for="service in listData" :key="service.id">
        <q-item-section class="flex flex-center" v-on:click="onClick(service)">
          <q-item-section>
            <q-btn v-if="service.isCustomIcon" class="btn .q-btn--outline" outline round size="1.33em">
              <img svg-inline :src="service.imageUrl" alt=""/>
            </q-btn>
            <q-btn v-else class="btn .q-btn--outline" outline round :icon="service.imageUrl" size="1.33em" />
          </q-item-section>
          <q-item-section>
            <q-item-label class="q-lbl q-pt-md">{{service.title}}</q-item-label>
          </q-item-section>
        </q-item-section>
      </q-item>
    </q-list>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import ServiceModel from './../../models/service/service.model';
import { ServiceInterface } from '../../interfaces/service';
import BaseListComponent from '../base/baseList.component.vue';
import ErrorDataComponent from './../shared/errorData.component.vue';
import ServiceListSkeletonLoaderComponent from './../shared/serviceListSkeletonLoader.component.vue';

@Component({
  components: { ErrorDataComponent, ServiceListSkeletonLoaderComponent }
})
export default class ServiceListComponent extends BaseListComponent {
   @Prop({ required: true }) declare listData: ServiceModel[];
   // @Prop() pageSize?: number;

   onClick (data: ServiceInterface) {
     window.location.href = data.url;
   }
}
</script>

<style scoped>
.q-btn--outline {
  background: white !important;
}
.btn {
  color: #E9E9EA;
}
.btn >>> i{
  color: #A8A9AA;
}
.btn:hover,
.btn:hover >>> i {
  color: #f47c20;
}
.btn:hover >>> img {
  filter: invert(50%) sepia(14%) saturate(3200%) hue-rotate(-30deg) brightness(135%) contrast(80%);
}
.btn >>> .q-focus-helper:before,
.btn >>> .q-focus-helper:after
{
    opacity: 1!important;
    transition: unset;
}
.q-lbl {
  color: #26272B;
  text-align: center;
  width: 7em;
}
</style>
