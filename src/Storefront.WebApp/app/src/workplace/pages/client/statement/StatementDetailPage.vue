<template>
 <q-layout view="hHh lpR lFf" style="background-color: gray;">
    <statement-detail-component/>
  </q-layout>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import StatementDetailComponent from '../../../components/client/detail/agreements/statement/statementDetail.component.vue';

@Component({
  components: { StatementDetailComponent }
})
export default class StatementDetailPage extends Vue {
}
</script>

<style scoped>
</style>
