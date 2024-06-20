import { Route } from 'vue-router';
import constants from '../../common/constants';

export class TravelNavigateQueryHelper {
  public getTravelId (route: Route) : number | null {
    return route.query[constants.navigateQuery.travel.travelId] ? parseInt(route.query[constants.navigateQuery.travel.travelId].toString()) : null;
  }

  public hasTravelId (route: Route) : boolean {
    return !!route.query[constants.navigateQuery.travel.travelId];
  }
}
