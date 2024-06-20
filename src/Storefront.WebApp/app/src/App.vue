<template>
  <div id="q-app">
    <router-view />
    <user-session-expiring-component :isShow="showIdle"  @hide="hideDialog" />
    <config-update-required-component :isShow="showUpdate"  @hide="hideUpdate" />
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from 'vue-property-decorator';
import { UserModule } from './workplace/store/modules/user.module';
import { SessionStorage } from 'quasar';

import UserSessionExpiringComponent from './workplace/components/user/userSessionExpiring.component.vue';
import ConfigUpdateRequiredComponent from './workplace/components/user/configUpdateRequired.component.vue';
import constants from './workplace/common/constants';

@Component({
  components: { UserSessionExpiringComponent, ConfigUpdateRequiredComponent }
})
export default class App extends Vue {
  private showIdle: boolean;
  private showUpdate: boolean;

  constructor () {
    super();
    this.showIdle = false;
    this.showUpdate = false;
  }

  // eslint-disable-next-line @typescript-eslint/require-await
  async created () {
    // this.$logger('It works!'); // Вариант логера через плагин
    // this.logger('It works!'); // Вариант логера через миксин. Тут понятно (удалил функционал)
    // logger.log('It works!'); // Вариант логера через index.template.html. Есть нюанс - постоянно ругаеться линт. Добавление logger в глобальные переменные ничего не дает
    // Вариант отключения консоли в продакшене. Нужно расскоменировать строки в babel.config.js и установить плагин

    // eslint-disable-next-line @typescript-eslint/no-this-alias
    const self = this;
    this.$axios.interceptors.response.use(undefined, async function (err) {
      if (err && err.response && err.response.status === 401 && err.response.config && !err.response.config.__isRetryRequest) {
        await UserModule.SignOut();
        await self.$router.push({
          path: '/login',
          query: { returnUrl: self.$route.fullPath }
        });
      }

      return new Promise(function () {
        throw err;
      });
    });
  }

  get isIdle () {
    return this.$store.state.idleVue.isIdle;
  }

  @Watch('isIdle')
  private isIdleChanged (isIdle: boolean) {
    if (this.$route.name === 'login') return;
    if (isIdle) {
      this.showIdle = isIdle;
    }
  }

  get needUpdate () {
    const needUpdate = SessionStorage.getItem(constants.isNeedUpdate);
    if (needUpdate) {
      this.showUpdate = true;
    }
    return needUpdate;
  }

  @Watch('needUpdate')
  private needUpdateChanged (needUpdate: any) {
    if (needUpdate) {
      this.showUpdate = true;
    }
  }

  hideDialog () {
    this.showIdle = false;
  }

  hideUpdate () {
    this.showUpdate = false;
  }
}
</script>
