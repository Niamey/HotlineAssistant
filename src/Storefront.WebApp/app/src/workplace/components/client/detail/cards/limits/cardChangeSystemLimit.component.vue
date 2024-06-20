<template>
  <div>
    <q-dialog ref="dialog" v-model="isShowDialog" @hide="hideDialog()">
      <q-card style="width: 352px" class="q-pa-sm">

         <q-card-section class="row items-center q-pb-none">
          <div class="text-h6 col">{{ $t('components.client.detail.cards.limits.cardChangeSystemLimit.changeSystemLimit') }}</div>
          <q-space />
          <q-btn icon="close" flat round dense v-close-popup text-color="grey-7"/>
        </q-card-section>
        <q-card-section class="row items-center q-pb-none">
          <div class="col-12 text-grey-7 q-mb-sm">{{ $t('components.client.detail.cards.limits.cardChangeSystemLimit.changeSystemLimitInfo') }}</div>
          <div class="col-5 q-pr-sm">
            <q-input @input="updateAmount" v-model.number="systemLimit.limits[0].limit" :label="$t('components.client.detail.cards.limits.cardChangeSystemLimit.amount')" dense filled />
          </div>
          <div class="col-7 q-pl-sm">
            <q-input v-model="data.phone" :label="$t('components.client.detail.cards.limits.cardChangeSystemLimit.phone')" dense filled />
          </div>
        </q-card-section>
        <q-card-section class="q-pb-none">
            <q-checkbox v-model="data.isP2PLimited" :label="$t('components.client.detail.cards.limits.cardChangeSystemLimit.changeP2PLimit')" />
        </q-card-section>
        <q-card-section class="row items-center q-pb-none">
            <div class="text-grey-7">{{ $t('components.client.detail.cards.limits.cardChangeSystemLimit.infoWillBePass') }}</div>
        </q-card-section>
        <q-card-section class="row items-center">
          <q-btn @click="changeLimit" :loading="isSending" class="q-mr-md" color="primary" :label="$t('components.client.detail.cards.limits.cardChangeSystemLimit.change')" no-caps/>
          <q-btn @click="cancel" outline color="primary" :label="$t('components.client.detail.cards.limits.cardChangeSystemLimit.cancel')" no-caps/>
        </q-card-section>
      </q-card>
    </q-dialog>
  </div>
</template>

<script lang="ts">
import { cardApi } from '../../../../../api/card.api';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { FormatMoneyFilter } from '../../../../../filters';
import ClientDetailMixin from '../../../../../mixins/client/clientDetail.mixin';
import { CardInterface } from '../../../../../interfaces/card';
import { CardLimitRiskChangeResponseModel } from '../../../../../models/responses/card/limits';
import { CardLimitInterface } from '../../../../../interfaces/card/limits/cardLimit.interface';
import { mixins } from 'vue-class-component';

@Component({
  filters: { FormatMoneyFilter }
})
export default class CardChangeSystemLimitComponent extends mixins(ClientDetailMixin) {
  @Prop({ default: false }) isShow?: boolean;
  @Prop({ required: true }) detailData!: CardInterface;
  @Prop({ required: true }) systemLimit!: CardLimitInterface;
  private data?: any;
  private isShowDialog: boolean;
  private isSending: boolean;

  constructor () {
    super();
    this.isShowDialog = false;
    this.isSending = false;
    this.data = {};
    this.data.phone = '';
    this.data.isP2PLimited = false;
  }

  @Watch('isShow')
  isShowChanged (newVal: boolean) {
    this.isShowDialog = newVal;
  }

  async created () {
    if (!this.ClientDetail) {
      await this.loadDetail(parseInt(this.$route.params.id));
    }
    this.data.phone = this.getFinancialPhone;
  }

  get getFinancialPhone () {
    return this.ClientDetail?.financialPhone;
  }

  get getSystemLimit () {
    if (this.systemLimit.limits) {
      return this.systemLimit.limits[0].limit;
    }
    return 0;
  }

  updateAmount (val:string) {
    if (this.systemLimit.limits) {
      if (/^[0-9]*$/ig.test(val)) {
        this.systemLimit.limits[0].limit = +val;
      } else {
        this.systemLimit.limits[0].limit = +val.replace(/[^0-9]+/, '');
      }
    }
  }

  async changeLimit () {
    let result : CardLimitRiskChangeResponseModel = new CardLimitRiskChangeResponseModel({ success: false, message: this.$t('components.shared.errorData.label').toString() });
    this.isSending = true;
    try {
      result = await cardApi.changeRiskAsync({
        clientId: parseInt(this.$route.params.id),
        cardId: this.detailData.cardId,
        limit: this.getSystemLimit,
        isP2PLimited: this.data.isP2PLimited,
        limitSetDate: new Date(),
        phone: this.data.phone
      });
    } finally {
      this.isSending = false;
      this.showSendingResult(result);
      // @ts-ignore
      this.$refs.dialog.hide();
    }
  }

  showSendingResult (result: CardLimitRiskChangeResponseModel) {
    if (result.success) {
      this.$q.notify({
        type: 'positive',
        message: result.message,
        position: 'top'
      });
    } else {
      this.$q.notify({
        type: 'negative',
        message: result.message,
        position: 'top'
      });
    }
  }

  cancel () {
    // @ts-ignore
    this.$refs.dialog.hide();
  }

  hideDialog () {
    this.$emit('hide');
  }
}
</script>

<style scoped>
</style>
