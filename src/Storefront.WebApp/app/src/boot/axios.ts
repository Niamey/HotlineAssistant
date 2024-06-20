import { boot } from 'quasar/wrappers';
import { AxiosInstance } from 'axios';
import baseApi from '../workplace/api/base.api';

declare module 'vue/types/vue' {
  interface Vue {
    $axios: AxiosInstance;
    $logger: any;
  }
}

export default boot(({ Vue }) => {
  // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
  Vue.prototype.$axios = baseApi;
  Vue.prototype.$logger = console.log.bind(console);
});

export { baseApi };
