<template>
  <div class="q-pa-md">
    <q-table
      :data="data"
      :columns="columns"
      row-key="name"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class OperationTapesComponent extends Vue {
  private columns: any[] = [];
  private data: any[] = [];

  constructor () {
    super();
    this.columns = [
      {}
    ];

    this.data = [
      {},
      {},
      {},
      {}
    ];
  }
}
</script>

<style scoped>

</style>
