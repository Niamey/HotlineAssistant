import { Route } from 'vue-router';
import constants from '../../common/constants';
// import { utils } from '../../utils/utils';

export class TariffNavigateQueryHelper {
  public getCardId (route: Route) : number | null {
    return route.query[constants.navigateQuery.card.cardId] ? parseInt(route.query[constants.navigateQuery.card.cardId].toString()) : null;
  }

  public getAgreementId (route: Route) : number | null {
    return route.query[constants.navigateQuery.agreement.agreementId] ? parseInt(route.query[constants.navigateQuery.agreement.agreementId].toString()) : null;
  }

  public getClientId (route: Route) : number | null {
    return route.query[constants.navigateQuery.tariff.clientId] ? parseInt(route.query[constants.navigateQuery.tariff.clientId].toString()) : null;
  }

  public getTariffType (route: Route) : string | null {
    return route.query[constants.navigateQuery.tariff.type] ? route.query[constants.navigateQuery.tariff.type].toString() : null;
  }
}
