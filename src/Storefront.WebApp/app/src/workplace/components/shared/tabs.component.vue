<template>
  <div class="q-pa-md">
    <q-tab-panels active-color="primary" v-model="selectedTab" v-for="item in tabs" :key="item.id">
      <q-tab-panel :name="item.name" :label="item.label">
        <router-view />
      </q-tab-panel>
    </q-tab-panels>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
interface TabItem {
  id: number;
  name: string;
  label: string;
}
@Component
export default class TabsComponent extends Vue {
  @Prop() tabs?: Array<TabItem>;
  private selectedTab: TabItem;
  constructor () {
    super();
    this.tabs = [
      { id: 0, name: 'details', label: 'Деталі картки' },
      { id: 1, name: 'stop-list', label: 'Стоп-лист' },
      { id: 2, name: 'limits', label: 'Ліміти' },
      { id: 3, name: 'tokens', label: 'Токени' },
      { id: 4, name: 'tarifs', label: 'Тарифи' },
      { id: 5, name: 'smsInfo', label: 'SMS-інфо' },
      { id: 6, name: 'details', label: 'Деталі картки' }
    ];
    this.selectedTab = this.tabs[0];
  }
}
</script>

<style scoped>

</style>
