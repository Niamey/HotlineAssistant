import Vue from 'vue';
import VueRouter from 'vue-router';
import routes from './routes';
Vue.use(VueRouter);

import VueBus from 'vue-bus';
// import constants from '../workplace/common/constants';
import { UserModule } from '../workplace/store/modules/user.module';
// import { UserModule } from '../../store/modules/user.module';

Vue.use(VueBus);

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Router instance.
 */

export default function (/* { store, ssrContext } */) {
  const Router = new VueRouter({
    scrollBehavior: (to, from /* ,savedPosition */) => {
      return (from.path !== to.path) ? { x: 0, y: 0 } : undefined;
    },
    routes,
    // Leave these as they are and change in quasar.conf.js instead!
    // quasar.conf.js -> build -> vueRouterMode
    // quasar.conf.js -> build -> publicPath
    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
    // @ts-ignore
    mode: process.env.VUE_ROUTER_MODE || 'history',
    base: process.env.VUE_ROUTER_BASE
  });

  // Router.beforeEach((to, from, next) => {
  //   if (!to.meta.middleware) {
  //     return next();
  //   }
  //   const middleware = to.meta.middleware;

  //   const context = {
  //     to,
  //     from,
  //     next,
  //     store: UserModule
  //   };
  //   return middleware[0]({
  //     ...context
  //   });
  //   /* if (to.matched.some(record => record.meta.requiresAuth)) {
  //     if (sessionStorage.getItem(constants.auth.tokenKey) == null) {
  //       next({
  //         path: '/login',
  //         params: { nextUrl: to.fullPath }
  //       });
  //     } else {
  //       // @ts-ignore
  //       // const user = JSON.parse(sessionStorage.getItem('user'));
  //       // Verify in roles
  //       if (to.matched.some(record => record.meta.is_admin)) {
  //         if (user.is_admin === 1) {
  //           next();
  //         } else {
  //           next({ name: '/adminboard' });
  //         }
  //       } else {
  //         next();
  //       } /
  //       next();
  //     }
  //   } else {
  //     next();
  //   } */
  // });

  return Router;
}
