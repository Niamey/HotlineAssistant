import { Component, Vue } from 'vue-property-decorator';

import { AgreementBalanceRequestInterface } from '../../../interfaces/requests/agreement/balance/agreementBalanceRequest.interface';
import { AgreementBalanceResponseInterface } from '../../../interfaces/responses/agreement/balance/agreementBalanceResponse.interface';
import { agreementApi } from '../../../api/agreement.api';
import { ApiWrapperActionHelper } from '../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../models/api/apiResult.model';
import { Store } from 'vuex';

@Component
export default class AgreementBalanceMixin extends Vue {
  public async loadAgreementBalance (store: Store<any>, params:AgreementBalanceRequestInterface) : Promise<void | ApiResultModel<AgreementBalanceResponseInterface>> {
    const promise = agreementApi.getBalanceAsync(params);
    return await ApiWrapperActionHelper.runWithTry<AgreementBalanceResponseInterface>(store, promise);
  }
}