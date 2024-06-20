<template>
  <q-card class="login-form"  @keypress.enter="onLogin" >
    <q-card-section>
        <div class="q-mt-sm text-center text-subtitle1 text-grey-7 text-weight-medium" v-if="message">{{ message }}</div>
        <div class="q-mt-sm q-mb-lg text-center text-subtitle1 text-grey-7 text-weight-medium">
          {{ $t('components.authorization.login.title') }}
        </div>

        <q-input class="q-mx-md"
        ref="userLogin"
        v-model="userLogin"
        dense
        filled
        :placeholder="$t('components.authorization.login.login')"
        :rules="[
          val => !!val || $t('components.shared.validation.requiredField'),
        ]"
      lazy-rules />
        <q-input class="q-mt-md q-mx-md"
        ref="password"
        v-model="password"
        :type="isPwd ? 'password' : 'text'"
        dense
        filled
        :placeholder="$t('components.authorization.login.password')"
        :rules="[
          val => !!val || $t('components.shared.validation.requiredField'),
        ]"
      lazy-rules>
          <template v-slot:append>
            <q-icon
              :name="!isPwd ? 'visibility' : 'visibility_off'"
              class="cursor-pointer text-grey-5"
              @click="isPwd = !isPwd"
            />
          </template>
        </q-input>
        <div class="q-mt-lg q-mx-md q-mb-md">
          <q-btn style="width: 100%;" color="primary" size="15px" no-caps label="Увійти" :loading="isLoading" @click="onLogin"/>
        </div>
        <!-- Bottom message -->
        <div v-if="exceptionMessage" class="text-caption text-center text-negative q-mx-lg q-mt-lg q-mb-md">{{ exceptionMessage }}</div>
    </q-card-section>
  </q-card>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import { UserModule } from '../../store/modules/user.module';

@Component
export default class Login extends Vue {
  @Prop({ default: '' }) message?: string;
  private userLogin = '';
  private password = '';

  private isPwd = true;
  private isLoading = false;
  private exceptionMessage: string|undefined|null = null;
  private hasError=false;
  private returnUrl = '';

  created () {
    this.returnUrl = (this.$route.query.returnUrl?.toString() || '/');
  }

  async onLogin () {
    this.isLoading = true;
    // @ts-ignore
    this.$refs.userLogin.validate();
    // @ts-ignore
    this.$refs.password.validate();
    // @ts-ignore
    if (!this.$refs.userLogin.hasError && !this.$refs.password.hasError) {
      const success = await UserModule.Login({ login: this.userLogin, password: this.password });
      if (!success) {
        this.exceptionMessage = UserModule.getErrorMessage;
        this.hasError = UserModule.hasError;
      } else {
        void this.$router.push(this.returnUrl);
      }
    }

    this.isLoading = UserModule.isLoading;
  }
}
</script>

<style scoped>
.login-form {
  width: 380px;
}
</style>
