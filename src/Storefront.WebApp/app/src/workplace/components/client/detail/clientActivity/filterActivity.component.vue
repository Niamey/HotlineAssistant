<template>
  <q-popup-proxy anchor="bottom right" self="top right" :offset="[0, 5]" ref="filterPopup" @before-show="beforeShowFilter">
    <q-card class="filter">
      <q-card-section class="q-px-lg q-pb-lg text-body2 column q-gutter-y-md">
        <div class="absolute-right close-btn">
          <q-btn icon="close" flat round dense v-close-popup style="color: #A8A9AA" />
        </div>

        <div class="col-12 text-weight-medium">{{ $t('components.client.detail.clientActivity.filter.title') }}:</div>

        <div class="col-12 row" v-if="issetDateRangeFilter">
          <div class="col-4">
            <q-input filled :value="$t('components.client.detail.clientActivity.filter.dateTitle')" dense readonly />
          </div>
          <div class="col q-pl-md">
            <q-input filled v-model="getDateRange" dense readonly @click="dateRangeClick">
              <template v-slot:prepend>
                <q-icon name="event" class="cursor-pointer">
                  <q-popup-proxy transition-show="scale" transition-hide="scale" ref="dateRangePopup">
                    <q-date v-model="dateRange"
                      range
                      :mask="formatting.date"
                      dense
                      today-btn
                      :options="dateOptions"
                      >
                      <div class="row items-center justify-end">
                        <q-btn v-close-popup :label="$t('components.client.detail.clientActivity.filter.calendar.close')" color="primary" outline no-caps/>
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </template>
            </q-input>
          </div>
          <q-icon class="col-auto q-pl-md q-mt-sm clear-icon cursor-pointer" name="clear" size="sm" @click="removeFilter(filterTypeEnum.DateRange)" />
        </div>

        <div class="col-12 row items-start" v-if="issetCardFilter">
          <q-input class="col-4" :value="$t('components.client.detail.clientActivity.filterTypeEnum.card')" filled dense readonly />
          <q-input class="col q-pl-md" v-model="cardNum" filled dense maxlength="4" mask="####" :label="$t('components.client.detail.clientActivity.filter.cardNumLabel')"
            :error="!isValidCardNum" hide-bottom-space ref="cardNum" />
          <q-icon class="col-auto q-pl-md q-mt-sm clear-icon cursor-pointer" name="clear" size="sm" @click="removeFilter(filterTypeEnum.Card)" />
        </div>
        <div class="col-12 row items-start" v-if="issetOperationFilter">
          <q-input class="col-4" :value="$t('components.client.detail.clientActivity.filterTypeEnum.operation')" filled dense readonly />
          <q-input class="col q-pl-md" v-model.number="amountFrom" :label="$t('components.client.detail.clientActivity.filter.amountFromLabel')" filled dense hide-bottom-space
            :error="!isValidAmountFrom" ref="amountFrom" />
          <q-input class="col q-pl-md" v-model.number="amountTo" :label="$t('components.client.detail.clientActivity.filter.amountToLabel')" filled dense hide-bottom-space
            :error="!isValidAmountTo" ref="amountTo" />
          <q-icon class="col-auto q-pl-md q-mt-sm clear-icon cursor-pointer" name="clear" size="sm"  @click="removeFilter(filterTypeEnum.Operation)" />
        </div>

        <div class="col-12" v-if="getAvailableFilters.length > 0">
          <span class="add-filter row-inline items-center cursor-pointer text-primary"  id="addfilter-link">
            <q-icon name="add" size="sm" /><span class="q-ml-sm">{{ $t('components.client.detail.clientActivity.filter.addNewFilter') }}</span>
          </span>
          <q-popup-proxy ref="addfilter" target="#addfilter-link">
            <q-list style="min-width: 100px">
              <q-item clickable v-for="(item, index) in getAvailableFilters" :key="index" >
                <q-item-section @click="addFilter(item)">{{ getTypeEnumName(item) }}</q-item-section>
              </q-item>
            </q-list>
          </q-popup-proxy>
        </div>

      </q-card-section>

      <q-card-actions class="q-pa-md q-px-lg actions">
        <q-btn color="primary" no-caps class="q-px-md" @click="applyFilters">{{ $t('components.client.detail.clientActivity.filter.applyFilter') }}</q-btn>
        <q-btn color="primary" outline no-caps class="q-px-md" @click="clearFilters">{{ $t('components.client.detail.clientActivity.filter.clearFilter') }}</q-btn>
      </q-card-actions>
    </q-card>
  </q-popup-proxy>
</template>

<script lang="ts">
import { Vue, Component, /* Watch, */ Prop } from 'vue-property-decorator';
import { ClientActivityFilterInterface } from '../../../../interfaces/client/activity/clientActivityFilter.interface';
import { ClientActivityFilterTypeEnum } from '../../../../enums/client/activity/ClientActivityFilterType.enum';
import { QPopupProxy, date as QuasarDate, QInput } from 'quasar';
import constants from '../../../../common/constants';

