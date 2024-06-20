import { AgreementNavigateQueryHelper } from './navigateQueryHelpers/agreementNavigateQuery.helper';
import { AccountNavigateQueryHelper } from './navigateQueryHelpers/accountNavigateQuery.helper';
import { CardNavigateQueryHelper } from './navigateQueryHelpers/cardNavigateQuery.helper';
import { TariffNavigateQueryHelper } from './navigateQueryHelpers/tariffNavigateQuery.helper';
import { StatementNavigateQueryHelper } from './navigateQueryHelpers/statementNavigateQuery.helper';
import { TravelNavigateQueryHelper } from './navigateQueryHelpers/travelNavigateQuery.helper';

export class NavigateQueryHelper {
  private static agreement : AgreementNavigateQueryHelper;
  private static account : AccountNavigateQueryHelper;
  private static card : CardNavigateQueryHelper;
  private static tariff: TariffNavigateQueryHelper;
  private static statement :StatementNavigateQueryHelper;
  private static travel : TravelNavigateQueryHelper;

  public static get agreements () : AgreementNavigateQueryHelper {
    if (!this.agreement) {
      this.agreement = new AgreementNavigateQueryHelper();
    }
    return this.agreement;
  }

  public static get accounts () : AccountNavigateQueryHelper {
    if (!this.account) {
      this.account = new AccountNavigateQueryHelper();
    }
    return this.account;
  }

  public static get cards () : CardNavigateQueryHelper {
    if (!this.card) {
      this.card = new CardNavigateQueryHelper();
    }
    return this.card;
  }

  public static get tariffs () : TariffNavigateQueryHelper {
    if (!this.tariff) {
      this.tariff = new TariffNavigateQueryHelper();
    }
    return this.tariff;
  }

  public static get statements () : StatementNavigateQueryHelper {
    if (!this.statement) {
      this.statement = new StatementNavigateQueryHelper();
    }
    return this.statement;
  }

  public static get travels () : TravelNavigateQueryHelper {
    if (!this.travel) {
      this.travel = new TravelNavigateQueryHelper();
    }
    return this.travel;
  }

  public static navigate (that: Vue, key:string, value:any):void {
    if (that.$route.query[key] !== value?.toString()) {
      const navigateQuery:any = Object.assign({}, that.$route.query);
      navigateQuery[key] = value;

      const navigateParams = Object.assign({}, that.$route.params);

      that.$router.push({ name: that.$route.name?.toString(), params: navigateParams, query: navigateQuery }).catch(err => {
      // Ignore the vuex err regarding  navigating to the page they are already on.
        if (err.name !== 'NavigationDuplicated') {
        // But print any other errors to the console
          console.error(err);
        }
      });
    }
  }
}
