<template>
  <!-- vertical carousel -->
  <div v-if="this.slotData">
    <q-carousel v-if="this.slotData.cardSlots.length > 3"
      v-model="slideNum"
      vertical
      transition-prev="slide-down"
      transition-next="slide-up"
      animated
      padding
      class="thumb-carousel"
      ref="cardCarousel"
    >
      <template v-slot:control>
        <q-carousel-control position="top" class="thumb-prevcontrol row items-start justify-center no-margin" v-show="$refs.cardCarousel && $refs.cardCarousel.value > 1">
          <q-icon name="keyboard_arrow_up" size="24px" @click="$refs.cardCarousel.previous()" class="text-primary cursor-pointer" />
        </q-carousel-control>
        <q-carousel-control position="bottom" class="thumb-nextcontrol row items-end justify-center no-margin" v-show="$refs.cardCarousel && $refs.cardCarousel.value < $refs.cardCarousel.panels.length">
          <q-icon name="keyboard_arrow_down" size="24px" @click="$refs.cardCarousel.next()" class="text-primary cursor-pointer" />
        </q-carousel-control>
      </template>

      <q-carousel-slide v-for="i in carouselSlideCount" :key="i" :name="i" class="column no-wrap items-center justify-start">
        <template v-for="slide in getCards(i-1)">
          <div class="thumb-selected" :key="slide.cardId" v-if="isSelectedCard(slide.cardId)">
            <q-img :src="getCardImg" basic spinner-color="white" :class="[isBlocked(slide) ? 'thumb-img card-wrapper' : 'thumb-img']" />
          </div>
          <q-img v-else :key="slide.cardId" :src="getCardImg" basic spinner-color="white" :class="[isBlocked(slide) ? 'thumb-img card-wrapper' : 'thumb-img']" @click="changeCard(slide.cardId)" />
        </template>
      </q-carousel-slide>

    </q-carousel>
    <!-- end vertical carousel -->

    <div v-else class="column no-wrap items-center justify-start">
      <template v-for="slide in getCards(0)">
        <div class="thumb-selected" :key="slide.cardId" v-if="isSelectedCard(slide.cardId)">
          <q-img :src="getCardImg" :class="[isBlocked(slide) ? 'thumb-img card-wrapper' : 'thumb-img']" basic spinner-color="white"/>
        </div>
        <q-img v-else :key="slide.cardId" :src="getCardImg" basic spinner-color="white" :class="[isBlocked(slide) ? 'thumb-img card-wrapper' : 'thumb-img']" @click="changeCard(slide.cardId)" />
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import { QCarousel } from 'quasar';
import { CardSliderModel } from '../../../../../models/card/slider';
import CardImageHelper from '../../../../../helpers/cardImage.helper';
import { CardInterface } from '../../../../../interfaces/card';
import { CardStatusEnum } from '../../../../../enums/card/cardStatus.enum';
import CardModel from '../../../../../models/card/card.model';

// import CardModel from '../../../../../models/card/card.model';

@Component
export default class CardSliderSlotComponent extends CardImageHelper {
  @Prop({ required: true }) slotData!: CardSliderModel;
  @Prop({ required: true }) cardCode!: string;
  @Prop({ required: true }) currentCard!: CardInterface;

  private cards?: any[];
  private slides:any[] = [];
  private slideNum: number;
  private selectedCardId: number;

  $refs!: {
    cardCarousel: QCarousel;
  }

  constructor () {
    super();
    this.slideNum = 1;
    this.selectedCardId = 0;
  }

  created () {
    this.cards = [...this.slotData.cardSlots];
    this.selectedCardId = this.slotData.mainCardId || 0;
  }

  get carouselSlideCount ():number {
    return Math.ceil(this.slotData.cardSlots.length / 3);
  }

  get getCardImg () {
    return this.getCardImageHelper({ cardCode: this.cardCode || '' });
  }

  private isBlocked (card: CardModel): boolean {
    return card.status !== CardStatusEnum.Active;
  }

  getCards (i:number) {
    if (!this.slides[i]) {
      this.slides[i] = [];

      if (this.cards) {
        for (let k = 0; k < 3 && this.cards?.length > 0; k++) {
          this.slides[i].push(this.cards?.shift());
        }
      }
    }
    return this.slides[i];
  }

  isSelectedCard (cardId:number): boolean {
    return this.selectedCardId === cardId;
  }

  changeCard (cardId:number) {
    this.selectedCardId = cardId;
    this.$emit('changeCard', cardId);
  }
}
</script>

<style scoped>
  /* vertical thumb */
  .thumb-img {
    width: 32px !important;
    height: 20px !important;
    border-radius: 2px;
    cursor: pointer;
  }
  @media (min-width: 1920px) {
    .thumb-img {
      width: 42px !important;
      height: 26px !important;
      border-radius: 3px;
    }
  }
  .thumb-img + .thumb-img {
    margin-top: 13px;
  }

  .thumb-prevcontrol i.q-icon {
    top: -8px;
  }

  .thumb-nextcontrol i.q-icon {
    bottom: -8px;
  }

  .thumb-carousel {
    height: 126px;
  }
  @media (min-width: 1920px) {
    .thumb-carousel {
      height: 145px;
    }
  }

  .thumb-carousel >>> .q-carousel__slide {
    padding: 19px 0 !important;
    justify-content: flex-start;
  }
  @media (min-width: 1920px) {
    .thumb-carousel >>> .q-carousel__slide {
      padding: 20px 0 !important;
    }
  }

  .thumb-selected {
    padding: 3px;
    border: 1px solid #F47C20;
    border-radius: 4px;
    width: 40px !important;
  }
  @media (min-width: 1920px) {
    .thumb-selected  {
      width: 50px !important;
    }
  }

  .thumb-selected:not(:first-child),
  .thumb-selected + .thumb-img {
    margin-top: 9px;
  }
  .card-wrapper{
    position: relative;
    filter: grayscale(100%);
  }
  .card-wrapper::after{
    content: '';
    position: absolute;
    top: 15%;
    left: 0;
    right: 0;
    margin-left: auto;
    margin-right: auto;
    width: 20%;
    height: 32%;
    background-size: 100%;
    background-image: url('~assets/images/icons/lock.svg');
  }
  /* end vertical thumb */
</style>
