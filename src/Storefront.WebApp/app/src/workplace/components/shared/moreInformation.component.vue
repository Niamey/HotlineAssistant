<template>
  <div class="q-pa-md">
    <div class="q-gutter-y-md">
      <div><slot name="header"></slot></div>
      <q-slide-transition :duration="200">
        <div v-if="expanded">
          <slot name="default"></slot>
        </div>
      </q-slide-transition>
      <div v-if="isShow" class="row items-center justify-center cursor-pointer label" @click="expanded = !expanded">
        {{ label }}
        <q-icon :name="iconName" color="#0078D4" class="q-ml-sm"/>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class MoreInformationComponent extends Vue {
  @Prop() labelExpand?: string;
  @Prop() labelCollapse?: string;
  @Prop() isShow!: boolean;
  private readonly expanded: boolean;

  constructor () {
    super();
    this.labelExpand = this.labelExpand ?? 'More';
    this.labelCollapse = this.labelCollapse ?? 'Less';
    this.expanded = false;
  }

  get label () {
    return this.expanded ? this.labelCollapse : this.labelExpand;
  }

  get iconName () {
    return this.expanded ? 'keyboard_arrow_up' : 'keyboard_arrow_down';
  }
}
</script>

<style scoped>
.label {
  color: #0078d4;
}
</style>
