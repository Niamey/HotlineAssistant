
import { Route } from 'vue-router';
import constants from '../../common/constants';
import { utils } from '../../utils/utils';

export class AgreementNavigateQueryHelper {
  public getAgreementId (route: Route) : number | null {
    return route.query[constants.navigateQuery.agreement.agreementId] ? parseInt(route.query[constants.navigateQuery.agreement.agreementId].toString()) : null;
  }

  public hasAgreementId (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.agreement.agreementId];
  }

  public getIsDebt (route: Route) : boolean {
    return route.query[constants.navigateQuery.agreement.isDebt] ? utils.stringToBoolean(route.query[constants.navigateQuery.agreement.isDebt].toString()) : false;
  }

  public hasIsDebt (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.agreement.isDebt];
  }

  public getIsInactive (route: Route) : boolean {
    return route.query[constants.navigateQuery.agreement.isInactive] ? utils.stringToBoolean(route.query[constants.navigateQuery.agreement.isInactive].toString()) : false;
  }

  public hasIsInactive (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.agreement.isInactive];
  }

  public getTabName (route: Route) : string | null {
    return route.query[constants.navigateQuery.agreement.tabName] ? route.query[constants.navigateQuery.agreement.tabName].toString() : null;
  }

  public hasTabName (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.agreement.tabName];
  }
}
