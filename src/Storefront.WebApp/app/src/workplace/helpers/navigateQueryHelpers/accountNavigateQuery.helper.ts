import { Route } from 'vue-router';
import constants from '../../common/constants';
// import { utils } from '../../utils/utils';

export class AccountNavigateQueryHelper {
  public getAccountId (route: Route) : number | null {
    return route.query[constants.navigateQuery.account.accountId] ? parseInt(route.query[constants.navigateQuery.account.accountId].toString()) : null;
  }

  public hasAccountId (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.account.accountId];
  }
}
