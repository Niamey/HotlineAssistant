import { Component, Vue } from 'vue-property-decorator';
import { SearchOptionType, SearchQueryType, SelectOptionType } from '../../types';
import { searchClientMapper } from '../../store/modules/searchClient.module';
import SearchCounterpart from '../../models/search/searchCounterpart.model';
import { SortDirectionEnum } from '../../enums/client/search/sortDirection.enum';
import constants from '../../common/constants';

const Mapper = Vue.extend({
  computed: searchClientMapper.mapGetters({
    Clients: 'Clients',
    SearchCategory: 'SearchCategory',
    AvailableNextPage: 'AvailableNextPage',
    SearchClientLoading: 'Loading',
    SearchClientHasError: 'HasError',
    SearchClientSuccess: 'Success',
    SearchClientValidationError: 'ValidationError'
  }),
  methods: searchClientMapper.mapActions({
    setCategory: 'setCategory',
    findClients: 'findClients',
    startLoadingClients: 'startLoading'
  })
});

// @ts-ignore
@Component
export default class ClientSearchMixin extends Mapper {
  startLoadingClients!: () => any;

  public options: Array<SearchOptionType> = [];
  public available: Array<SearchOptionType> = [];
  public selectedOption?: SearchOptionType;
  public selectedAdvOption?: SelectOptionType;
  public selectOptions: Array<SelectOptionType> = [];
  public searchQuery: SearchQueryType = { searchType: 'NONE', searchFilter: '' };
  private searchParams: SearchCounterpart = new SearchCounterpart(constants.paging.pageIndex, constants.paging.pageSize);
  public searchValue = '';

  created () {
    this.options = [
      {
        id: 1,
        code: 'FullName',
        label: this.$t('components.client.clientSearchAdvance.labels.initials').toString(),
        condition: '[^\\d\\+@]+',
        type: 'text'
      },
      {
        id: 2,
        code: 'INN',
        label: this.$t('components.client.clientSearchAdvance.labels.inn').toString(),
        condition: '((\\d{8})|(\\d{10}))',
        type: 'text'
      },
      {
        id: 3,
        code: 'PlasticCard',
        label: this.$t('components.client.clientSearchAdvance.labels.cardNumber').toString(),
        condition: '\\d{16}',
        type: 'soon'
      },
      {
        id: 4,
        code: 'IBAN',
        label: this.$t('components.client.clientSearchAdvance.labels.iban').toString(),
        condition: '[A-Z]{2}[0-9]{27}',
        type: 'soon'
      },
      {
        id: 5,
        code: 'FinancePhone',
        label: this.$t('components.client.clientSearchAdvance.labels.phone').toString(),
        condition: '((\\+[0-9]{1,12})|(\\d{8,12}))',
        type: 'text'
      },
      {
        id: 6,
        code: 'Email',
        label: 'Email',
        condition: '.+@.+',
        type: 'email'
      },
      {
        id: 7,
        code: 'Passport',
        label: this.$t('components.client.clientSearchAdvance.labels.passport').toString(),
        condition: '((\\d{9})|([A-ZА-Я]{2} ?\\d{6}))',
        type: 'text'
      },
      {
        id: 8,
        code: 'Barcode',
        label: this.$t('components.client.clientSearchAdvance.labels.barcode').toString(),
        condition: '\\d{13}',
        type: 'soon'
      },
      {
        id: 9,
        code: 'Energo',
        label: this.$t('components.client.clientSearchAdvance.labels.energo').toString(),
        condition: '\\d{8}',
        type: 'soon'
      }
    ];
    this.selectOptions = [
      { value: 1, label: this.$t('components.client.clientSearchAdvance.options.initials').toString() },
      { value: 2, label: this.$t('components.client.clientSearchAdvance.options.inn').toString() },
      { value: 3, label: this.$t('components.client.clientSearchAdvance.options.cardNumber').toString(), type: 'soon' },
      { value: 4, label: this.$t('components.client.clientSearchAdvance.options.iban').toString(), type: 'soon' },
      { value: 5, label: this.$t('components.client.clientSearchAdvance.options.phone').toString() },
      { value: 6, label: 'Email' },
      { value: 7, label: this.$t('components.client.clientSearchAdvance.options.passport').toString() },
      { value: 8, label: this.$t('components.client.clientSearchAdvance.options.barcode').toString(), type: 'soon' },
      { value: 9, label: this.$t('components.client.clientSearchAdvance.options.energo').toString(), type: 'soon' }
    ];
    this.searchQuery = { searchType: 'NONE', searchFilter: '' };
    this.searchValue = '';
    this.selectedAdvOption = this.selectOptions[0];
    this.searchParams.pageSize = 10;
    this.searchParams.sortColumn = '';
    this.searchParams.sortDirection = SortDirectionEnum.Ascending;
  }

