import { Component, Vue } from 'vue-property-decorator';

import { CardDebtToBankRequestInterface } from '../../../interfaces/requests/card/debtToBank/cardDebtToBankRequest.interface';
import { CardDebtToBankResponseInterface } from '../../../interfaces/responses/card/debtToBank/cardDebtToBankResponse.interface';
import { cardApi } from '../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../models/api/apiResult.model';
import { Store } from 'vuex';

@Component
export default class CardDebtToBankDetailMixin extends Vue {
  public async loadCardDebtToBankDetail (store: Store<any>, params: CardDebtToBankRequestInterface) : Promise<void | ApiResultModel<CardDebtToBankResponseInterface>> {
    const promise = cardApi.getDebtToBankAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardDebtToBankResponseInterface>(store, promise);
  }
}