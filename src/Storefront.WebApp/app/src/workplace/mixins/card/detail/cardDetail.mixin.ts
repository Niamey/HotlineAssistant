import { Component, Vue } from 'vue-property-decorator';
import { cardDetailMapper } from '../../../store/modules/cardDetail.module';
import CardModel from '../../../models/card/card.model';
import CardListMixin from '../../../mixins/card/list/cardList.mixin';
import { mixins } from 'vue-class-component';

const Mapper = Vue.extend({
  computed: cardDetailMapper.mapGetters({
    CardDetail: 'Detail',
    CardDetailLoading: 'Loading',
    // CardDetailException: 'Exception',
    CardDetailSuccess: 'Success',
    CardDetailHasError: 'HasError'
  }),
  methods: cardDetailMapper.mapActions({
    updateCardDetail: 'updateDetail',
    getCardDetail: 'getDetail',
    startLoadingCardDetail: 'startLoading',
    stopLoadingCardDetail: 'stopLoading'
  })
});

@Component
export default class CardDetailMixin extends mixins(Mapper, CardListMixin) {
  startLoadingCardDetail!: () => any;
  stopLoadingCardDetail!: () => any;

  public onCardDetailUpdate (payload : CardModel) {
    void this.updateCardDetail(payload);
  }

  public async loadCardDetail (solarId: number, cardId: number) : Promise<void> {
    if (this.CardDetail?.solarId !== solarId || this.CardDetail?.cardId !== cardId) {
      await this.getCardDetail({ solarId, cardId });
    }
  }

  public async refreshCardDetail () : Promise<void> {
    if (this.CardDetail) {
      const solarId = this.CardDetail.solarId || 0;
      const cardId = this.CardDetail.cardId;
      await this.getCardDetail({ solarId, cardId });
      this.updateCardList(this.CardDetail);
    }
  }
}
