<template>
  <q-item-label v-if="selected">{{$t('components.client.clientSearchCategory.label')}}: {{category}}</q-item-label>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class ClientSearchCategoryComponent extends Vue {
  @Prop() category?: string;
  @Prop() selected?: boolean;
}
</script>
