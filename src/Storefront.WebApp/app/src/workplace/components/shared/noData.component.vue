<template>
  <div class="full-width row flex-center q-gutter-sm text-h6">
    <q-icon size="2em" name="sentiment_dissatisfied" />
    <span> {{ $t('components.shared.noData.label')}}</span>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class NoDataComponent extends Vue {
}
</script>

<style scoped>

</style>
