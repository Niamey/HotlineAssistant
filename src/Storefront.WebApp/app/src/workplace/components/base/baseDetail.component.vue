<template>
  <div></div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import RouteHelper from '../../mixins/routing/router.mixin';

@Component
export default class BaseDetailComponent extends RouteHelper {
  @Prop({ required: true }) detailData!: any;
  @Prop({ required: true }) isLoading!: boolean;
  @Prop({ required: true }) hasError!: boolean;

  protected get showLoading (): boolean {
    return this.isLoading;
  }

  protected get showError (): boolean {
    return !this.showLoading && this.hasError;
  }

  protected get showNoData (): boolean {
    return !this.showLoading && !this.showData && !this.showError;
  }

  protected get showData (): boolean {
    return !this.showLoading && !this.showError && this.detailData !== undefined;
  }
}
</script>
