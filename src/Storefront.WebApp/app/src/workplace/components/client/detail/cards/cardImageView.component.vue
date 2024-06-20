<template>
<block-spinner-component v-if="isLoading" :loaded="isLoading" />
  <no-data-component v-else-if="showNoData" />
  <error-data-component v-else-if="hasError" />
  <div @click="navigate" v-else>
        <q-skeleton type="rect" height="125px" v-if="CardImageLoading" />
        <q-img
          :src="getCardImg"
          spinner-color="white"
          style="max-width: 241px"
          img-class="card-image"
          :class="[isBlocked ? 'rounded-borders card-wrapper' : 'rounded-borders']"
        >
          <div class="caption absolute-bottom  text-left column">
              <div class="text-subtitle1 text-left col card-num">{{ detailData.number | formatCardNumber }}</div>
              <div class="text-caption col row">
                <div class="col-shrink card-name">{{ detailData.embossedName }}</div>
                <div class="text-right col card-exp">{{ detailData.expired | formatDate(formatting.expiryDate) }}</div>
              </div>
            </div>
        </q-img>
      </div>
</template>

<script lang="ts">
import BaseDetailComponent from '../../../base/baseDetail.component.vue';
import { FormatCardNumberFilter, FormatDateFilter, FormatMoneyFilter } from '../../../../filters';
import { CardInterface } from '../../../../interfaces/card';
import { Component, Prop } from 'vue-property-decorator';
import constants from '../../../../common/constants';
import BlockSpinnerComponent from '../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../shared/errorData.component.vue';
import { CardStatusEnum } from '../../../../enums/card/cardStatus.enum';
import CardImageHelper from '../../../../helpers/cardImage.helper';
import { mixins } from 'vue-class-component';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent },
  filters: { FormatCardNumberFilter, FormatMoneyFilter, FormatDateFilter }
})
export default class CardImageViewComponent extends mixins(BaseDetailComponent, CardImageHelper) {
  @Prop({ required: true }) declare detailData: CardInterface;

  private formatting: any;

  constructor () {
    super();
    this.formatting = constants.formatting;
  }

  private navigate () {
    this.$emit('navigate');
  }

  private get isBlocked () {
    return this.detailData.status !== CardStatusEnum.Active;
  }

  get getCardImg () {
    return this.getCardImageHelper({ cardCode: this.detailData?.cardCode || '' });
  }
}
</script>

<style scoped>
.caption {
    /* background: linear-gradient(180deg, rgba(224, 88, 74, 0.8) 0%, rgba(223, 88, 75, 0.8) 6.67%, rgba(218, 88, 77, 0.8) 13.33%, rgba(210, 88, 80, 0.8) 20%, rgba(199, 88, 85, 0.8) 26.67%, rgba(185, 88, 92, 0.8) 33.33%, rgba(168, 88, 99, 0.8) 40%, rgba(149, 88, 108, 0.8) 46.67%, rgba(129, 87, 116, 0.8) 53.33%, rgba(110, 87, 125, 0.8) 60%, rgba(93, 87, 132, 0.8) 66.67%, rgba(79, 87, 139, 0.8) 73.33%, rgba(68, 87, 144, 0.8) 80%, rgba(60, 87, 147, 0.8) 86.67%, rgba(55, 87, 149, 0.8) 93.33%, rgba(54, 87, 150, 0.8) 100%); */
    background:rgba(0, 0, 0, 0.15);
    backdrop-filter: blur(7px);
    border-radius: 0 0 8px 8px;
    padding: 10px;
  }
  .card-num {
    font-size: 12px;
    line-height: 20px;
  }
  .card-name, .card-exp {
    font-size: 10px;
    line-height: 18px;
  }

  .blue {
    color: #0078D4;
  }
  .col.text-subtitle1.blue {
    line-height: 16px;
  }
  .detail-wrapper {
    background-color: #F8F9F9;
  }
  .tbl > div:nth-child(2n+1),
  .gray {
    color: #7D7D80
  }
  .blured {
    filter: blur(3px);
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
</style>
