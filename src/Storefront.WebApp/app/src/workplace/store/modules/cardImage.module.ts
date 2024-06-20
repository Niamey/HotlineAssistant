import { createMapper, Module, Actions, Context } from 'vuex-smart-module';
import { Store } from 'vuex';
import root from '../root';
import CardImageModel from '../../models/card/cardImage/cardImage.model';
import { CardImageResponseInterface } from '../../interfaces/responses/card/cardImage/cardImageResponse.interface';
import { cardApi } from '../../api/card.api';
import BaseState from '../base/filter/baseFilter.state';
import BaseGetters from '../base/base.getters';
import { CardImageRequestInterface } from '../../interfaces/requests/card/cardImage/cardImageRequest.interface';
import { CardImageInterface } from '../../interfaces/card/cardImage/cardImage.interface';
import { StoreWrapperActionHelper } from '../../helpers/storeWrapperAction.helper';
import BaseMutations from '../base/base.mutations';

class State extends BaseState {
  public images: Array<CardImageModel> = [];
}

class CardImageGetters extends BaseGetters<State> {
  public get List () {
    return this.state.images;
  }

  public get Loading () {
    return this.state.isLoading;
  }
}

class CardImageMutations extends BaseMutations<State> {
  public updateList (payload: CardImageInterface) {
    if (payload.frontUrl !== null) {
      this.state.images.push(payload);
    }
  }

  public startLoading () {
    this.state.isLoading = true;
  }

  public stopLoading () {
    this.state.isLoading = false;
  }
}

class CardImageActions extends Actions<State, CardImageGetters, CardImageMutations, CardImageActions> {
  root!: Context<typeof root>;

  $init (store: Store<any>): void {
    this.root = root.context(store);
  }

  public stopLoading () : void {
    this.mutations.stopLoading();
  }

  public startLoading () : void {
    this.mutations.startLoading();
  }

  public updateList (payload: CardImageInterface) : void {
    this.mutations.startLoading();
    this.mutations.updateList(payload);
    this.mutations.stopLoading();
  }

  public async getImage (params: CardImageRequestInterface) : Promise<void> {
    const promise = cardApi.getCardImageAsync(params).then((resp: CardImageResponseInterface) => {
      this.mutations.updateList(resp.item);
    });
    await StoreWrapperActionHelper.runWithTry(this.root, promise, this.mutations);
  }
}

const cardImageStore = new Module({
  namespaced: true,
  state: State,
  getters: CardImageGetters,
  mutations: CardImageMutations,
  actions: CardImageActions
});

export const cardImageMapper = createMapper(cardImageStore);

export default cardImageStore;