interface DateRange {
  from: string;
  to: string;
}

@Component
export default class FilterActivityComponent extends Vue {
  @Prop() initFilter?: ClientActivityFilterInterface;

  private filter?: ClientActivityFilterInterface;
  private dateRange: DateRange | string;
  // private defaultRange: DateRange;

  private filterTypeList: string[] = [];
  private cardNum?: string = '';
  private amountFrom?: string = '';
  private amountTo?: string = '';
  private currentDate: Date;

  private filterTypeEnum: any;
  private formatting: any;

  constructor () {
    super();
    this.formatting = constants.formatting;
    this.filterTypeEnum = ClientActivityFilterTypeEnum;
    this.currentDate = new Date();
    this.dateRange = '';
  }

  $refs!: {
    addfilter: QPopupProxy;
    filterPopup: QPopupProxy;
    dateRangePopup: QPopupProxy;
    cardNum: QInput;
    amountFrom: QInput;
    amountTo: QInput;
  }

  private dateRangeClick () {
    this.$refs.dateRangePopup.show();
  }

  get isValidCardNum () {
    return !(this.cardNum && !/^\d{4}$/i.test(this.cardNum));
  }

  get isValidAmountFrom () {
    const isValid = Number.isInteger(parseInt(this.amountFrom ?? ''));
    return !!(!isValid || (isValid && this.amountFrom !== undefined && /^\d*$/i.test(this.amountFrom.toString())));
  }

  get isValidAmountTo () {
    const isValidFrom = Number.isInteger(parseInt(this.amountFrom ?? ''));
    const isValidTo = Number.isInteger(parseInt(this.amountTo ?? ''));

    if ((isValidTo && this.amountTo !== undefined && !/^\d*$/i.test(this.amountTo.toString())) ||
    (isValidFrom && isValidTo && this.amountFrom !== undefined && this.amountTo !== undefined && parseInt(this.amountTo) - parseInt(this.amountFrom) < 0)
    ) {
      return false;
    }
    return true;
  }

  get getFormatCurrentDate () {
    return QuasarDate.formatDate(this.currentDate, this.formatting.dateSlashed);
  }

  private dateOptions (date: any) {
    return date <= this.getFormatCurrentDate;
  }

  private beforeShowFilter () {
    this.currentDate = new Date();
    this.settingInitFilter();
  }

  private settingInitFilter () {
    if (this.initFilter !== undefined && this.initFilter !== null) {
      if (this.initFilter.filters !== undefined && this.initFilter.filters.length > 0) {
        this.filterTypeList = [];
        for (const f of this.initFilter.filters) {
          switch (f.filterType) {
            case ClientActivityFilterTypeEnum.DateRange:
              this.filterTypeList.push(ClientActivityFilterTypeEnum.DateRange);

              if (f.filterValueFrom === undefined || f.filterValueTo === undefined) {
                this.dateRange = '';
                break;
              }

              // eslint-disable-next-line no-case-declarations
              const from = f.filterValueFrom?.toString() || '';
              // eslint-disable-next-line no-case-declarations
              const to = f.filterValueTo?.toString() || '';

              this.dateRange = (from === to) ? from : { from, to };
              break;
            case ClientActivityFilterTypeEnum.Card:
              this.filterTypeList.push(ClientActivityFilterTypeEnum.Card);
              this.cardNum = f.filterValueFrom?.toString();
              break;
            case ClientActivityFilterTypeEnum.Operation:
              this.filterTypeList.push(ClientActivityFilterTypeEnum.Operation);
              this.amountFrom = f.filterValueFrom?.toString();
              this.amountTo = f.filterValueTo?.toString();
              break;
          }
        }
      }
    }
  }

  /*
  @Watch('dateRange', { immediate: true })
  private onDateRangeChange (val: DateRange) {
    if (!val) {
      this.dateRange = Object.assign({}, this.defaultRange);
    }
  }
  */

  get getDateRange () {
    if (this.dateRange === undefined || this.dateRange === null) return '';

    if (typeof this.dateRange === 'string') {
      return QuasarDate.formatDate(this.dateRange, this.formatting.dateVue);
    }

    // @ts-ignore
    const from = QuasarDate.formatDate(this.dateRange?.from, this.formatting.dateVue);
    // @ts-ignore
    const to = QuasarDate.formatDate(this.dateRange?.to, this.formatting.dateVue);
    return `${from} – ${to}`;
  }

  private isValidFilterForm () {
    return (!this.$refs.cardNum?.error ?? true) &&
      (!this.$refs.amountFrom?.error ?? true) &&
      (!this.$refs.amountTo?.error ?? true);
  }

