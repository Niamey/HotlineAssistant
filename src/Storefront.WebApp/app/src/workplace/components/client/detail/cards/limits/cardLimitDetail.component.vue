<template>
  <div v-if="isDetailLimit()">
    <div v-for="(limit, index) in detailLimit.limits" :key="index" :class="computedClasses()">
      <div class="text-grey-7 q-mt-sm">{{ getLabelTitle(limit) }}</div>
      <div v-if="isOperation(limit)" class="row">
        <div class="col-auto q-mr-sm">{{ limit.limit | FormatStringUnitsFilter('pieces') }}</div>
        <q-chip class="chip" square :label="limitUnits(limit.used, 'pieces')" />
      </div>
      <div v-else class="row">
        <div class="col-auto q-mr-sm">{{ limit.limit | FormatMoneyFilter }}<small class="text-grey-7">{{ getCurrency(limit) }}</small></div>
        <q-chip class="chip" square :label="limitMoneyFiltered(limit.used)"><small class="text-grey-7">{{ getCurrency(limit) }}</small></q-chip>
      </div>
    </div>
  </div>
  <no-data-component v-else class="row flex-center q-my-md"/>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardLimitInterface } from '../../../../../interfaces/card/limits/cardLimit.interface';
import { AmountCardLimitInterface } from '../../../../../interfaces/card/limits/amountCardLimit.interface';
import { OperationLimitPeriodEnumHelper } from '../../../../../helpers/enumHelpers/operationLimitPeriodEnum.helper';
import { AmountLimitPeriodEnumHelper } from '../../../../../helpers/enumHelpers/amountLimitPeriodEnum.helper';
import { LimitTypeEnum } from '../../../../../enums/card/limits/limitType.enum';
import { FormatMoneyFilter, FormatStringUnitsFilter } from '../../../../../filters';
import NoDataComponent from '../../../../shared/noData.component.vue';

@Component({
  components: { NoDataComponent },
  filters: { FormatMoneyFilter, FormatStringUnitsFilter }
})
export default class CardLimitDetailComponent extends Vue {
  @Prop({ required: true }) detailLimit!: CardLimitInterface;
  @Prop() parentClasses!: any;

  isDetailLimit () {
    return this.detailLimit?.limits;
  }

  isOperation (limit: AmountCardLimitInterface) {
    return limit.typeLimit === LimitTypeEnum.Operation;
  }

  getCurrency (limit: AmountCardLimitInterface) {
    return limit.currencyName ?? '#';
  }

  getLabelTitle (limit: AmountCardLimitInterface) {
    if (limit.typeLimit === LimitTypeEnum.Operation) {
      return OperationLimitPeriodEnumHelper.getResult(limit.periodTypeLimit);
    } else {
      return AmountLimitPeriodEnumHelper.getResult(limit.periodTypeLimit);
    }
  }

  limitUnits (val: any, unit: string) {
    return FormatStringUnitsFilter(val, unit);
  }

  limitMoneyFiltered (val: number) {
    return FormatMoneyFilter(val);
  }

  computedClasses () {
    if (this.parentClasses) {
      return [...this.parentClasses];
    } else {
      return [];
    }
  }
}
</script>

<style scoped>
.chip {
  background-color: #DFF6DD;
  color: #107C10;
  margin: 0;
  padding: 2px 8px;
  height: 1.5em;
}
small {
  padding-left: 4px;
}
.blured {
  filter: blur(3px);
}
</style>
