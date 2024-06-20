<template>
  <div class="q-pa-md q-gutter-xs">
    <div class="box">
      <q-spinner-cube
        :color="color"
        :size="size"
        :thickness="thickness"/>
      <label class="text-center">{{ label }}</label>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
@Component
export default class PageSpinnerComponent extends Vue {
  @Prop() color?: string;
  @Prop() size?: string;
  @Prop({ default: 5 }) thickness?: number;
  @Prop() label?: string;
}
</script>

<style scoped>
.box {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