  public getSearchOptions (): Array<SearchOptionType> {
    return this.options;
  }

  public getCategory (type: string | (string | null)[] | undefined): string {
    return this.options.filter(i => i.code === type)[0].label;
  }

  public getAvailableOptionsById (id: number): Array<SearchOptionType> {
    return this.options.filter(i => i.id === id);
  }

  public getAvailableOptions (value: string) : Array<SearchOptionType> {
    const res = this.options.filter(i => new RegExp(`^${i.condition}$`, 'gi').test(value));
    return res.filter(i => i.type !== 'soon');
  }

  onClear () {
    this.searchValue = '';
    this.selectedOption = undefined;
    this.searchQuery = { searchType: 'NONE', searchFilter: '' };
    this.available = [];
  }

  async onSearchClient() {
    if (this.selectedOption === undefined) return;

    this.searchQuery.searchType = this.selectedOption?.code || 'NONE';
    if (this.searchQuery.searchType === 'FinancePhone') {
      switch (this.searchValue.length) {
        case 11: this.searchValue = '3' + this.searchValue;
          break;
        case 10: this.searchValue = '38' + this.searchValue;
          break;
        case 9: this.searchValue = '380' + this.searchValue;
          break;
      }
    }
    this.searchQuery.searchFilter = this.searchValue;
    await this.$router.push({ name: 'searchResult', query: { filter: this.searchQuery.searchFilter, type: this.searchQuery.searchType } }).catch((err:any) => {
      // Ignore the vuex err regarding  navigating to the page they are already on.
      if (err.name !== 'NavigationDuplicated') {
        // But print any other errors to the console
        console.error(err);
      }
    });

    this.$emit('onSearchClient', this.searchQuery);
  }

  onPrepareOptions (item: SelectOptionType) {
    void this.setCategory(item);
    this.$emit('onPrepareOptions', item);
  }

  prepareParams (filter: string | (string | null)[] | undefined,
    type: string | (string | null)[] | undefined, currentPage: number, pageSize?: number, sortColumn?: string, sortDirection?: SortDirectionEnum) {
    this.searchParams.searchFilter = filter;
    this.searchParams.searchType = type;
    this.searchParams.pageIndex = currentPage;
    if (pageSize !== undefined && pageSize !== null) {
      this.searchParams.pageSize = pageSize;
    }
    if (sortColumn !== undefined && sortColumn !== null) {
      this.searchParams.sortColumn = sortColumn;
    }
    if (sortDirection !== undefined && sortDirection !== null) {
      this.searchParams.sortDirection = sortDirection;
    }
  }

  async getClientLists (filter: string | (string | null)[] | undefined,
    type: string | (string | null)[] | undefined, currentPage: number,
    pageSize?: number, sortColumn?: string, sortDirection?: SortDirectionEnum): Promise<void> {
    this.prepareParams(filter, type, currentPage, pageSize, sortColumn, sortDirection);
    await this.findClients(this.searchParams);
  }

  async getClientsByPage (currentPage: number) {
    this.searchParams.pageIndex = currentPage;
    await this.findClients(this.searchParams);
  }
}
