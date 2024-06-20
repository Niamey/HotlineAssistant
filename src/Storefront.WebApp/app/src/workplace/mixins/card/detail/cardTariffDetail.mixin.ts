import { Vue, Component } from 'vue-property-decorator';
import { cardTariffDetailMapper } from '../../../store/modules/cardTariffDetail.module';

const Mapper = Vue.extend({
  computed: cardTariffDetailMapper.mapGetters({
    CardTariffDetail: 'Detail',
    CardTariffDetailLoading: 'Loading',
    CardTariffDetailException: 'Exception',
    CardTariffDetailSuccess: 'Success',
    CardTariffDetailHasError: 'HasError'
  }),
  methods: cardTariffDetailMapper.mapActions({
    getCardTariffDetail: 'getDetail',
    startLoadingCardTariffDetail: 'startLoading',
    stopLoadingCardTariffDetail: 'stopLoading'
  })
});

@Component
// @ts-ignore
export default class CardTariffDetailMixin extends Mapper {
  stopLoadingCardTariffDetail!: () => any;
  startLoadingCardTariffDetail!: () => any;

  public async loadCardTariffDetail (solarId: number, cardId: number) : Promise<void> {
    await this.getCardTariffDetail({ solarId, cardId });
  }
}
