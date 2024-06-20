import { Vue } from 'vue-property-decorator';

import { CardFullBalanceRequestInterface } from '../../../../interfaces/requests/card/balance/full';
import { CardFullBalanceResponseInterface } from '../../../../interfaces/responses/card/balance/full';
import { cardApi } from '../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../models/api/apiResult.model';
import { Store } from 'vuex';
import { Mixin } from 'vue-mixin-decorator';

@Mixin
export default class CardFullBalanceMixin extends Vue {
  public async loadCardFullBalance(store: Store<any>, params: CardFullBalanceRequestInterface): Promise<void | ApiResultModel<CardFullBalanceResponseInterface>> {
    const promise = cardApi.getFullBalanceAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardFullBalanceResponseInterface>(store, promise);
  }
}
