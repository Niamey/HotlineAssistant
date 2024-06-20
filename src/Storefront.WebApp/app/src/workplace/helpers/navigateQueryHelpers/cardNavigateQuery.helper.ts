import { Route } from 'vue-router';
import constants from '../../common/constants';
// import { utils } from '../../utils/utils';

export class CardNavigateQueryHelper {
  public getCardId (route: Route) : number | null {
    return route.query[constants.navigateQuery.card.cardId] ? parseInt(route.query[constants.navigateQuery.card.cardId].toString()) : null;
  }

  public hasCardId (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.card.cardId];
  }

  public getCardNum (route: Route) : string | null {
    return route.query[constants.navigateQuery.card.cardNum] ? (route.query[constants.navigateQuery.card.cardNum].toString()) : null;
  }

  public hasCardNum (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.card.cardNum];
  }

  public getTabName (route: Route) : string | null {
    return route.query[constants.navigateQuery.card.tabName] ? route.query[constants.navigateQuery.card.tabName].toString() : null;
  }

  public hasTabName (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.card.tabName];
  }
}
