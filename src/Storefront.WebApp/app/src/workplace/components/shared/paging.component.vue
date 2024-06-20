<template>
  <div class="row justify-end items-center q-pr-md" >
    <block-spinner-component v-if="loading" :loaded="loading" size="1.5em" label="" />
    <span class="col-auto text-body1" v-if="hasData">{{ pageOfTitle }}</span>
    <q-icon
      class="col-auto q-ml-lg text-weight-bold"
      :class="{'cursor-pointer': pager.currentPage > 1, 'inactive': pager.currentPage === 1}"
      name="keyboard_arrow_left"
      size="sm"
      @click="setPage(pager.currentPage - 1)"
    />
    <q-icon
      class="col-auto q-ml-md text-weight-bold"
      :class="{'cursor-pointer': pager.currentPage !== pager.totalPages, 'inactive': pager.currentPage === pager.totalPages}"
      name="keyboard_arrow_right"
      size="sm"
      @click="setPage(pager.currentPage + 1)"
    />
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import BlockSpinnerComponent from '../shared/blockSpinner.component.vue';
import { BaseCollectionInterface } from '../../interfaces/base/baseCollection.interface';
import { paginateUtil } from '../../utils/paginate.util';
import constants from '../../common/constants';

@Component({
  components: { BlockSpinnerComponent }
})
export default class PagingComponent extends Vue {
  @Prop({ required: true }) items!: Array<BaseCollectionInterface>;
  @Prop({ required: true, default: constants.paging.pageIndex }) initialPage!: number;
  @Prop({ required: true, default: constants.paging.pageSize }) pageSize!: number;
  @Prop({ default: constants.paging.maxPages }) maxPages!: number;
  @Prop() loading?: boolean;
  private label = '';
  private pager = {};

  created () {
    // this.setPage(this.initialPage);
  }

  @Watch('items', { immediate: true })
  onChangeItems () {
    this.setPage(this.initialPage);
  }

  private get pageOfTitle () : string {
    return this.label;
  }

  private get hasData () : boolean {
    return this.items.length !== undefined && this.items.length > 0;
  }

  private setPage (page: number) {
    if (page < 1 || page > this.items.length) return;

    const { items, pageSize, maxPages } = this;
    const pager = paginateUtil(items.length, page, pageSize, maxPages);

    this.label = `${pager.startIndex + 1}–${pager.endIndex + 1} ${this.$t('components.shared.pager.label')} ${pager.totalItems}`; // для записей
    // this.label = `${pager.currentPage}–${pager.endPage} ${this.$t('components.shared.pager.label')} ${pager.totalPages}`; // для страниц
    const pageOfItems = items.slice(pager.startIndex, pager.endIndex + 1);
    this.pager = pager;
    // emit change page event to parent component
    this.$emit('changePage', pageOfItems, pager.currentPage);
  }
}
</script>

<style scoped>
  .inactive {
    color: #A8A9AA;
  }
</style>
