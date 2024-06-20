import { Component, Vue } from 'vue-property-decorator';
import { Route } from 'vue-router';
import { KeyValueInterface } from '../../interfaces/common';
import constants from '../../common/constants';

type Dictionary<T> = { [key: string]: T }

@Component
export default class RouteHelper extends Vue {
  /*
  beforeRouteEnter (to:Route, from:Route, next:any) {
    next((vm : any) => {
      vm.from = from;
    });
  } */

  // @ts-ignore
  // eslint-disable-next-line
  getParamValuesFromRoute (route: Route, keyValue: string): string {
    const parsedParams = {};
    route.hash.substr(1)
      .split('&')
      .map(part => part.replace(/#/, ''))
      .forEach(param => {
        const parts = param.split('=');
        // @ts-ignore
        parsedParams[parts[0]] = parts[1];
      });
    return <string> parsedParams;
  }

  setRouteParameter (routeKey: string, value: string) : string {
    return `${routeKey}=${value}`;
  }

  setParameterFromObj (param: KeyValueInterface) : string {
    return `${param.key}=${param.value}`;
  }

  setRouteParameters (param: Array<KeyValueInterface>) : string {
    let params = '';
    for (let i = 0; i <= param.length - 1; i++) {
      if (i === 0) {
        params = `${param[i].key}=${param[i].value}`;
      } else {
        params += `&${param[i].key}=${param[i].value}`;
      }
    }
    return params;
  }

  async navigateToPath (routePath: string, params: string) {
    await this.$router.push({ path: routePath, hash: `#${params}` });
  }

  async navigateBySolarId (name: string, solarId: number, query?: Dictionary<string | (string | null)[] | null | undefined>) {
    await this.$router.push({ name: name, params: { id: solarId.toString() }, query: query }).catch(err => {
      // Ignore the vuex err regarding  navigating to the page they are already on.
      if (err.name !== 'NavigationDuplicated') {
        // But print any other errors to the console
        console.error(err);
      }
    });
  }

  newTabBySolarId (name: string, solarId: number, query?: Dictionary<string | (string | null)[] | null | undefined>) {
    const routeData = this.$router.resolve({ name: name, params: { id: solarId.toString() }, query: query });
    window.open(routeData.href, '_blank');
  }

  getPageIndex (key: string) : number {
    if (!key) {
      return constants.paging.pageIndex;
    }

    const pageIndex = this.$route.query[key] ? parseInt(this.$route.query[key].toString()) : constants.paging.pageIndex;
    return pageIndex;
  }

  async navigateToRouteName (routeName: string, params: string) {
    await this.$router.push({ name: routeName, hash: `#${params}` });
  }

  protected navigateByQueryParam (key:string, value:any, pageIndexKey: string, defaultValue: any): void {
    if (this.$route.query[key] !== value?.toString()) {
      const navigateQuery:any = Object.assign({}, this.$route.query);
      if (value === defaultValue) {
        navigateQuery[key] = undefined;
      } else {
        navigateQuery[key] = value;
      }
      navigateQuery[pageIndexKey] = undefined;

      const navigateParams = Object.assign({}, this.$route.params);

      this.$router.push({ name: this.$route.name?.toString(), params: navigateParams, query: navigateQuery }).catch(err => {
      // Ignore the vuex err regarding  navigating to the page they are already on.
        if (err.name !== 'NavigationDuplicated') {
        // But print any other errors to the console
          console.error(err);
        }
      });
    }
  }
}
