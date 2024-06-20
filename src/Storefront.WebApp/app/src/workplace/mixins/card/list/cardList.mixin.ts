import { Component, Vue } from 'vue-property-decorator';
import { Route } from 'vue-router';
import CardModel from '../../../models/card/card.model';
import { cardListMapper } from '../../../store/modules/cardList.module';
import { agreementDetailMapper } from '../../../store/modules/agreementDetail.module';
import { agreementListMapper } from '../../../store/modules/agreementList.module';
import { CardFilterInterface } from '../../../interfaces/card/cardFilter.interface';
import { NavigateQueryHelper } from '../../../helpers/navigateQuery.helper';

const Mapper = Vue.extend({
  computed: {
    ...cardListMapper.mapGetters({
      Cards: 'List',
      CardsCount: 'CardsCount',
      CardsForSlider: 'CardsForSlider',
      CardsLoading: 'Loading',
      CardsSuccess: 'Success',
      CardsHasError: 'HasError',
      CardsException: 'Exception',
      CardsValidationError: 'ValidationError'
    }),
    ...agreementDetailMapper.mapGetters({ AgreementDetail: 'Detail' }),
    ...agreementListMapper.mapGetters({ AgreementList: 'List' })
  },
  methods: cardListMapper.mapActions({
    getCardList: 'getList',
    startLoadingCards: 'startLoading',
    stopLoadingCards: 'stopLoading',
    applyFilterCards: 'applyFilter',
    updateCardListItem: 'updateListItem'
  })
});

@Component
export default class CardListMixin extends Mapper {
  startLoadingCards!: () => any;
  stopLoadingCards!: () => any;
  applyFilterCards!: (filter: CardFilterInterface) => any;

  beforeRouteUpdate(to: Route, from: Route, next: any) {
    if (NavigateQueryHelper.cards.getCardId(to) !== NavigateQueryHelper.cards.getCardId(from) ||
      NavigateQueryHelper.accounts.getAccountId(to) !== NavigateQueryHelper.accounts.getAccountId(from) ||
      NavigateQueryHelper.agreements.getAgreementId(to) !== NavigateQueryHelper.agreements.getAgreementId(from)) {
      this.$bus.emit('card-aggregator-detail-update', undefined);

      if (this.Cards?.length === 0) {
        this.$bus.emit('card-aggregator-list-update');
      }

      this.filteringCardList(to);
    } else {
      if (NavigateQueryHelper.cards.getCardNum(to) !== NavigateQueryHelper.cards.getCardNum(from)) {
        if (NavigateQueryHelper.cards.hasCardNum(from)) {
          this.$bus.emit('card-aggregator-detail-update', undefined);
        }
        this.filteringCardList(to);
      }
    }

    next();
  }

  public async onCardListUpdate() {
    this.$bus.emit('card-aggregator-detail-update', undefined);
    await this.getClientCardList(parseInt(this.$route.params.id));
  }

  public async getClientCardList(solarId: number): Promise<void> {
    await this.getCardList({ solarId });
    this.filteringCardList(this.$route);
  }

  public updateCardList(card: CardModel): void {
    this.updateCardListItem(card);
  }

  private static prepareFilterFromRoute(route: Route) {
    return {
      agreementId: NavigateQueryHelper.agreements.getAgreementId(route),
      accountId: NavigateQueryHelper.accounts.getAccountId(route),
      cardId: NavigateQueryHelper.cards.getCardId(route),
      cardNum: NavigateQueryHelper.cards.getCardNum(route)
    };
  }

  private filteringCardList(route: Route) {
    const searchFilter = CardListMixin.prepareFilterFromRoute(route);
    this.applyFilterCards(searchFilter);

    if (this.Cards?.length === 1 || NavigateQueryHelper.cards.hasCardId(route) /* условие с agreement.Id временно так как ключ сегда один */) {
      this.$bus.emit('card-aggregator-detail-update', this.Cards[0]);
    }
  }

  public getAgreementNumberById(id: number | null) {
    if (id === null) {
      return null;
    }

    if (this.AgreementList.length > 0) {
      return this.AgreementList.find(i => i.agreementId === id)?.agreementNumber;
    } else {
      return this.AgreementDetail?.agreementNumber;
    }
  }
}
