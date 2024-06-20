<template>
  <div>
    <q-table
        class="visual-table"
        :data="this.Cards"
        :columns="columns"
        row-key="cardId"
        selection="multiple"
        :selected.sync="dynamicData.travel.cards"
        hide-selected-banner
        hide-pagination
    >
      <template v-slot:top>
        <div class="row">
          <div>{{ $t('components.client.detail.travels.clientTravelChooseCards.instruction') }}</div>
        </div>
      </template>

      <template v-slot:header="props">
        <q-tr :props="props">
          <q-th colspan="2" class="text-left" width="200px">{{props.cols[0].label}}</q-th>
          <q-th
              v-for="col in props.cols.filter((element, index) => { return index > 0;})"
              :key="col.name"
              :props="props"
          >
            {{ col.label }}
          </q-th>
        </q-tr>
      </template>
    </q-table>
  </div>
</template>

<script lang="ts">
import { Component, Prop } from 'vue-property-decorator';
import { GridColumnBaseInterface } from '../../../../../interfaces/base';
import BaseStepComponent from './baseStep.component.vue';
import { BaseStepInterface } from '../../../../../interfaces/travel/baseStep.interface';
import CardListMixin from '../../../../../mixins/card/list/cardList.mixin';
import { FormatCardNumberFilter } from '../../../../../filters';
import { mixins } from 'vue-class-component';
import { travelApi } from '../../../../../api/travel.api';

interface IClientCardListComponentData {
  number?: string,
  embossedName?: string,
  agreementId?: number,
}

@Component({
  filters: { FormatCardNumberFilter }
})
export default class ClientTravelChooseCardsComponent extends mixins(BaseStepComponent, CardListMixin) implements BaseStepInterface {
  @Prop() pageSize?: number;
  private columns: Array<GridColumnBaseInterface> = [];
  private data?: Array<IClientCardListComponentData> = [];
  private rowsInPage: number;

  constructor () {
    super();
    this.rowsInPage = this.pageSize || 10;
    this.columns = [
      {
        name: 'cardNumber',
        label: this.$t('components.client.detail.travels.clientTravelChooseCards.columns.cardNumber').toString(),
        field: 'number',
        align: 'left',
        format: (number: string) => FormatCardNumberFilter(number)
      },
      {
        name: 'agreementId',
        label: this.$t('components.client.detail.travels.clientTravelChooseCards.columns.agreementId').toString(),
        align: 'left',
        field: 'agreementId',
        format: (agreementId: number) => this.getAgreementNumberById(agreementId)
      },
      { name: 'column', field: '' }
    ];
  }

  async created () {
    if (this.Cards.length === 0) {
      await this.getClientCardList(parseInt(this.$route.params.id));
    }
  }

  validateStep (): string|string[]|undefined {
    return this.dynamicData.travel.cards.length === 0 ? this.$t('components.client.detail.travels.clientTravelChooseCards.errors.requiredСard').toString() : undefined;
  }

  public async collectData () {
    if (!this.dynamicData.isExistRiskyCountries) {
      const result = await travelApi.createAsync({
        solarId: parseInt(this.$route.params.id),
        travelId: this.dynamicData.travel.travelId,
        startTravel: this.dynamicData.travel.startTravel,
        endTravel: this.dynamicData.travel.endTravel,
        contactPhone: this.dynamicData.travel.contactPhone.match(/\d+/g).join(''),
        countries: this.dynamicData.travel.countries,
        cards: this.dynamicData.travel.cards,
        cashWithdrawalLimit: this.dynamicData.travel.cashWithdrawalLimit,
        limitCardTransfers: this.dynamicData.travel.limitCardTransfers,
        isClientAbroad: this.dynamicData.travel.isClientAbroad
      });

      return result;
    }
  }
}
</script>

<style scoped>
  .visual-table >>> .q-tr:first-of-type:after {
    display: none;
  }
  .visual-table >>> th:first-of-type{
    padding: 7px 10px 7px 0;
  }
  .visual-table >>> .q-table__top {
    padding-left: 0;
    padding-right: 0;
  }
  .visual-table >>> .q-checkbox__inner--truthy .q-checkbox__bg,
  .visual-table >>> .q-checkbox__inner--indet .q-checkbox__bg {
    background: var(--q-color-primary);
    border-color: var(--q-color-primary);
  }
  .visual-table.q-table__card {
    box-shadow: none;
  }
  .visual-table >>> .q-table--col-auto-width {
    padding-left: 0;
    padding-right: 0;
  }
  .visual-table >>> td:nth-child(2){
    padding-left: 0;
  }
  .visual-table >>> tr > *:nth-child(1),
  .visual-table >>> tr > *:nth-child(2),
  .visual-table >>> tr > *:nth-child(3) {
    width: 1rem;
  }
</style>
