import { Vue } from 'vue-property-decorator';
import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import CardModel from '../../models/card/card.model';
import { CardListResponseInterface } from '../../interfaces/responses/card/list';
import { cardApi } from '../../api/card.api';
import BaseFilterMutations from '../base/filter/baseFilter.mutations';
import BaseFilterState from '../base/filter/baseFilter.state';
import BaseGetters from '../base/base.getters';
import CardListRequestModel from '../../models/requests/card/cardListRequest.model';
import { CardFilterInterface } from '../../interfaces/card/cardFilter.interface';
import { CardSliderCollectionModel, CardSliderModel } from '../../models/card/slider';
import { CardCategoryEnum } from '../../enums/card/cardCategory.enum';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import { CardHelper } from '../../helpers/card.helper';

class State extends BaseFilterState {
  public cards: Array<CardModel> = [];
  public filtered: Array<CardModel> = [];

  public cardsForSlider: CardSliderCollectionModel = { items: [] };
}

class CardListGetters extends BaseGetters<State> {
  public get List () {
    return this.state.hasFiltered ? this.state.filtered : this.state.cards;
  }

  public get Loading () {
    return this.state.isLoading;
  }

  public get CardsForSlider () {
    return this.state.cardsForSlider;
  }

  public get CardsCount () {
    return this.state.cards?.length ?? 0;
  }
}

class CardListMutations extends BaseFilterMutations<State> {
  public setList (payload: CardListResponseInterface) {
    this.state.cards = payload.rows;
    super.clearFilter();
  }

  public updateListItem (cardItem: CardModel) {
    if (this.state.cards) {
      const index = this.state.cards.findIndex(card => card.cardId === cardItem.cardId);
      if (index > -1) {
        Vue.set(this.state.cards, index, Object.assign(this.state.cards[index], cardItem));
        this.state.cards = CardHelper.sortCardsByStatus(this.state.cards);
      }
    }
  }

  public startLoading () {
    this.state.isLoading = true;
    this.state.cards = [];
    this.state.cardsForSlider.items = [];
  }

  public stopLoading () {
    this.state.isLoading = false;
  }

  public setFilter (filter: CardFilterInterface) {
    this.state.filtered = this.state.cards.filter(x => (x.agreementId === filter.agreementId || !filter.agreementId) &&
      (x.cardId === filter.cardId || !filter.cardId) &&
      (!filter.cardNum || x.number?.includes(filter.cardNum ?? false)));

    this.state.hasFiltered = true;
  }

  public setCardsForSlider (rows: CardModel[]) {
    let agrFiltered = true, currentAgrId;
    const sortArr = (a:any, b:any) => {
      if (a.category === undefined || b.category === undefined) return 0;

      if (<CardCategoryEnum>a.category === CardCategoryEnum.Primary &&
        <CardCategoryEnum>b.category === CardCategoryEnum.Supplementary) {
        return -1;
      } else if (<CardCategoryEnum>a.category === CardCategoryEnum.Supplementary &&
        <CardCategoryEnum>b.category === CardCategoryEnum.Primary) {
        return 1;
      } else {
        return 0;
      }
    };

    if (rows.length > 0) {
      currentAgrId = rows[0].agreementId;
      for (const card of rows) {
        if (currentAgrId !== card.agreementId) {
          agrFiltered = false;
          break;
        }
        currentAgrId = card.agreementId ?? 0;
      }
    }

    const cardSliderCollection = new CardSliderCollectionModel();
    if (agrFiltered) {
      // сортировка списка карт по параметру категория, сначала основная потом дополнительная
      const sorted:CardModel[] = rows.sort(sortArr);

      for (const card of sorted) {
        const cards = [];
        cards.push(card);

        const cardSlider = new CardSliderModel(cards);
        cardSlider.mainCardId = card.cardId;
        cardSliderCollection.items.push(cardSlider);
      }
    } else {
      // метод должен уметь группировать данные по договору (CardSliderModel)
      // и наполнять коллекцию CardSliderCollectionModel
      const agreementList = new Map();
      for (const card of rows) {
        let cards:CardModel[] = [];
        if (agreementList.has(card.agreementId)) {
          cards = agreementList.get(card.agreementId);
        }
        cards.push(card);
        agreementList.set(card.agreementId, cards);
      }

      for (const cards of agreementList.values()) {
        // сортировка списка карт по параметру категория, сначала основная потом дополнительная
        const sorted:CardModel[] = cards.sort(sortArr);

        const cardSlider = new CardSliderModel(sorted);
        cardSlider.mainCardId = sorted[0].cardId;
        cardSliderCollection.items.push(cardSlider);
      }
    }

    this.state.cardsForSlider = cardSliderCollection;
  }
}

class CardListActions extends Actions<State, CardListGetters, CardListMutations, CardListActions> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public startLoading () : void {
    this.mutations.startLoading();
  }

  public stopLoading () : void {
    this.mutations.stopLoading();
  }

  public async getList (params: CardListRequestModel) : Promise<void> {
    const promise = cardApi.getCardListAsync(params).then((resp: CardListResponseInterface) => {
      this.mutations.setList(resp);
      this.mutations.setCardsForSlider(resp.rows);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }

  public applyFilter (filter: CardFilterInterface) : void {
    this.mutations.setFilter(filter);
    this.mutations.setCardsForSlider(this.state.filtered);
  }

  public updateListItem (card: CardModel) : void {
    this.mutations.updateListItem(card);
  }
}

const cardListStore = new Module({
  namespaced: true,
  state: State,
  getters: CardListGetters,
  mutations: CardListMutations,
  actions: CardListActions
});

export const cardListMapper = createMapper(cardListStore);

export default cardListStore;
