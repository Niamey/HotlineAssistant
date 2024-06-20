import '../router/componentHooks';
import { boot } from 'quasar/wrappers';
import Store from '../workplace/store';

export default boot(({ app }) => {
  app.store = Store;
});
