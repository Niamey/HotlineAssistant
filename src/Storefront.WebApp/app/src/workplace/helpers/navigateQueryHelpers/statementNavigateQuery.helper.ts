import { Route } from 'vue-router';
import constants from '../../common/constants';
// import { utils } from '../../utils/utils';

export class StatementNavigateQueryHelper {
  public getAgreementId (route: Route) : number | null {
    return route.query[constants.navigateQuery.agreement.agreementId] ? parseInt(route.query[constants.navigateQuery.agreement.agreementId].toString()) : null;
  }

  public getClientId (route: Route) : number | null {
    return route.query[constants.navigateQuery.tariff.clientId] ? parseInt(route.query[constants.navigateQuery.tariff.clientId].toString()) : null;
  }
}