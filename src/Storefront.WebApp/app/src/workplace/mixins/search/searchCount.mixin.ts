import { Component, Vue } from 'vue-property-decorator';
import { searchCountMapper } from '../../store/modules/searchCount.module';
import SearchCountCounterpart from '../../models/search/searchCountCounterpart.model';

const Mapper = Vue.extend({
  computed: searchCountMapper.mapGetters({
    TotalRows: 'TotalRows',
    SearchCountLoading: 'Loading',
    // SearchCountException: 'Exception',
    SearchCountSuccess: 'Success',
    SearchCountHasError: 'HasError',
    SearchCountValidationError: 'ValidationError'
  }),
  methods: searchCountMapper.mapActions({
    findTotalRows: 'findTotalRows',
    startLoadingTotalRows: 'startLoadingTotalRows',
    stopLoadingTotalRows: 'stopLoadingTotalRows'
  })
});

@Component
export default class SearchCountMixin extends Mapper {
 private searchParam: SearchCountCounterpart = new SearchCountCounterpart();

 prepareSearchParam (filter: string | (string | null)[] | undefined, type: string | (string | null)[] | undefined) {
   this.searchParam.searchFilter = filter;
   this.searchParam.searchType = type;
 }

 async getTotalRows (filter: string | (string | null)[] | undefined, type: string | (string | null)[] | undefined) : Promise<void> {
   this.prepareSearchParam(filter, type);
   await this.findTotalRows(this.searchParam);
 }
}
