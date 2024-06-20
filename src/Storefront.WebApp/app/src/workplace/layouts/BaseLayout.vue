<template>
  <div>
    <q-layout view="hHh lpR lFf">
      <header-component />
      <left-sidebar-component :list-data="this.SideBarItems" :hasError="this.SideBarItemsHasError" :isLoading="this.SideBarItemsLoading" />
      <q-page-container class="bq-page-container">
        <router-view />
      </q-page-container>
    </q-layout>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import LeftSidebarComponent from './components/leftSidebar.component.vue';
import HeaderComponent from './components/header.component.vue';
import MenuLeftMixin from '../../workplace/mixins/menu/menuLeft.mixin';
import { mixins } from 'vue-class-component';
import { LocalizationEnum } from '../../workplace/enums/localization/localization.enum';

@Component({
  components: { LeftSidebarComponent, HeaderComponent }
})

export default class BaseLayout extends mixins(MenuLeftMixin) {
  mounted () {
    // temporary solution
    void this.loadMenuLeftDetail(LocalizationEnum.Ukraine);
  }
}
</script>
<style lang="css">
.bq-page-container {
  background: #F8F9F9;
}
</style>
