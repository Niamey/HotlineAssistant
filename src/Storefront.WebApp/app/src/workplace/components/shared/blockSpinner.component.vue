<template>
  <div class="q-pa-md">
    <div class="box">
      <q-spinner
        :color="'primary'"
        :size="size"
        :thickness="2" />
      <q-item-label class="text-center">{{ label }}</q-item-label>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { i18n } from '../../../boot/i18n';

@Component
export default class BlockSpinnerComponent extends Vue {
  @Prop({ default: '5em' }) size?: string;
  @Prop({ default: i18n.t('components.shared.blockSpinner.label') }) label!: string;
  @Prop(Boolean) loaded!: boolean;
}
</script>

<style scoped>
.box {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
