import auth from './middleware/auth';
import BaseLayout from '../workplace/layouts/BaseLayout.vue';

const routes = [
  {
    path: '/',
    component: BaseLayout,
    children: [
      {
        name: 'search',
        path: 'client/search',
        component: () => import('src/workplace/pages/client/search/ClientSearchPage.vue'),
        beforeEnter: auth
        // meta: {
        //   middleware: [auth]
        // }
      },
      {
        path: 'client/search/result',
        name: 'searchResult',
        component: () => import('src/workplace/pages/client/search/ClientSearchResultPage.vue'),
        props: true,
        beforeEnter: auth
      },
      {
        path: '',
        name: 'home',
        component: () => import('src/workplace/pages/IndexPage.vue'),
        beforeEnter: auth
      }
    ]
  },
  {
    path: '/client',
    component: () => import('src/workplace/layouts/ClientDetailLayout.vue'),
    children: [
      {
        name: 'clientDetail',
        path: ':id',
        component: () => import('src/workplace/pages/client/detail/ClientDetailPage.vue'),
        props: true,
        beforeEnter: auth
      },
      {
        path: '',
        redirect: { name: 'home' }
      }
    ]
  },
  {
    name: 'clientTariff',
    path: '/tariff',
    component: () => import('src/workplace/pages/client/tariff/ClientTariffDetailPage.vue'),
    beforeEnter: auth
  },
  {
    name: 'clientStatement',
    path: '/statement',
    component: () => import('src/workplace/pages/client/statement/StatementDetailPage.vue'),
    beforeEnter: auth
  },
  {
    name: 'login',
    path: '/login',
    component: () => import('src/workplace/pages/LoginPage.vue'),
    props: true
  },
  {
    path: '*',
    component: () => import('src/workplace/pages/Error404Page.vue')
  }
];

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('src/workplace/pages/Error404Page.vue')
  });
}

export default routes;