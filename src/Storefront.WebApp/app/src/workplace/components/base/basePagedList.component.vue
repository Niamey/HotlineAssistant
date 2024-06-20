<template>
<div></div>
</template>
<script lang="ts">
import BaseListComponent from './baseList.component.vue';
import { Component } from 'vue-property-decorator';

@Component
export default class BasePagedListComponent<TLocalData> extends BaseListComponent {
  protected currentPageIndexKey !: string;
  protected localData : Array<TLocalData>;

  constructor () {
    super();
    this.localData = [];
  }

  protected get currentPageIndex (): number {
    return this.getPageIndex(this.currentPageIndexKey);
  }

  protected onChangePage (items: any, page: number) {
    this.localData = items;
    this.onChangePageIndex(page);
  }

  protected onChangePageIndex (page: number):void {
    if (this.$route.query[this.currentPageIndexKey] !== page.toString()) {
      const navigateQuery:any = Object.assign({}, this.$route.query);

      if (page <= 1) {
        navigateQuery[this.currentPageIndexKey] = undefined;
      } else {
        navigateQuery[this.currentPageIndexKey] = page.toString();
      }

      const navigateParams = Object.assign({}, this.$route.params);

      this.$router.push({ name: this.$route.name?.toString(), params: navigateParams, query: navigateQuery }).catch(err => {
      // Ignore the vuex err regarding  navigating to the page they are already on.
        if (err.name !== 'NavigationDuplicated') {
        // But print any other errors to the console
          console.error(err);
        }
      });
    }
  }

  protected navigateByQueryParam (key:string, value:any, defaultValue: any):void {
    if (this.$route.query[key] !== value?.toString()) {
      const navigateQuery:any = Object.assign({}, this.$route.query);
      if (value === defaultValue) {
        navigateQuery[key] = undefined;
      } else {
        navigateQuery[key] = value;
      }
      navigateQuery[this.currentPageIndexKey] = undefined;

      const navigateParams = Object.assign({}, this.$route.params);

      this.$router.push({ name: this.$route.name?.toString(), params: navigateParams, query: navigateQuery }).catch(err => {
      // Ignore the vuex err regarding  navigating to the page they are already on.
        if (err.name !== 'NavigationDuplicated') {
        // But print any other errors to the console
          console.error(err);
        }
      });
    }
  }
}
</script>
