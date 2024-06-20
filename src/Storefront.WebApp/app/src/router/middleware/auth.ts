import { UserModule } from '../../workplace/store/modules/user.module';
import { Route, NavigationGuardNext } from 'vue-router'

export default async function auth(to: Route, from: Route, next: NavigationGuardNext) {
  console.log('UserModule.isAuthenticated', UserModule.isAuthenticated)
  if (!UserModule.isAuthenticated) {
    next({
      path: '/login',
      query: { returnUrl: to.fullPath }
    });
  }

  if (!UserModule.profileLoaded) {
    if (!await UserModule.RefreshToken()) {
      next({
        path: '/login',
        query: { returnUrl: to.fullPath }
      });
    }
  }

  next();
}
