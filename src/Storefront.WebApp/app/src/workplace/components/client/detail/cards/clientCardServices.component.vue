<template>
   <div class="row q-mt-sm">
      <div class="col">
        <div class="q-pa-md q-mr-sm bg-grey-11 rounded-borders" style="height: 100%;">
          <div class="text-h6 q-mb-sm">SMS-info</div>
          <card-sms-info-component
            :detail-data="SmsInfoData"
            :is-loading="SmsInfoIsLoading"
            :has-error="SmsInfoHasError"
            @show-status="onShowStatus"
          />
        </div>
      </div>

      <div class="col">
        <div class="q-pa-md q-mr-sm bg-grey-11 rounded-borders" style="height: 100%;">
          <div class="text-h6 q-mb-sm">3D-Secure</div>
          <card-3-d-secure-component
            :detail-data="Secure3DData"
            :is-loading="Secure3DIsLoading"
            :has-error="Secure3DHasError"
            @show-status="onShowStatus"
          />
        </div>
      </div>

      <q-dialog v-model="isShowDialog">
          <q-card style="width: 364px">
            <q-card-section class="row items-center q-pb-none">
              <div class="text-h6 q-mt-sm">{{$t('components.client.detail.cards.clientCardServices.status')}} ServiceName</div>
              <q-space />
              <q-btn icon="close" flat round dense v-close-popup style="color: #A8A9AA" />
            </q-card-section>

            <q-card-section>
              <div class="column q-gutter-y-sm">
                <div>
                  <div class="text-subtitle2 text-grey-7">{{$t('components.client.detail.cards.clientCardServices.lastChange')}}</div>
                  <div class="text-subtitle1">11.03.2013 21:11:07</div>
                </div>
                <div>
                  <div class="text-subtitle2 text-grey-7">{{$t('components.client.detail.cards.clientCardServices.statusChangedBy')}}</div>
                  <div class="text-subtitle1">Клієнт</div>
                </div>
              </div>
            </q-card-section>
          </q-card>
        </q-dialog>
    </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import CardSmsInfoComponent from './services/cardSmsInfo.component.vue';
import Card3DSecureComponent from './services/card3DSecure.component.vue';

import { cardApi } from '../../../../api/card.api';
import { ApiWrapperActionHelper } from '../../../../helpers/apiWrapperAction.helper';
import ApiResultModel from '../../../../models/api/apiResult.model';
import { Store } from 'vuex';

import { CardSmsInfoRequestInterface, Card3DSecureRequestInterface } from '../../../../interfaces/requests/card/services';
import { CardSmsInfoResponseInterface, Card3DSecureResponseInterface } from '../../../../interfaces/responses/card/services';

@Component({
  components: { CardSmsInfoComponent, Card3DSecureComponent }
})
export default class ClientCardServicesComponent extends Vue {
  @Prop() cardId!: number;

  private isShowDialog: boolean;
  private SmsInfoData: any;
  private SmsInfoIsLoading: boolean;
  private SmsInfoHasError: boolean;

  private Secure3DData: any;
  private Secure3DIsLoading: boolean;
  private Secure3DHasError: boolean;

  constructor () {
    super();
    this.isShowDialog = false;

    this.SmsInfoData = null;
    this.SmsInfoIsLoading = false;
    this.SmsInfoHasError = false;

    this.Secure3DData = null;
    this.Secure3DIsLoading = false;
    this.Secure3DHasError = false;
  }

  created () {
    void this.doServicesRequest();
  }

  onShowStatus () {
    this.isShowDialog = true;
  }

  private async doServicesRequest () {
    this.SmsInfoIsLoading = true;
    this.Secure3DIsLoading = true;

    const smsInfo = this.loadCardSmsInfoServiceData(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.cardId });
    const secure3D = this.loadCard3DSecureServiceData(this.$store, { solarId: parseInt(this.$route.params.id), cardId: this.cardId });

    const resultSmsInfo = await smsInfo;
    if (resultSmsInfo instanceof ApiResultModel) {
      this.SmsInfoHasError = !!resultSmsInfo.hasError;
      this.SmsInfoData = resultSmsInfo.result?.item;
    }
    this.SmsInfoIsLoading = false;

    const resultSecure3D = await secure3D;
    if (resultSecure3D instanceof ApiResultModel) {
      this.Secure3DHasError = !!resultSecure3D.hasError;
      this.Secure3DData = resultSecure3D.result?.item;
    }
    this.Secure3DIsLoading = false;
  }

  private async loadCardSmsInfoServiceData (store: Store<any>, params:CardSmsInfoRequestInterface) : Promise<void | ApiResultModel<CardSmsInfoResponseInterface>> {
    const promise = cardApi.getSmsInfoAsync(params);
    return await ApiWrapperActionHelper.runWithTry<CardSmsInfoResponseInterface>(store, promise);
  }

  private async loadCard3DSecureServiceData (store: Store<any>, params:Card3DSecureRequestInterface) : Promise<void | ApiResultModel<Card3DSecureResponseInterface>> {
    const promise = cardApi.get3DSecureAsync(params);
    return await ApiWrapperActionHelper.runWithTry<Card3DSecureResponseInterface>(store, promise);
  }
}

</script>

<style scoped>
</style>
