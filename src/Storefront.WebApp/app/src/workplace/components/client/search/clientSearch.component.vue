<template>
  <div class="input-color">
    <q-input color="#f47c20"
             :outlined="bordered"
             :placeholder="$t('search.placeholder')"
             :error-message="this.validationText"
             :error="this.validationText !== ''"
             unmasked-value
             v-model="searchValue"
             debounce="1"
             @keypress.enter.prevent="onEnter"
             @input="onInput">
          <template v-slot:prepend>
            <q-icon name="search" />
          </template>
          <template v-slot:append>
            <q-icon name="close" @click="onClear()"/>
            <q-btn color="primary"
                   @click="onShowMenu(!showAdvanceSearch)"
                   flat round aria-label="Menu">
              <template>
                <q-icon v-if="showAdvanceSearch" name="keyboard_arrow_up" />
                <q-icon v-else name="keyboard_arrow_down" />
                <q-tooltip>
                  {{ $t('components.client.clientSearchAdvance.title') }}
                </q-tooltip>
              </template>
            </q-btn>
          </template>
          <q-menu style="z-index:1;" v-if="showAdvanceSearch" @before-hide="onHideAdvanceForm">
            <client-search-advance-component
              style="width: 39.5em;"
              v-bind:is-value="searchValue !== ''"
              v-on:onPrepareOptions="onPrepareOptions" />
          </q-menu>
        </q-input>
    <q-list bordered class="w"
                v-if="showMenu && available.length > 0"
                transition-show="scale"
                separator
                transition-hide="scale">
            <q-item v-if="available.length > 0">{{ $t('components.client.clientSearch.searchBy') }}:</q-item>
            <q-item clickable
                    v-ripple
                    :key="item.label"
                    v-for="item in available"
                    @click="onSelectItem(item)">
              <q-item-section>
                <q-item-label v-if="item.type !== 'soon'">{{item.label}}</q-item-label>
              </q-item-section>
            </q-item>
        </q-list>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Watch } from 'vue-property-decorator';
import { mixins } from 'vue-class-component';
import ClientSearchAdvanceComponent from './clientSearchAdvance.component.vue';
import ClientSearchMixin from '../../../mixins/search/clientSearch.mixin';
import { SearchOptionType, SelectOptionType } from '../../../types';

@Component({
  components: { ClientSearchAdvanceComponent }
})
export default class ClientSearchComponent extends mixins(ClientSearchMixin) {
  @Prop() bordered?: boolean;
  private showAdvanceSearch: boolean;
  private showMenu: boolean;
  private validationText: string;

  constructor () {
    super();
    this.showAdvanceSearch = false;
    this.showMenu = false;
    this.validationText = '';
  }

  mounted () {
    this.options = this.getSearchOptions();
    this.searchValue = (this.$route.query.filter ?? '').toString();
  }

  onInput (value: string) {
    this.validationText = '';
    this.showMenu = false;
    this.selectedOption = undefined;

    this.available = this.getAvailableOptions(value);
    this.showAdvanceSearch = this.available.length > 1;
    if (this.available.length >= 1 && this.selectedOption === undefined) {
      this.showMenu = true;
      if (this.available.length === 1) {
        this.selectedOption = this.available[0];
      } else {
        this.selectedOption = undefined;
      }
    }
  }

  async onEnter () {
    this.showMenu = false;
    await this.onSearchClient();
  }

  // Error text
  @Watch('SearchClientValidationError')
  onChanged () {
    let errorString = '';
    if (this.SearchClientValidationError !== undefined) {
      if (this.SearchClientValidationError.errors !== undefined) {
        for (let i = 0; i <= this.SearchClientValidationError.errors.length - 1; i++) {
          errorString += this.SearchClientValidationError.errors[i].message;
        }
        this.validationText = `${this.SearchClientValidationError.message} : ${errorString}`;
      }
    }
  }

  async onPrepareOptions (data: SelectOptionType) {
    this.available = this.getAvailableOptionsById(data.value);
    if (this.available.length === 1) {
      this.selectedOption = this.available[0];
      this.showMenu = false;
    }
    await this.onSearchClient();
  }

  setFocus () {
    if (this.selectedOption === undefined) {
      this.showMenu = true;
    }
  }

  onHideAdvanceForm () {
    this.showAdvanceSearch = false;
  }

  async onSelectItem (value: SearchOptionType) {
    this.selectedOption = value;
    this.showMenu = false;
    await this.onSearchClient();
  }

  onShowMenu (value: boolean) {
    this.showAdvanceSearch = value;
    this.showMenu = true;
  }
}
</script>

<style scoped>
.w {
  z-index: 1;
  position: fixed;
  background-color: white;
  width: 39.5em;
  box-shadow: 0px 0.6px 1.8px rgba(0, 0, 0, 0.1), 0px 3.2px 7.2px rgba(0, 0, 0, 0.13);
  border-radius: 4px;

}

.input-color {
  background: white;
  border-radius: 4px;
  width: 39.5em;
}
.input-color >>> .q-field__control,
.input-color >>> .q-field__marginal {
    height: 48px;
}

.input-color >>> .q-field--with-bottom {
  padding: 0;
}
</style>
