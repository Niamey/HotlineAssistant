import { Component, Vue } from 'vue-property-decorator';

import { CardBalanceRequestInterface } from '../../../interfaces/requests/card/balance';
import { CardBalanceResponseInterface } from '../../../interfaces/responses/card/balance';
import { cardApi } from '../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../models/api/apiResult.model';
import { Store } from 'vuex';

@Component
export default class CardBalanceMixin extends Vue {
  public async loadCardBalance (store: Store<any>, params:CardBalanceRequestInterface) : Promise<void | ApiResultModel<CardBalanceResponseInterface>> {
    const promise = cardApi.getBalanceAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardBalanceResponseInterface>(store, promise);
  }
}
