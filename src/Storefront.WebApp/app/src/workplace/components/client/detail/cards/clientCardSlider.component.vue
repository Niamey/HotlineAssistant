<template>
  <div class="full-width" v-if="this.showLoading">
        <client-card-slider-skeleton-loader-component />
  </div>

  <div v-else-if="showError" style="min-height: 122px;" class="full-width row flex-center" >
    <error-data-component  />
  </div>

  <q-carousel v-else-if="carouselSlideCount"
    v-model="slideNum"
    transition-prev="slide-right"
    transition-next="slide-left"
    animated
    padding
    class="list-carousel"
    ref="listCarousel"
    :key="cardSliderInfoCount"
  >
    <template v-slot:control>
      <q-carousel-control position="bottom" class="nav-block no-margin row justify-center items-center" v-if="$refs.listCarousel" >
        <span class="shadow-2 nav-block-icon row flex-center" v-show="$refs.listCarousel.value > 1">
          <q-icon name="keyboard_arrow_left" size="16px" class="text-primary cursor-pointer" @click="$refs.listCarousel.previous()"  />
        </span>
        <span class="shadow-2 nav-block-icon nav-block-lens row flex-center" v-if="$refs.listCarousel.panels.length > 1">
          <q-icon v-for="i in $refs.listCarousel.panels.length"
            :key="i"
            name="lens"
            size="7px"
            class="cursor-pointer"
            :class="{ 'text-primary' : $refs.listCarousel.value === i }"
            @click="$refs.listCarousel.goTo(i)" />
        </span>
        <span class="shadow-2 nav-block-icon row flex-center" v-show="$refs.listCarousel.value < $refs.listCarousel.panels.length">
          <q-icon name="keyboard_arrow_right" size="16px" class="text-primary cursor-pointer" @click="$refs.listCarousel.next()" />
        </span>
      </q-carousel-control>
    </template>

    <q-carousel-slide v-for="i in carouselSlideCount" :key="i" :name="i" class="column no-wrap">
      <div class="row fit justify-start items-start q-gutter-x-xs no-wrap">
          <card-slider-info-component v-for="slide in getCardSliderInfo(i-1)" :key="slide.key" class="col-4" :slideData="slide.data"
                                    v-bind:detailData="listData"
                                    v-bind:hasError="hasError"
                                    v-bind:isLoading="isLoading"/>
      </div>
    </q-carousel-slide>
  </q-carousel>

  <div v-else style="height: 122px;" class="full-width row flex-center" >
    <no-data-component />
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import { QCarousel } from 'quasar';
import CardSliderInfoComponent from './slider/cardSliderInfo.component.vue';
import { CardSliderModel } from '../../../../models/card/slider';
import BaseListComponent from '../../../base/baseList.component.vue';
import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import ClientCardSliderSkeletonLoaderComponent from './clientCardSliderSkeletonLoader.component.vue';

@Component({
  components: { CardSliderInfoComponent, BlockSpinnerComponent, NoDataComponent, ErrorDataComponent, ClientCardSliderSkeletonLoaderComponent }
})
export default class ClientCardSliderComponent extends BaseListComponent {
  @Prop({ required: true }) declare listData: CardSliderModel[];
  private slideNum: number;
  private cardSliderList?: CardSliderModel[];
  private slides:any[] = [];
  private key:number;

  $refs!: {
    listCarousel: QCarousel;
  }

  constructor () {
    super();
    this.slideNum = 1;
    this.key = 0;
  }

  created () {
    this.cardSliderList = [...this.listData];
  }

  private update () {
    this.slideNum = 1;
    this.slides = [];
    this.key = 0;
    this.cardSliderList = [...this.listData];
  }

  get cardSliderInfoCount ():number {
    return !this.$q.screen.lt.xl ? 3 : 2;
  }

  get carouselSlideCount ():number {
    this.update();
    return Math.ceil(this.listData.length / this.cardSliderInfoCount);
  }

  getCardSliderInfo (i:number) {
    if (!this.slides[i]) {
      this.slides[i] = [];

      if (this.cardSliderList) {
        for (let k = 0; k < this.cardSliderInfoCount && this.cardSliderList?.length > 0; k++) {
          this.slides[i].push({ key: this.key++, data: this.cardSliderList?.shift() });
        }
      }
    }

    return this.slides[i];
  }
}
</script>

<style scoped>
  /* navigation block */
  .nav-block {
    height: 24px;
    margin-bottom: 4px !important;
  }
  .nav-block .nav-block-icon {
    border-radius: 12px;
  }
  .nav-block span.nav-block-icon {
    height: 24px;
    min-width: 24px;
  }
  .nav-block span.nav-block-lens {
    padding-left: 15px;
    padding-right: 15px;
    margin: 0 24px;
  }
  .nav-block span.nav-block-lens .q-icon:not(:first-child) {
    margin-left: 7px;
  }
  .nav-block span.nav-block-icon .q-icon {
    color: #D4D4D5;
  }
  /* end navigation block */

  /* main carousel */
  .list-carousel { height: 434px; }
  @media (min-width: 1920px) {
    .list-carousel { height: 476px; }
  }
  .list-carousel >>> .q-carousel__slide {
    padding: 5px 10px 20px 10px;
  }
</style>
