<template>
  <q-input
    v-model="cardNum"
    outlined
    dense
    clearable
    type="text"
    mask="#### #### #### ####"
    unmasked-value
    :placeholder="placeholder"
    class="search-card"
    :class="{ 'long-field': isFocus }"
    @focus="onFocus"
    @blur="onBlur"
    @keypress.enter.prevent="searchCard"
    @clear="searchCard"
  >
    <template v-slot:prepend>
      <q-icon name="search" />
    </template>
  </q-input>
</template>

<script lang="ts">
import { Vue, Component, Watch } from 'vue-property-decorator';
// import RouteHelper from '../../../../mixins/routing/router.mixin';
import { NavigateQueryHelper } from '../../../../helpers/navigateQuery.helper';

@Component
export default class ClientCardSearchComponent extends Vue {
  private cardNum: string;
  private placeholder: string;
  private isFocus!: boolean;

  constructor () {
    super();
    this.cardNum = NavigateQueryHelper.cards.getCardNum(this.$route) ?? '';
    this.placeholder = this.$t('components.client.detail.cards.clientCardSearch.search').toString();
    // this.isFocus = false;
  }

  @Watch('cardNum', { immediate: true })
  onChangeCardNum (newVal:string) {
    this.isFocus = !!(newVal !== '' && newVal !== null);
  }

  private onFocus () {
    this.placeholder = this.$t('components.client.detail.cards.clientCardSearch.searchFocus').toString();
    this.isFocus = true;
  }

  private onBlur () {
    this.placeholder = this.$t('components.client.detail.cards.clientCardSearch.search').toString();
    this.isFocus = !!(this.cardNum !== '' && this.cardNum !== null);
  }

  private searchCard () {
    this.$emit('change', this.cardNum);
  }
}
</script>

<style scoped>
  /* search card input field */
  .search-card {
    width: 100px;
    font-size: 14px;
    transition: width .2s;
  }
  .search-card.long-field {
    width: 225px;
  }
  @media (min-width: 1920px) {
    .search-card {
      width: 105px;
    }
    .search-card.long-field {
      width: 245px;
    }
  }
  .search-card >>> .q-field__control {
    padding: 0 7px;
    transition: background-color .2s;
  }
  .search-card >>> .q-field__prepend  {
    padding-right: 11px;
  }
  .search-card >>> .q-field__control:hover{
    background-color:rgba(0, 0, 0, 0.05)
  }
  .search-card >>> .q-field__control:before,
  .search-card >>> .q-field__control:after {
    content: none;
  }
  /* end search card input field */
</style>
