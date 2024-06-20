import Vue from 'vue';
import Vuex from 'vuex';
// import createPersistedState from 'vuex-persistedstate';
// import Router from '../../router/index';
import { IUserState } from './modules/user.module';
// import * as Vuex from 'vuex';
import { createStore } from 'vuex-smart-module';
import rootStore from './root';
/* Modules */
// import { IUserState } from './modules/user.module';
/*
import { ISideBarState } from './modules/sideBar.module';
import { IServiceListState } from './modules/serviceList.module';

import { ISearchCountState } from './modules/searchCount.module';
import { ISearchClientState } from './modules/searchClient.module';
import { IClientDetailState } from './modules/clientDetail.module';
import { IAgreementListState } from './modules/agreementList.module';
import { ICardListState } from './modules/cardList.module';
import { IAccountListState } from './modules/accountList.module';
import { IAgreementDetailState } from './modules/agreementDetail.module';
import { ICardDetailState } from './modules/cardDetail.module';
*/
Vue.use(Vuex);
// The 1st argument is root module.
// Vuex store options should be passed to the 2nd argument.
const debug = process.env.NODE_ENV !== 'production';
export interface IRootState {
  user: IUserState
  /* sideBar: ISideBarState,
  services: IServiceListState,
  user: IUserState,
  searchCount: ISearchCountState,
  searchClient: ISearchClientState,
  clientDetail: IClientDetailState,
  agreementList: IAgreementListState,
  agreementDetail: IAgreementDetailState,
  cardList: ICardListState,
  cardDetail: ICardDetailState,
  accountList: IAccountListState */
}
// @ts-ignore

export const newStore = new Vuex.Store<IRootState>({
  strict: debug
/*
  mutations: {
    NAVIGATE_TO_ROUTE (state, route) {
      // @ts-ignore
      void Router.push(route);
    }
  },
  plugins: [
    createPersistedState({
      // paths: ['Auth'],
      key: 'hotline_jwt',
      storage: {
        getItem: (key: string) => {
          return sessionStorage.getItem(key);
        },
        setItem: (key, value) => {
          sessionStorage.setItem(key, value);
        },
        removeItem: key => sessionStorage.removeItem(key)
      }
    })
  ] */
});

const Store = createStore(rootStore,
  {
    strict: debug
  });

export default Store;
/* import Vue from 'vue';
import * as Vuex from 'vuex';
import { createStore } from 'vuex-smart-module';
import rootStore from './root';

Vue.use(Vuex);

// The 1st argument is root module.
// Vuex store options should be passed to the 2nd argument.
const debug = process.env.NODE_ENV !== 'production';

const Store = createStore(rootStore,
  {
    strict: debug
  });

export default Store;
*/
