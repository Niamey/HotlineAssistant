import { Vue } from 'vue-property-decorator';
import Component from 'vue-class-component';
import { countryListMapper } from '../../../store/modules/countryList.module';

const Mapper = Vue.extend({
  computed: countryListMapper.mapGetters({
    CountryList: 'Countries',
    CountryListLoading: 'Loading',
    CountryListException: 'Exception',
    CountryListHasError: 'HasError',
    CountryListSuccess: 'Success',
    CountryListValidationError: 'ValidationError'
  }),
  methods: countryListMapper.mapActions({
    getCountryList: 'getCountryList'
  })
});

@Component
export default class CountryListMixin extends Mapper {
  startLoadingServices!: () => any;
  public async getCountriesList (): Promise<void> {
    await this.getCountryList({});
  }
}
