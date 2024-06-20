<template>
      <div>
          <div class="q-gutter-sm">
            <q-option-group
              class="q-ml-none"
              :options="abroadOptions"
              type="radio"
              v-model="isClientAbroad"
            />
          </div>

          <div class="q-mt-lg">
            <div class="q-mb-md">
              {{ isClientAbroad ? $t('components.client.detail.travels.clientTravelNew.whichСountryThereClient') : $t('components.client.detail.travels.clientTravelNew.whichСountryTripPlanned') }}
            </div>
            <div style="">
              <q-select
                dense
                v-model="dynamicData.travel.countries"
                multiple
                :options="filteredCountries"
                :loading="CountryListLoading"
                stack-label
                filled
                use-input
                input-debounce="0"
                @filter="filterFn"
              >
                <template v-slot:selected-item="scope">
                    <q-chip
                      removable
                      @remove="scope.removeAtIndex(scope.index)"
                      :tabindex="scope.tabindex"
                      dense
                      color="#E9E9EA"
                      text-color="#26272B"
                      class="q-my-xs q-ml-xs q-mr-none country-chip"
                    >
                      {{ scope.opt.countryName }}
                    </q-chip>
                </template>

                <template v-slot:option="scope">
                    <q-item
                        v-bind="scope.itemProps"
                        v-on="scope.itemEvents"
                        dense
                      >
                        <q-item-section avatar>
                          <div :class="`vti__flag ${scope.opt.countryA2.toLowerCase()}`"></div>
                        </q-item-section>
                        <q-item-section>
                          <div class="row items-center" style="max-height: 21px">
                            <div class="col">{{scope.opt.countryName}}</div>
                            <div class="col text-right text-primary" v-if="scope.opt.isRisky">{{ $t('components.client.detail.travels.clientTravelNew.riskyCountry') }}</div>
                          </div>
                        </q-item-section>
                      </q-item>
                </template>
              </q-select>
            </div>
          </div>

          <div class='row q-my-md'>
            <div class='col-6 q-pr-sm'>
              <div>
                 <q-input v-model="data.date" class="date-input" readonly dense filled :label="$t('components.client.detail.travels.clientTravelNew.stayPeriod')"  @click="$refs.qDateProxy.show()">
                  <template v-slot:prepend>
                    <q-icon name="event" class="cursor-pointer">
                      <q-popup-proxy ref="qDateProxy" transition-show="scale" transition-hide="scale">
                        <q-date
                          range
                          @input="qDateInput"
                          v-model="data.qDate"
                          >
                          <div class="row items-center justify-end">
                            <q-btn icon="close" flat round dense v-close-popup text-color="grey-7" />
                          </div>
                        </q-date>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                </q-input>
              </div>
            </div>
            <div class='col-6 q-pl-sm'>
              <div>
                <vue-tel-input v-bind="bindProps" v-model="dynamicData.travel.contactPhone" :placeholder="$t('components.client.detail.travels.clientTravelNew.foreignNumber')" class="rounded"></vue-tel-input>
              </div>
            </div>
          </div>

          <div class="q-mt-sm row text-grey-7 flex-center">
            <q-icon class="col-auto q-mr-xs" name="o_info" />
            <div class="col text-caption">{{ $t('components.client.detail.travels.clientTravelNew.allFieldsRequired') }}</div>
          </div>
      </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
// @ts-ignore
import { VueTelInput } from 'vue-tel-input';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from '../../../../../interfaces/travel/baseStep.interface';
import { mixins } from 'vue-class-component';
import CountryListMixin from '../../../../../mixins/country/list/countryList.mixin';
import { CountryInterface } from '../../../../../interfaces/country';
import { FormatDateFilter } from '../../../../../filters';
import constants from '../../../../../common/constants';

@Component({
  components: {
    VueTelInput
  },
  filters: { FormatDateFilter }
})
export default class ClientTravelNewComponent extends mixins(BaseStepComponent, CountryListMixin) implements BaseStepInterface {
  private isClientAbroad: boolean | null;
  private abroadOptions: Array<any>;
  private bindProps: any;
  private data: any;
  private filteredCountries: Array<CountryInterface>;

