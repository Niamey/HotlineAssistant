<template>
  <div>
    <q-btn class="full-width block-button round-borders"
      align="left"
      unelevated
      :label="$t('components.client.detail.cards.limits.cardLimits.blockCard')"
      no-caps
      @click="showCardBlocking"
      icon="block"
    />
    <card-blocking-process-component :isShow="isShowBlockDialog" @hide="hideBlockDialog"/>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import CardBlockingProcessComponent from '../../limits/cardBlockingProcess.component.vue';

import BlockSpinnerComponent from '../../../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../../../shared/noData.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';

@Component({
  components: { CardBlockingProcessComponent, BlockSpinnerComponent, NoDataComponent, ErrorDataComponent }
})
export default class CardStopListComponent extends Vue {
  private isShowBlockDialog: boolean;

  constructor () {
    super();
    this.isShowBlockDialog = false;
  }

  showCardBlocking () {
    this.isShowBlockDialog = true;
  }

  hideBlockDialog () {
    this.isShowBlockDialog = false;
  }
}
</script>

<style scoped>
.block-button {
  background: #F8F9F9;
  color: #D83B01;
  height: 44px;
}
</style>
