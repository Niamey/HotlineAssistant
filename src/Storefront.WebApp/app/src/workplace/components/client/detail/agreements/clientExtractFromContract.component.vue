<template>
  <div class="column">
    <div class="text-h6">{{ $t('components.client.detail.agreements.ClientExtractFromContract.label') }}</div>
    <div class="text-body2 text-grey p-t-12">E-mail:</div>
    <div class="text-body1" style="padding-top: 2px;">kolesnik.v@gmail.com</div>
    <q-toggle
        v-model="isActive"
        dense
        :label="$t('components.client.detail.agreements.ClientExtractFromContract.isActive')"
        class="p-t-12 text-body1"
      />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';

@Component
export default class ClientExtractFromContractComponent extends Vue {
  private isActive:boolean;

  constructor () {
    super();
    this.isActive = true;
  }
}

</script>

<style scoped>
  .text-grey { color: #7D7D80 }
  .p-t-12 { padding-top: 12px; }
</style>
