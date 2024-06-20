import { Vue, Component } from 'vue-property-decorator';
import { cardTariffListMapper } from '../../../store/modules/cardTariffList.module';

const Mapper = Vue.extend({
  computed: cardTariffListMapper.mapGetters({
    CardTariffs: 'List',
    CardTariffsLoading: 'Loading',
    CardTariffsException: 'Exception',
    CardTariffsSuccess: 'Success',
    CardTariffsHasError: 'HasError'
  }),
  methods: cardTariffListMapper.mapActions({
    getCardTariffs: 'getList',
    startLoadingCardTariffs: 'startLoading',
    stopLoadingCardTariffs: 'stopLoading'
  })
});

@Component
// @ts-ignore
export default class CardTariffListMixin extends Mapper {
  startLoadingCardTariffs!: () => any;
  stopLoadingCardTariffs!: () => any;

  public async loadCardTariffDetail (solarId: number, cardId: number) : Promise<void> {
    await this.getCardTariffs({ solarId, cardId });
  }

  public async getCardTariffList (solarId: number, cardId: number): Promise<void> {
    await this.getCardTariffs({ solarId, cardId });
  }
}
