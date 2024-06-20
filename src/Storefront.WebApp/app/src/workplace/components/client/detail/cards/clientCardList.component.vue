<template>
  <div class="q-pa-md">
    <div class="row items-center title" v-if="listData">
      <q-icon name="arrow_back" style="font-size: 24px; cursor: pointer;" class="q-mr-md" @click="clearFilter" v-if="showBack" />
      <div class="col text-h6">{{ getTitle }}</div>
      <div class="col-auto row items-center">
        <client-card-search-component @change="onSearchCard" />
        <div class="view-tabs">
            <q-icon name="menu_book" size="24px" class="cursor-pointer" />
            <q-icon name="view_list" size="24px" class="cursor-pointer" :class="{ active: checkTabView('table') }" @click="tabView='table'" />
            <q-icon name="view_module" size="24px" class="cursor-pointer" :class="{ active: checkTabView('slider') }" @click="tabView='slider'" />
        </div>
      </div>
    </div>

    <q-tab-panels v-model="tabView" animated>
      <q-tab-panel name="table">
        <client-card-table-list-component
          :list-data="this.listData"
          :page-size="10"
          :has-error="this.hasError"
          :is-loading="this.isLoading" />
      </q-tab-panel>

      <q-tab-panel name="slider">
        <client-card-slider-component
          :list-data="getCardsForSlider()"
          :has-error="this.hasError"
          :is-loading="this.isLoading" />
      </q-tab-panel>
    </q-tab-panels>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import BaseListComponent from '../../../base/baseList.component.vue';
import ClientCardSliderComponent from './clientCardSlider.component.vue';
import ClientCardTableListComponent from './clientCardTableList.component.vue';
import CardModel from '../../../../models/card/card.model';
import { mixins } from 'vue-class-component';
import CardSliderMixin from '../../../../mixins/card/list/cardSlider.mixin';
import ClientCardSearchComponent from './clientCardSearch.component.vue';
import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';
import constants from '../../../../common/constants';

@Component({
  components: { ClientCardSliderComponent, ClientCardTableListComponent, ClientCardSearchComponent }
})
export default class ClientCardListComponent extends mixins(BaseListComponent, CardSliderMixin) {
  @Prop({ required: true }) declare listData: Array<CardModel>;
  @Prop() agreementNumber?: string;

  private tabView: string;

  constructor () {
    super();
    this.tabView = 'slider';
  }

  private onSearchCard (cardnum?: string) {
    this.navigateByQueryParam(constants.navigateQuery.card.cardNum, cardnum || false, constants.navigateQuery.card.pageIndex, false);
  }

  get showBack () {
    return !!(NavigateQueryHelper.agreements.hasAgreementId(this.$route));
  }

  get getTitle ():string {
    /*
    return (NavigateQueryHelper.agreements.hasAgreementId(this.$route))
      ? this.$t('components.client.detail.cards.clientCardList.titleFiltered').toString() + ` № ${this.getAgreementNumberById(NavigateQueryHelper.agreements.getAgreementId(this.$route))}`
      : this.$t('components.client.detail.cards.clientCardList.title').toString();
    */

    return (this.agreementNumber)
      ? this.$t('components.client.detail.cards.clientCardList.titleFiltered').toString() + ` № ${this.agreementNumber}`
      : this.$t('components.client.detail.cards.clientCardList.title').toString();
  }

  async clearFilter () {
    await this.navigateBySolarId('clientDetail', parseInt(this.$route.params.id), {});
  }

  private checkTabView (val:string): boolean {
    // eslint-disable-next-line
    return (this.tabView === val) ? true : false;
  }
}
</script>

<style scoped>
  .title {
    position: relative;
    top: -4px;
  }

  /* change view tabs */
  .view-tabs {
    max-width: 350px;
    color: #A8A9AA;
  }
  .view-tabs .active {
    color: #515255 !important;
  }

  .view-tabs .q-icon {
    margin-left: 14px;
  }

  .view-tabs >>> .q-field__control,
  .view-tabs >>> .q-field__prepend {
    height: 32px !important;
  }
  /* end change view tabs */

</style>