  constructor () {
    super();
    /* eslint-disable new-cap */
    this.filteredCountries = [];
    this.bindProps = {
      mode: 'international',
      defaultCountry: 'UA',
      disabledFetchingCountry: false,
      disabled: false,
      disabledFormatting: false,
      placeholder: '',
      required: true,
      autocomplete: 'off',
      validCharactersOnly: true
    };
    this.isClientAbroad = null;
    this.abroadOptions = [
      { label: this.$t('components.client.detail.travels.clientTravelNew.clientPlanningTrip'), value: false },
      { label: this.$t('components.client.detail.travels.clientTravelNew.clientAlreadyAbroad'), value: true }
    ];
    this.data = {};
    this.data.date = '';
    this.data.startDate = '';
    this.data.endDate = '';
    this.data.qDate = '';
  }

  validateStep (): string|string[]|undefined {
    const err:string[] = [];
    if (this.isClientAbroad === null) err.push(this.$t('components.client.detail.travels.clientTravelNew.errors.requiredIsClientAbroad').toString());
    if (this.dynamicData.travel.countries.length === 0) err.push(this.$t('components.client.detail.travels.clientTravelNew.errors.requiredСountry').toString());
    if (!this.dynamicData.travel.contactPhone) err.push(this.$t('components.client.detail.travels.clientTravelNew.errors.requiredPhone').toString());
    if (!this.data.date) err.push(this.$t('components.client.detail.travels.clientTravelNew.errors.requiredPeriod').toString());
    return (err.length > 0) ? err : undefined;
  }

  collectData (): void {
    this.travelInfo.isClientAbroad = this.isClientAbroad;
    this.dynamicData.travel.startTravel = this.data.startDate;
    this.dynamicData.travel.endTravel = this.data.endDate;
    this.dynamicData.travel.isClientAbroad = this.isClientAbroad;
  }

  qDateInput (value: any) {
    if (value instanceof Object) {
      this.data.date = `${FormatDateFilter(value.from, constants.formatting.dateVue)}-${FormatDateFilter(value.to, constants.formatting.dateVue)}`;
      this.data.startDate = value.from;
      this.data.endDate = value.to;
    } else {
      this.data.date = FormatDateFilter(value, constants.formatting.dateVue);
      this.data.startDate = this.data.endDate = value;
    }
  }

  async created () {
    await this.getCountriesList();
    this.filteredCountries = this.CountryList;
  }

  filterFn (val: string, update: any) {
    if (val === '') {
      update(() => {
        this.filteredCountries = this.CountryList;
      });
      return;
    }

    update(() => {
      const searchValue = val.toLowerCase();
      this.filteredCountries = this.CountryList.filter(
        v => {
          if (v.countryName) {
            return v.countryName.toLowerCase().indexOf(searchValue) > -1;
          } else {
            return false;
          }
        }
      );
    });
  }
}
</script>

<style scoped>
.country-chip:hover, .country-chip:hover >>> .q-chip__icon--remove {
  color: var(--q-color-primary)!important;
}
.country-chip >>> .q-chip__icon--remove {
  color: #AAA9A8;
  opacity: 1;
}
.vue-tel-input {
  height: 40px;
  border: none;
  background: #f2f2f2;
  position: relative;
}
.vue-tel-input >>> input {
  background: #f2f2f2;
}
.vue-tel-input:focus-within {
  box-shadow: none;
  border-bottom: 2px solid var(--q-color-primary) !important;
  background: #E6E6E6;
}
.vue-tel-input:focus-within >>> input {
  background: #E6E6E6;
}
.vue-tel-input >>> .vti__dropdown:focus{
  outline: none!important;
}
.vue-tel-input >>> .vti__dropdown-list {
    margin-left: -5.5em;
    width: 300px;
    height: 200px;
}
.date-input >>> input {
  cursor: pointer !important;
}
</style>
