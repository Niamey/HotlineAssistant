<template>
  <div class="row inline justify-end items-center content-end" >
    <block-spinner-component v-if="loading" :loaded="loading" size="1.5em" label="" />
    <q-item-label class="column" v-if="hasData">{{pageOfTitle}}</q-item-label>
    <div class="column">
      <q-btn flat round outline class="btn" icon="keyboard_arrow_left"
             :disable="hasBack" @click="onBack()"></q-btn>
    </div>
    <div class="column">
      <q-btn flat round outline class="btn" icon="keyboard_arrow_right"
             :disable="hasNext" @click="onNext()"></q-btn>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import BlockSpinnerComponent from '../shared/blockSpinner.component.vue';
import constants from '../../common/constants';

@Component({
  components: { BlockSpinnerComponent }
})
export default class PagerComponent extends Vue {
  @Prop({ required: true, default: constants.paging.pageIndex }) pageIndex!: number;
  @Prop({ required: true, default: constants.paging.pageSize }) pageSize!: number;
  @Prop() total?: number;
  @Prop() isAvailableNextPage?: boolean;
  @Prop() loading?: boolean;

  private startRows: number;
  private currentPage : number;
  private endRows: number;
  private readonly size: number;
  private readonly rowSize = constants.paging.pageSize;

  constructor () {
    super();
    this.currentPage = this.pageIndex;
    this.size = this.pageSize || this.rowSize;
    this.currentPage = this.pageIndex || constants.paging.pageIndex;
    this.startRows = constants.paging.pageIndex;
    this.endRows = this.pageSize || this.rowSize;
  }

  private get hasPageInFrom () : boolean {
    return this.total !== undefined && this.total >= this.endRows;
  }

  private get pageOfTitle () : string {
    if (this.total === undefined) {
      return '';
    }

    if (this.currentPage > 1) {
      this.onCalc(this.currentPage);
    }

    if (this.total > this.endRows) {
      return `${this.startRows} - ${this.endRows} ${this.$t('components.shared.pager.label')} ${this.total}`;
    } else {
      if (this.total < this.endRows) {
        if (this.startRows === this.total) {
          return `${this.startRows} ${this.$t('components.shared.pager.label')} ${this.total}`;
        }
      }
    }

    return `${this.startRows} - ${this.total} ${this.$t('components.shared.pager.label')} ${this.total}`;
  }

  private get hasData () : boolean {
    return this.total !== undefined && this.total > 0;
  }

  private get hasBack () : boolean {
    return this.currentPage <= 1;
  }

  private get hasNext () : boolean {
    return (this.total !== undefined && (!this.isAvailableNextPage || this.currentPage >= this.total)) || (!this.total && !this.isAvailableNextPage);
  }

  @Watch('total')
  clear () {
    this.startRows = 0;
    this.endRows = 0;
    this.onCalc(1);
    this.currentPage = 1;
  }

  onCalc (pageNum: number) {
    if (pageNum !== constants.paging.pageIndex) {
      const num = pageNum - 1;
      if (num !== undefined) {
        this.startRows = (num * constants.paging.pageSize) + constants.paging.pageIndex;
        this.endRows = pageNum * constants.paging.pageSize;
      }
    } else {
      this.startRows = constants.paging.pageIndex;
      this.endRows = pageNum * this.size;
    }
  }

  onBack () {
    this.currentPage--;
    this.onCalc(this.currentPage);
    this.$emit('onNavigate', this.currentPage);
  }

  onNext () {
    this.currentPage++;
    this.onCalc(this.currentPage);
    this.$emit('onNavigate', this.currentPage);
  }
}
</script>

<style scoped>
.btn {
  color: #7D7D80;
}
</style>
