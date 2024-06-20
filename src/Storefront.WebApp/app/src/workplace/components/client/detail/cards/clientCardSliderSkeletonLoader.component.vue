<template>
  <div
    class="list-carousel bg-none row"
  >
    <div v-for="i in carouselSlideCount" :key="i"  :class="[carouselSlideCount < 3 ? 'col-auto no-wrap' : 'col no-wrap']">
      <div class="row fit justify-start items-start q-gutter-x-xs no-wrap">
        <q-card class="card no-box-shadow bg-none">
          <q-card-section>
            <div  class="card-title cursor-pointer"><q-skeleton type="rect" width="50%"/></div>
          </q-card-section>
          <q-card-section class="q-pt-none row justify-center q-pb-md">
              <div class="col-2 q-px-sm">
                <!-- vertical carousel -->
                <q-skeleton v-for="j in 4" :key="'j' + j" type="rect" width="32px" :class="[j === 4 ? 'q-mt-xs v-carusel-item' : 'q-mt-xs b14 v-carusel-item']"/>
                <!-- end vertical carousel -->
              </div>
              <div class="col img-container">
                <q-skeleton type="rect" class="card-img"/>
              </div>
          </q-card-section>
          <q-card-section class="q-pt-none info-title">
            <div class="card-info cursor-pointer"><q-skeleton type="text" width="50%"/></div>
          </q-card-section>
          <q-card-section class="q-pt-none q-pb-sm info-section">
            <div class="row tbl q-gutter-y-sm">
              <template v-for="k in 5">
                <div class="col-5" :key="'k1' + k"><q-skeleton type="text" width="60%" class="info-text"/></div>
                <div class="col-7" :key="'k2' + k"><q-skeleton type="text" width="40%" class="info-text"/></div>
              </template>
            </div>
          </q-card-section>
        </q-card>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CardSliderInfoComponent from './slider/cardSliderInfo.component.vue';
import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';

@Component({
  components: { CardSliderInfoComponent, BlockSpinnerComponent, NoDataComponent, ErrorDataComponent }
})
export default class ClientCardSliderSkeletonLoaderComponent extends Vue {
  private slideNum: number;

  constructor () {
    super();
    this.slideNum = 1;
  }

  private update () {
    this.slideNum = 1;
  }

  get carouselSlideCount ():number {
    return !this.$q.screen.lt.xl ? 3 : 2;
  }
}
</script>

<style scoped>
  /* main carousel */
  .list-carousel { height: 434px;
  background: none!important; }

  .card {
    min-width: 320px;
    background: none!important;
  }
  .card-img {
      width: 200px;
      height: 126px;
    }
  .img-container {
    padding-left: 28px;
  }
  .b14 {
    margin-bottom: 14px;
  }
  @media (min-width: 1366px) {
    .card { min-width: 350px; }
  }
  @media (min-width: 1920px) {
    .card {
      min-width: 352px;
      width: 100%;
     }
    .list-carousel { height: 476px; }
    .v-carusel-item {
      width: 42px !important;
      height: 26px !important;
      margin-bottom: 12px!important;
    }
    .img-container {
      padding-left: 32px;
    }
    .card-img {
      width: 232px !important;
      height: 146px !important;
    }
    .info-title {
      padding-top:10px;
    }
    .info-text {
      height: 24px;
    }
  }
</style>