  private applyFilters () {
    if (!this.isValidFilterForm()) {
      return;
    }

    this.filter = { filters: [] };
    // this.filter.filters = [];

    if (this.issetDateRangeFilter && this.dateRange !== undefined && this.dateRange !== null && this.dateRange !== '') {
      this.filter.filters.push({
        filterType: ClientActivityFilterTypeEnum.DateRange,
        filterValueFrom: typeof this.dateRange === 'string' ? this.dateRange : this.dateRange.from,
        filterValueTo: typeof this.dateRange === 'string' ? this.dateRange : this.dateRange.to
      });
    }

    if (this.issetCardFilter && this.cardNum && this.cardNum.length > 0) {
      this.filter.filters.push({
        filterType: ClientActivityFilterTypeEnum.Card,
        filterValueFrom: this.cardNum
      });
    }

    if (this.issetOperationFilter &&
      ((this.amountFrom !== '' && this.amountFrom !== undefined && this.amountFrom !== null) ||
      (this.amountTo !== '' && this.amountTo !== undefined && this.amountTo !== null))) {
      const from = parseInt(this.amountFrom ?? ''), to = parseInt(this.amountTo ?? '');

      this.filter.filters.push({
        filterType: ClientActivityFilterTypeEnum.Operation,
        filterValueFrom: !isNaN(from) ? from : undefined,
        filterValueTo: !isNaN(to) ? to : undefined
      });
    }

    if (this.filter.filters === undefined || this.filter.filters.length === 0) {
      this.filter = undefined;
    }

    this.$refs.filterPopup.hide();
    this.clearData();
    this.$emit('apply-filter', this.filter);
  }

  private clearFilters () {
    this.$refs.filterPopup.hide();
    this.clearData();

    this.filter = undefined;
    this.$emit('apply-filter', this.filter);
  }

  private clearData () {
    this.dateRange = '';
    this.cardNum = undefined;
    this.amountFrom = undefined;
    this.amountTo = undefined;
    this.filterTypeList = [];
  }

  private removeFilter (type: ClientActivityFilterTypeEnum) {
    switch (type) {
      case ClientActivityFilterTypeEnum.DateRange:
        this.dateRange = '';
        break;
      case ClientActivityFilterTypeEnum.Card:
        this.cardNum = undefined;
        break;
      case ClientActivityFilterTypeEnum.Operation:
        this.amountFrom = undefined;
        this.amountTo = undefined;
        break;
    }

    const index = this.filterTypeList.indexOf(type);
    if (index >= 0) {
      this.filterTypeList.splice(index, 1);
    }
  }

  get getEnumArray () {
    const types = [];
    for (const type in ClientActivityFilterTypeEnum) {
      if (type !== 'Undefined') {
        types.push(type);
      }
    }
    return types;
  }

  get getAvailableFilters () {
    return this.getEnumArray.filter(i => this.filterTypeList.indexOf(i) === -1);
  }

  get issetDateRangeFilter () {
    return !!(this.filterTypeList.indexOf(ClientActivityFilterTypeEnum.DateRange) !== -1);
  }

  get issetCardFilter () {
    return !!(this.filterTypeList.indexOf(ClientActivityFilterTypeEnum.Card) !== -1);
  }

  get issetOperationFilter () {
    return !!(this.filterTypeList.indexOf(ClientActivityFilterTypeEnum.Operation) !== -1);
  }

  private getTypeEnumName (item: string) {
    switch (item) {
      case ClientActivityFilterTypeEnum.DateRange: return this.$t('components.client.detail.clientActivity.filterTypeEnum.dateRange').toString();
      case ClientActivityFilterTypeEnum.Card: return this.$t('components.client.detail.clientActivity.filterTypeEnum.card').toString();
      case ClientActivityFilterTypeEnum.Operation: return this.$t('components.client.detail.clientActivity.filterTypeEnum.operation').toString();
    }
  }

  private addFilter (type: string) {
    if (type === ClientActivityFilterTypeEnum.DateRange) {
      if (!constants.clientActivity.filter.defaultDays || constants.clientActivity.filter.defaultDays === 0) {
        this.dateRange = '';
      } else if (constants.clientActivity.filter.defaultDays === 1) {
        this.dateRange = QuasarDate.formatDate(this.currentDate, this.formatting.date);
      } else {
        this.dateRange = {
          from: QuasarDate.formatDate(QuasarDate.subtractFromDate(this.currentDate, { days: constants.clientActivity.filter.defaultDays - 1 }), this.formatting.date),
          to: QuasarDate.formatDate(this.currentDate, this.formatting.date)
        };
      }
    }

    this.filterTypeList.push(type);
    this.$refs.addfilter.hide();
  }
}
</script>

<style scoped>
  .filter {
    width: 492px;
  }

  .filter .q-card__actions--horiz > .q-btn-item + .q-btn-item {
    margin-left: 16px;
  }

  .filter .actions {
    box-shadow: 0 -1px 8px rgb(0 0 0 / 8%), 0 -3px 4px rgb(0 0 0 / 6%), 0 -3px 3px -2px rgb(0 0 0 / 1%);
  }

  .clear-icon {
    color: #A8A9AA;
  }
  .clear-icon:hover {
    color:#000;
  }

  .close-btn {
    top: 8px;
    right: 8px;
  }
</style>
