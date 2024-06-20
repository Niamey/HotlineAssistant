<template>
  <q-list padding :class="this.parentClasses">
      <q-item class="text-grey-4"
                clickable
                v-ripple
                v-for="item in this.itemsCount" :key="item.Id">
          <q-item-section class="q-pr-none" avatar>
            <q-skeleton size="24px" class="bg-grey-4" animation="fade" />
          </q-item-section>
          <q-item-section>
            <q-skeleton type="text" class="bg-grey-4" animation="fade" />
          </q-item-section>
      </q-item>
    </q-list>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
@Component
export default class LeftSidebarSkeletonLoaderComponent extends Vue {
  @Prop() itemsCount?: number;
  @Prop() parentClasses?: Array<string>;

  constructor () {
    super();
    this.itemsCount = this.itemsCount || 4;
  }
}
</script>

<style scoped>

</style>
