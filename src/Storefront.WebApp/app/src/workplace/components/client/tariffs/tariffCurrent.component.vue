<template>
  <block-spinner-component v-if="showLoading"
                             v-bind:loaded="showLoading"
                             v-bind:size="'5em'"
                             v-bind:label="$t('components.shared.blockSpinner.label')"/>
  <no-data-component v-else-if="showNoData" />
  <error-data-component v-else-if="showError" />
  <div class="row q-mt-lg cursor-pointer" v-else-if="showData" @click="viewTariff(tariffType)">
    <div class="col-auto q-pr-md">
      <q-img
        src="../../../../assets/images/icons/pdf_file_icon.svg"
        style="height: 40px; width: 33px"
        img-class="pdf-icon"
      />
    </div>
    <div class="col column">
      <div class="col text-subtitle1 text-blue-7">
        {{ detailData.tariffName }}
      </div>
      <div class="col text-body2 text-grey-6" v-if="detailData.tariffStart">{{ $t('components.client.detail.cards.clientCardDetail.from')}} {{ detailData.tariffStart }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import BlockSpinnerComponent from '../../../components/shared/blockSpinner.component.vue';
import NoDataComponent from '../../shared/noData.component.vue';
import ErrorDataComponent from '../../shared/errorData.component.vue';
import { TariffInterface } from '../../../interfaces/tariff';
import { Prop, Component } from 'vue-property-decorator';
import BaseDetailComponent from '../../base/baseDetail.component.vue';
import constants from '../../../common/constants';
import { mixins } from 'vue-class-component';

@Component({
  components: { BlockSpinnerComponent, NoDataComponent, ErrorDataComponent }
})
export default class TariffCurrentComponent extends mixins(BaseDetailComponent) {
  @Prop({ required: true }) declare detailData: TariffInterface;
  @Prop({ required: true }) tariffType!: string;
  @Prop() agreementId!: number;
  @Prop() cardId!: number;

  async viewTariff (tariffType : string) {
    let params;
    if (tariffType === 'card') {
      params = {
        [constants.navigateQuery.tariff.type]: tariffType,
        [constants.navigateQuery.tariff.clientId]: this.$route.params.id,
        [constants.navigateQuery.tariff.cardId]: this.cardId?.toString()
      };
    } else {
      params = {
        [constants.navigateQuery.tariff.type]: tariffType,
        [constants.navigateQuery.tariff.clientId]: this.$route.params.id,
        [constants.navigateQuery.tariff.agreementId]: this.agreementId?.toString()
      };
    }

    await this.newTabBySolarId('clientTariff', parseInt(this.$route.params.id), params);
  }
}
</script>

<style scoped>

</style>
