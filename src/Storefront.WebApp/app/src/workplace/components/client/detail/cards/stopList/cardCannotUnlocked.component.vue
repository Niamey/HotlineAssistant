<template>
  <div class="row">

      <q-banner inline-actions rounded class="col-7 banner-wrapper">
        {{ $t('components.client.detail.cards.limits.cardCannotUnlocked.titleUnableUnlock') }}
        <template v-slot:action>
          <q-btn flat :label="$t('components.client.detail.cards.limits.cardCannotUnlocked.showDetails')" no-caps dense class="q-px-md">
            <q-popup-proxy anchor="bottom left" self="top left" :offset="[0, 5]" >
              <q-card>
                <q-card-section>
                  <no-data-component v-if="!statusData" />
                  <div v-else class="q-mt-sm q-gutter-y-sm">
                    <div>
                      <div class="text-subtitle2 text-grey">{{ $t('components.client.detail.cards.limits.cardCannotUnlocked.timeOfBlocking') }}</div>
                      <div class="text-subtitle1">{{ statusData.dateChangeStatus | formatDate(dateTimeFormat) }}</div>
                    </div>
                    <div>
                      <div class="text-subtitle2 text-grey">{{ $t('components.client.detail.cards.limits.cardCannotUnlocked.reasonOfBlocking') }}</div>
                      <div class="text-subtitle1">{{ statusData.reason }}</div>
                    </div>
                  </div>
                </q-card-section>
              </q-card>
            </q-popup-proxy>
          </q-btn>
        </template>
      </q-banner>
    </div>
</template>

<script lang="ts">

import { Component, Prop, Vue } from 'vue-property-decorator';
import NoDataComponent from '../../../../shared/noData.component.vue';

import { CardCurrentStatusModel } from '../../../../../models/card/status';

import { FormatDateFilter } from '../../../../../filters';
import constants from '../../../../../common/constants';

@Component({
  components: { NoDataComponent },
  filters: { FormatDateFilter }
})
export default class CardCannotUnlockedComponent extends Vue {
  @Prop({ required: true }) statusData!: CardCurrentStatusModel;
  private dateTimeFormat: string = constants.formatting.dateTimeVue;
}
</script>

<style scoped>
  li {
    color: var(--q-color-primary);
  }
  li span {
    color: black;
    font-weight: 400;
  }
  .detail-wrapper {
    background-color: #F8F9F9;
  }

  .banner-wrapper {
    position: relative;
    background-color: #FDE7E9;
    color: #D83B01;
    height: 36px !important;
    padding: 0 16px;
    min-height: 0;
  }
</style>
