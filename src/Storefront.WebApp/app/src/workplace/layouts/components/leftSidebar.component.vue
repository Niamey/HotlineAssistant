<template>
  <q-drawer
    show-if-above

    :mini="miniState"
    @mouseover="onMouseOver"
    @mouseleave="onMouseLeave"
    mini-to-overlay

    :width="290"
    :breakpoint="500"
    content-class="bg-white shadow-5">
    <q-scroll-area  :class="[this.showLoading ? 'fit overflow-hidden' : 'fit']">
      <left-sidebar-skeleton-loader-component v-if="this.showLoading" :itemsCount="4" :parentClasses="['fixed-top']"/>
      <div v-else-if="showError">
            <error-data-component  />
      </div>
      <q-list v-else padding class="fixed-top">
        <q-item clickable
                v-ripple
                v-for ="module in this.topItems" :key="module.id">
                <q-item-section class="q-pr-none" avatar>
                    <q-icon class="icon-color" :name="module.iconUrl"/>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label class="text-color">{{module.moduleName}}</q-item-label>
                  </q-item-section>
        </q-item>
      </q-list>

      <left-sidebar-skeleton-loader-component  v-if="this.showLoading" :itemsCount="3" :parentClasses="['fixed-bottom']"/>
      <div v-else-if="showError">
            <error-data-component  />
      </div>
      <q-list padding class="fixed-bottom">
        <q-item clickable
                v-ripple
                v-for="module in this.bottomItems" :key="module.id">
                <q-item-section class="q-pr-none" avatar >
                    <q-icon class="icon-color" :name="module.iconUrl"/>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label class="text-color">{{module.moduleName}}</q-item-label>
                  </q-item-section>
        </q-item>
      </q-list>
    </q-scroll-area>
  </q-drawer>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import MenuLeftModel from '../../models/menu/menuLeft.model';
import { MenuItemPositionEnum } from '../../enums/sidebarMenuItemPosition.enum';
import BaseListComponent from '../../components/base/baseList.component.vue';
import LeftSidebarSkeletonLoaderComponent from './leftSidebarSkeletonLoader.component.vue';

@Component({
  components: { LeftSidebarSkeletonLoaderComponent }
})

export default class LeftSidebar extends BaseListComponent {
  @Prop({ required: true }) declare listData: MenuLeftModel[];
  private miniState: boolean;
  private menuHovered: boolean;
  private timeoutId: any;

  constructor () {
    super();
    this.miniState = true;
    this.menuHovered = false;
    this.timeoutId = 0;
  }

  get topItems () {
    return this.listData.filter(x => x.align === MenuItemPositionEnum.LeftTop);
  }

  get bottomItems () {
    return this.listData.filter(x => x.align === MenuItemPositionEnum.LeftBottom);
  }

  onMouseOver () {
    this.menuHovered = true;
    clearTimeout(this.timeoutId);
    this.timeoutId = setTimeout(() => {
      if (this.menuHovered) {
        this.miniState = false;
      }
    }, 300);
  }

  onMouseLeave () {
    clearTimeout(this.timeoutId);
    this.menuHovered = false;
    this.miniState = true;
  }
}
</script>

<style scoped>
.icon-color {
  color: #a8a9aa;
}
.text-color {
  color: #7D7D80;
}
</style>
