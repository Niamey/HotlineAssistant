<template>
  <div>
    <div class="text-body1 text-weight-medium q-mb-md">Знайдіть токени зі статусом «Неактивний» та видаліть активовані шахраями</div>
    <client-card-token-list-component :cardId="dynamicData.cardDetail.cardId" @tokenDeleted = "tokenDeleted"/>
  </div>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import BaseStepComponent from './baseStep.component.vue';

import BlockSpinnerComponent from '../../../../shared/blockSpinner.component.vue';
import ErrorDataComponent from '../../../../shared/errorData.component.vue';
import { mixins } from 'vue-class-component';
import ClientDetailMixin from '../../../../../mixins/client/clientDetail.mixin';
import { BaseStepInterface } from '../../../../../interfaces/card/limits/baseStep.interface';
import ClientCardTokenListComponent from '../../../../../components/client/detail/cards/tokens/clientCardTokenList.component.vue';
import { CardTokenModel } from '../../../../../models/card/token/cardToken.model';

@Component({
  name: 'CardBlockManageTokenComponent',
  components: { BlockSpinnerComponent, ErrorDataComponent, ClientCardTokenListComponent }
})
export default class CardBlockManageTokenComponent extends mixins(BaseStepComponent, ClientDetailMixin) implements BaseStepInterface {
  private deletedTokenList: Array<CardTokenModel> = [];

  tokenDeleted (item: CardTokenModel) {
    this.deletedTokenList.push(item);
  }

  validateStep (): string|string[]|undefined {
    return undefined;
  }

  collectData (): void {
    if (this.deletedTokenList.length > 0) {
      const localReasonInfo = Object.assign({}, this.reasonInfo);

      localReasonInfo.deletedTokens = this.deletedTokenList;
      this.$emit('update:reason-info', localReasonInfo);
    }
  }
}
</script>

<style scoped>

</style>
