<template>
    <div class="col-md-6 offset-md-3 login">
      <div class="column items-center" style="display: flex;align-items: center;">
        <index-page-title-component class="col q-mb-xl" style="padding-top: 4em;"/>
        <login-component :message="message"/>
      </div>
    </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';

import IndexPageTitleComponent from '../components/shared/indexPageTitle.component.vue';
import LoginComponent from '../components/user/login.component.vue';

@Component({
  components: { IndexPageTitleComponent, LoginComponent }
})

export default class Login extends Vue {
 @Prop({ default: '' }) message?: string;
}
</script>

<style scoped>
.login {
  background-color: #f8f9f9;
  height: 100vh;
}
.login >>> .txt{
  font-size: 36px;
  line-height: 44px;
}
</style>
