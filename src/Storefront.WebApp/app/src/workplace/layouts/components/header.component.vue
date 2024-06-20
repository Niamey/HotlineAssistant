<template>
      <q-header elevated class="header-style">
        <q-toolbar class="toolbar row no-wrap">
          <div class="col-auto q-pr-sm">
            <img @click="goHomePage()" :class="{'cursor-pointer': this.$route.path !== '/'}" svg-inline :src="menuLogo" style="max-width: 81.8px" alt="" />
          </div>
          <slot name="default"></slot>
          <div class="col user-profile">
            <user-profile-component class="float-right bg-white" />
          </div>
        </q-toolbar>
      </q-header>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import UserProfileComponent from './userProfile.component.vue';

@Component({
  components: { UserProfileComponent }
})
export default class Header extends Vue {
  private menuLogo?: string;

  constructor () {
    super();
    this.menuLogo = require('../../../assets/images/platform_logo_orange.svg') || '';
  }

  goHomePage () {
    if (this.$route.path !== '/') {
      void this.$router.push('/');
    }
  }
}
</script>

<style scoped>
 .header-style {
   color: #26272B;
   background-color: #ffffff;
   z-index: 3001;
 }
 .user-profile {
   z-index: 1;
 }
</style>
