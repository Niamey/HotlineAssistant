<template>
  <div>
    <q-layout view="hHh lpR lFf">
      <header-component>
        <div class="col-9 cursor-pointer overflow-hidden" @click="scrollToTop()">
          <client-contact-data-component :detail-data="this.ClientDetail" :has-error="this.HasError" :is-loading="this.Loading" :is-show="this.ShowClientHeaderData" />
        </div>
      </header-component>
      <left-sidebar-component :list-data="this.SideBarItems" :has-error="this.SideBarItemsHasError" :is-loading="this.SideBarItemsLoading" />
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
import MenuLeftMixin from '../mixins/menu/menuLeft.mixin';
import ClientDetailMixin from '../mixins/client/clientDetail.mixin';
import { mixins } from 'vue-class-component';
import { LocalizationEnum } from '../../workplace/enums/localization/localization.enum';
import ClientContactDataComponent from '../components/client/detail/clientContactData.component.vue';

@Component({
  components: { LeftSidebarComponent, HeaderComponent, ClientContactDataComponent }
})

export default class ClientDetailLayout extends mixins(MenuLeftMixin, ClientDetailMixin) {
  mounted () {
    // temporary solution
    void this.loadMenuLeftDetail(LocalizationEnum.Ukraine);
  }

  scrollToTop () {
    this.$bus.emit('client-contact-data-scroll-to-top');
  }
}
</script>

<style lang="css">
.bq-page-container {
  background: #F8F9F9;
}
</style>
